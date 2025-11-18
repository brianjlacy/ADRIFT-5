#!/usr/bin/env python3
"""
VB.NET to C# Converter for ADRIFT-5 Project
Converts VB.NET source files to C# syntax
"""

import os
import re
import sys
from pathlib import Path
from typing import List, Tuple, Dict

class VBToCSharpConverter:
    def __init__(self):
        self.indent_level = 0
        self.in_region = False
        self.in_property = False
        self.in_namespace = False
        self.using_statements = set()

    def convert_file(self, vb_content: str, filename: str) -> str:
        """Convert VB.NET file content to C#"""
        lines = vb_content.split('\n')
        cs_lines = []
        self.indent_level = 0
        self.in_region = False
        self.in_property = False
        skip_next = False

        # Collect using statements first
        self.using_statements = self._collect_imports(lines)

        # Add using statements
        cs_lines.extend(self._generate_using_statements())
        cs_lines.append('')

        # Add namespace
        namespace = self._extract_namespace(lines)
        if namespace:
            cs_lines.append(f'namespace {namespace}')
            cs_lines.append('{')
            self.indent_level = 1

        i = 0
        while i < len(lines):
            if skip_next:
                skip_next = False
                i += 1
                continue

            line = lines[i]
            original_line = line
            line = line.rstrip()

            # Skip empty lines and imports (already handled)
            if not line or line.strip().startswith("'"):
                cs_lines.append(line)
                i += 1
                continue

            if self._is_import_line(line):
                i += 1
                continue

            # Convert the line
            converted = self._convert_line(line, i, lines)

            if isinstance(converted, list):
                cs_lines.extend(converted)
            elif converted:
                cs_lines.append(converted)

            i += 1

        # Close namespace
        if namespace:
            cs_lines.append('}')

        return '\n'.join(cs_lines)

    def _collect_imports(self, lines: List[str]) -> set:
        """Collect all Import statements"""
        imports = {
            'System',
            'System.Collections.Generic',
            'System.Linq'
        }

        for line in lines:
            match = re.match(r'\s*Imports?\s+(.+)', line, re.IGNORECASE)
            if match:
                import_name = match.group(1).strip()
                if not import_name.startswith('Microsoft.VisualBasic'):
                    imports.add(import_name)

        return imports

    def _generate_using_statements(self) -> List[str]:
        """Generate using statements"""
        return [f'using {imp};' for imp in sorted(self.using_statements)]

    def _extract_namespace(self, lines: List[str]) -> str:
        """Extract namespace from RootNamespace or infer it"""
        # Default namespace
        return 'ADRIFT'

    def _is_import_line(self, line: str) -> bool:
        """Check if line is an import statement"""
        return bool(re.match(r'\s*Imports?\s+', line, re.IGNORECASE))

    def _convert_line(self, line: str, line_num: int, all_lines: List[str]) -> str | List[str]:
        """Convert a single VB.NET line to C#"""
        stripped = line.strip()
        indent = self._get_indent(line)

        # Preprocessor directives
        if stripped.startswith('#'):
            return self._convert_preprocessor(line)

        # Comments
        if stripped.startswith("'"):
            return indent + '//' + stripped[1:]

        # Module declaration
        if re.match(r'^\s*(Public|Friend|Private)?\s*Module\s+(\w+)', stripped, re.IGNORECASE):
            return self._convert_module(line)

        # Class declaration
        if re.match(r'^\s*(Public|Friend|Private|Protected)?\s*(MustInherit|NotInheritable|Partial)?\s*Class\s+', stripped, re.IGNORECASE):
            return self._convert_class(line)

        # Interface declaration
        if re.match(r'^\s*(Public|Friend|Private)?\s*Interface\s+', stripped, re.IGNORECASE):
            return self._convert_interface(line)

        # Enum declaration
        if re.match(r'^\s*(Public|Friend|Private)?\s*Enum\s+', stripped, re.IGNORECASE):
            return self._convert_enum(line)

        # Property declaration
        if re.match(r'^\s*(Public|Private|Friend|Protected)?\s*(Shared|ReadOnly|WriteOnly)?\s*Property\s+', stripped, re.IGNORECASE):
            return self._convert_property(line, line_num, all_lines)

        # Function/Sub declaration
        if re.match(r'^\s*(Public|Private|Friend|Protected)?\s*(Shared|Overrides|Overridable|MustOverride)?\s*(Function|Sub)\s+', stripped, re.IGNORECASE):
            return self._convert_method(line)

        # Field/Variable declaration
        if re.match(r'^\s*(Public|Private|Friend|Protected|Dim)?\s*(Shared|ReadOnly|Const)?\s*(\w+)\s+(As|=)', stripped, re.IGNORECASE):
            return self._convert_field(line)

        # End statements
        if stripped.startswith('End '):
            return self._convert_end_statement(line)

        # Return statement
        if stripped.startswith('Return '):
            return self._convert_return(line)

        # If statements
        if re.match(r'^\s*If\s+.+\s+Then\s*$', stripped, re.IGNORECASE):
            return self._convert_if(line)

        # Select Case
        if re.match(r'^\s*Select\s+Case\s+', stripped, re.IGNORECASE):
            return self._convert_select(line)

        # Case statement
        if re.match(r'^\s*Case\s+', stripped, re.IGNORECASE):
            return self._convert_case(line)

        # For loop
        if re.match(r'^\s*For\s+', stripped, re.IGNORECASE):
            return self._convert_for(line)

        # For Each loop
        if re.match(r'^\s*For\s+Each\s+', stripped, re.IGNORECASE):
            return self._convert_foreach(line)

        # While loop
        if re.match(r'^\s*While\s+', stripped, re.IGNORECASE):
            return self._convert_while(line)

        # Do loop
        if re.match(r'^\s*Do\s*$', stripped, re.IGNORECASE):
            return indent + 'do\n' + indent + '{'

        # Region
        if re.match(r'^\s*#Region\s+', stripped, re.IGNORECASE):
            match = re.match(r'^\s*#Region\s+"(.+)"', stripped, re.IGNORECASE)
            if match:
                self.in_region = True
                return indent + f'#region {match.group(1)}'

        if re.match(r'^\s*#End\s+Region', stripped, re.IGNORECASE):
            self.in_region = False
            return indent + '#endregion'

        # Try-Catch
        if stripped.startswith('Try'):
            return indent + 'try\n' + indent + '{'

        if re.match(r'^\s*Catch\s+', stripped, re.IGNORECASE):
            return self._convert_catch(line)

        if stripped.startswith('Finally'):
            return indent + '}\n' + indent + 'finally\n' + indent + '{'

        # Throw
        if stripped.startswith('Throw '):
            return line.replace('Throw ', 'throw ')

        # Using statement
        if re.match(r'^\s*Using\s+', stripped, re.IGNORECASE):
            return self._convert_using(line)

        # Get/Set within property
        if re.match(r'^\s*(Get|Set)\s*$', stripped, re.IGNORECASE):
            return self._convert_accessor(line)

        # Assignment and method calls
        return self._convert_statement(line)

    def _convert_preprocessor(self, line: str) -> str:
        """Convert preprocessor directives"""
        line = re.sub(r'#If\s+', '#if ', line, flags=re.IGNORECASE)
        line = re.sub(r'#ElseIf\s+', '#elif ', line, flags=re.IGNORECASE)
        line = re.sub(r'#Else\s*$', '#else', line, flags=re.IGNORECASE)
        line = re.sub(r'#End\s+If', '#endif', line, flags=re.IGNORECASE)
        line = re.sub(r'\s+Then\s*$', '', line)
        line = re.sub(r'\s*=\s*True', '', line)
        line = re.sub(r'\s*=\s*False', ' == false', line)
        return line

    def _convert_module(self, line: str) -> str:
        """Convert Module to static class"""
        match = re.match(r'^(\s*)(Public|Friend|Private)?\s*Module\s+(\w+)', line, re.IGNORECASE)
        if match:
            indent = match.group(1) or ''
            visibility = match.group(2) or 'internal'
            name = match.group(3)

            if visibility.lower() == 'friend':
                visibility = 'internal'
            else:
                visibility = visibility.lower()

            return f'{indent}{visibility} static class {name}\n{indent}{{'
        return line

    def _convert_class(self, line: str) -> str:
        """Convert Class declaration"""
        line = re.sub(r'\bMustInherit\b', 'abstract', line, flags=re.IGNORECASE)
        line = re.sub(r'\bNotInheritable\b', 'sealed', line, flags=re.IGNORECASE)
        line = re.sub(r'\bPartial\b', 'partial', line, flags=re.IGNORECASE)
        line = re.sub(r'\bClass\b', 'class', line, flags=re.IGNORECASE)
        line = re.sub(r'\bPublic\b', 'public', line, flags=re.IGNORECASE)
        line = re.sub(r'\bPrivate\b', 'private', line, flags=re.IGNORECASE)
        line = re.sub(r'\bFriend\b', 'internal', line, flags=re.IGNORECASE)
        line = re.sub(r'\bProtected\b', 'protected', line, flags=re.IGNORECASE)

        indent = self._get_indent(line)
        return line.strip() + '\n' + indent + '{'

    def _convert_interface(self, line: str) -> str:
        """Convert Interface declaration"""
        line = re.sub(r'\bInterface\b', 'interface', line, flags=re.IGNORECASE)
        line = self._convert_visibility(line)
        indent = self._get_indent(line)
        return line.strip() + '\n' + indent + '{'

    def _convert_enum(self, line: str) -> str:
        """Convert Enum declaration"""
        line = re.sub(r'\bEnum\b', 'enum', line, flags=re.IGNORECASE)
        line = self._convert_visibility(line)
        indent = self._get_indent(line)
        return line.strip() + '\n' + indent + '{'

    def _convert_property(self, line: str, line_num: int, all_lines: List[str]) -> str | List[str]:
        """Convert Property declaration"""
        # Simple auto-property
        match = re.match(r'^(\s*)(Public|Private|Friend|Protected)?\s*(Shared|ReadOnly)?\s*Property\s+(\w+)\s*\(\s*\)\s+As\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1) or ''
            visibility = self._convert_visibility_keyword(match.group(2))
            shared = 'static ' if match.group(3) and match.group(3).lower() == 'shared' else ''
            readonly = match.group(3) and match.group(3).lower() == 'readonly'
            prop_name = match.group(4)
            prop_type = self._convert_type(match.group(5))

            if readonly:
                return f'{indent}{visibility} {shared}{prop_type} {prop_name} {{ get; }}'
            else:
                return f'{indent}{visibility} {shared}{prop_type} {prop_name} {{ get; set; }}'

        # Property with getter/setter
        match = re.match(r'^(\s*)(Public|Private|Friend|Protected)?\s*(Shared|ReadOnly)?\s*Property\s+(\w+)\s*\(\s*\)\s+As\s+(.+)$', line, re.IGNORECASE)
        if not match:
            match = re.match(r'^(\s*)(Public|Private|Friend|Protected)?\s*(Shared)?\s*Property\s+(\w+)\s+As\s+(.+)$', line, re.IGNORECASE)

        if match:
            indent = match.group(1) or ''
            visibility = self._convert_visibility_keyword(match.group(2))
            shared = 'static ' if match.group(3) and 'shared' in match.group(3).lower() else ''
            prop_name = match.group(4)
            prop_type = self._convert_type(match.group(5))

            # Check if next lines have Get/Set
            next_line_idx = line_num + 1
            has_accessor = False
            if next_line_idx < len(all_lines):
                next_line = all_lines[next_line_idx].strip()
                if next_line.lower() in ['get', 'set']:
                    has_accessor = True

            if has_accessor:
                self.in_property = True
                return f'{indent}{visibility} {shared}{prop_type} {prop_name}'
            else:
                return f'{indent}{visibility} {shared}{prop_type} {prop_name} {{ get; set; }}'

        return line

    def _convert_method(self, line: str) -> str:
        """Convert Function/Sub to method"""
        # Handle method declaration
        match = re.match(r'^(\s*)(Public|Private|Friend|Protected)?\s*(Shared|Overrides|Overridable|MustOverride)?\s*(Function|Sub)\s+(\w+)\s*\((.*?)\)(\s+As\s+(.+))?', line, re.IGNORECASE)

        if match:
            indent = match.group(1) or ''
            visibility = self._convert_visibility_keyword(match.group(2))
            modifiers = match.group(3) or ''
            method_type = match.group(4)
            method_name = match.group(5)
            params = match.group(6) or ''
            return_type_match = match.group(8)

            # Convert modifiers
            modifiers = modifiers.replace('Shared', 'static').replace('Overrides', 'override').replace('Overridable', 'virtual').replace('MustOverride', 'abstract')
            if modifiers:
                modifiers = modifiers.lower() + ' '

            # Convert parameters
            cs_params = self._convert_parameters(params)

            # Determine return type
            if method_type.lower() == 'sub' or not return_type_match:
                return_type = 'void'
            else:
                return_type = self._convert_type(return_type_match)

            # Check for abstract
            if 'abstract' in modifiers:
                return f'{indent}{visibility} {modifiers}{return_type} {method_name}({cs_params});'
            else:
                return f'{indent}{visibility} {modifiers}{return_type} {method_name}({cs_params})\n{indent}{{'

        return line

    def _convert_field(self, line: str) -> str:
        """Convert field declaration"""
        # Const
        match = re.match(r'^(\s*)(Public|Private|Friend|Protected)?\s*Const\s+(\w+)\s+As\s+(.+?)\s*=\s*(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1) or ''
            visibility = self._convert_visibility_keyword(match.group(2))
            field_name = match.group(3)
            field_type = self._convert_type(match.group(4))
            value = self._convert_expression(match.group(5))
            return f'{indent}{visibility} const {field_type} {field_name} = {value};'

        # Regular field with initialization
        match = re.match(r'^(\s*)(Public|Private|Friend|Protected|Dim)?\s*(Shared|ReadOnly)?\s*(\w+)\s+As\s+(.+?)\s*=\s*(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1) or ''
            visibility = self._convert_visibility_keyword(match.group(2))
            modifiers = match.group(3) or ''
            field_name = match.group(4)
            field_type = self._convert_type(match.group(5))
            value = self._convert_expression(match.group(6))

            modifiers = modifiers.replace('Shared', 'static').replace('ReadOnly', 'readonly')
            if modifiers:
                modifiers = modifiers.lower() + ' '

            return f'{indent}{visibility} {modifiers}{field_type} {field_name} = {value};'

        # Regular field without initialization
        match = re.match(r'^(\s*)(Public|Private|Friend|Protected|Dim)?\s*(Shared|ReadOnly)?\s*(\w+)\s+As\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1) or ''
            visibility = self._convert_visibility_keyword(match.group(2))
            modifiers = match.group(3) or ''
            field_name = match.group(4)
            field_type = self._convert_type(match.group(5))

            modifiers = modifiers.replace('Shared', 'static').replace('ReadOnly', 'readonly')
            if modifiers:
                modifiers = modifiers.lower() + ' '

            return f'{indent}{visibility} {modifiers}{field_type} {field_name};'

        return line

    def _convert_end_statement(self, line: str) -> str:
        """Convert End statements"""
        indent = self._get_indent(line)
        stripped = line.strip()

        if stripped.lower() == 'end property':
            self.in_property = False

        if stripped.lower() in ['end module', 'end class', 'end structure', 'end interface',
                                'end enum', 'end namespace', 'end sub', 'end function',
                                'end property', 'end get', 'end set', 'end if',
                                'end select', 'end while', 'end with', 'end try']:
            return indent + '}'

        if stripped.lower() == 'loop':
            return indent + '} while (true);'

        return line

    def _convert_return(self, line: str) -> str:
        """Convert Return statement"""
        match = re.match(r'^(\s*)Return\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            value = self._convert_expression(match.group(2))
            return f'{indent}return {value};'

        match = re.match(r'^(\s*)Return\s*$', line, re.IGNORECASE)
        if match:
            return match.group(1) + 'return;'

        return line

    def _convert_if(self, line: str) -> str:
        """Convert If statement"""
        match = re.match(r'^(\s*)If\s+(.+?)\s+Then\s*$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            condition = self._convert_expression(match.group(2))
            return f'{indent}if ({condition})\n{indent}{{'
        return line

    def _convert_select(self, line: str) -> str:
        """Convert Select Case"""
        match = re.match(r'^(\s*)Select\s+Case\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            expression = self._convert_expression(match.group(2))
            return f'{indent}switch ({expression})\n{indent}{{'
        return line

    def _convert_case(self, line: str) -> str:
        """Convert Case statement"""
        indent = self._get_indent(line)

        match = re.match(r'^\s*Case\s+Else\s*$', line, re.IGNORECASE)
        if match:
            return f'{indent}default:\n{indent}    {{'

        match = re.match(r'^\s*Case\s+(.+)$', line, re.IGNORECASE)
        if match:
            value = self._convert_expression(match.group(1))
            # Handle multiple values
            if ',' in value:
                cases = []
                for v in value.split(','):
                    cases.append(f'{indent}case {v.strip()}:')
                return '\n'.join(cases) + f'\n{indent}    {{'
            else:
                return f'{indent}case {value}:\n{indent}    {{'

        return line

    def _convert_for(self, line: str) -> str:
        """Convert For loop"""
        match = re.match(r'^(\s*)For\s+(\w+)\s+As\s+(.+?)\s*=\s*(.+?)\s+To\s+(.+?)(\s+Step\s+(.+))?\s*$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            var = match.group(2)
            var_type = self._convert_type(match.group(3))
            start = self._convert_expression(match.group(4))
            end = self._convert_expression(match.group(5))
            step = match.group(7) if match.group(7) else '1'
            step = self._convert_expression(step)

            if step == '1':
                return f'{indent}for ({var_type} {var} = {start}; {var} <= {end}; {var}++)\n{indent}{{'
            else:
                return f'{indent}for ({var_type} {var} = {start}; {var} <= {end}; {var} += {step})\n{indent}{{'

        return line

    def _convert_foreach(self, line: str) -> str:
        """Convert For Each loop"""
        match = re.match(r'^(\s*)For\s+Each\s+(\w+)\s+As\s+(.+?)\s+In\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            var = match.group(2)
            var_type = self._convert_type(match.group(3))
            collection = self._convert_expression(match.group(4))
            return f'{indent}foreach ({var_type} {var} in {collection})\n{indent}{{'

        return line

    def _convert_while(self, line: str) -> str:
        """Convert While loop"""
        match = re.match(r'^(\s*)While\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            condition = self._convert_expression(match.group(2))
            return f'{indent}while ({condition})\n{indent}{{'
        return line

    def _convert_catch(self, line: str) -> str:
        """Convert Catch statement"""
        indent = self._get_indent(line)
        match = re.match(r'^\s*Catch\s+(\w+)\s+As\s+(.+)$', line, re.IGNORECASE)
        if match:
            var = match.group(1)
            exc_type = self._convert_type(match.group(2))
            return f'{indent}}}\n{indent}catch ({exc_type} {var})\n{indent}{{'

        match = re.match(r'^\s*Catch\s*$', line, re.IGNORECASE)
        if match:
            return f'{indent}}}\n{indent}catch\n{indent}{{'

        return line

    def _convert_using(self, line: str) -> str:
        """Convert Using statement"""
        match = re.match(r'^(\s*)Using\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            declaration = match.group(2)

            # Check if it's a declaration or just a variable
            if ' As ' in declaration:
                declaration = self._convert_variable_declaration(declaration)
            else:
                declaration = self._convert_expression(declaration)

            return f'{indent}using ({declaration})\n{indent}{{'
        return line

    def _convert_accessor(self, line: str) -> str:
        """Convert Get/Set accessors"""
        indent = self._get_indent(line)
        stripped = line.strip()

        if stripped.lower() == 'get':
            return f'{indent}{{\n{indent}    get\n{indent}    {{'
        elif stripped.lower() == 'set':
            # Property setter - need to close previous accessor
            return f'{indent}    }}\n{indent}    set\n{indent}    {{'

        return line

    def _convert_statement(self, line: str) -> str:
        """Convert general statements"""
        indent = self._get_indent(line)
        stripped = line.strip()

        if not stripped:
            return line

        # Handle Set (value assignment)
        if stripped.lower().startswith('set('):
            return line.replace('Set(', 'set(').replace('value As ', '').replace(')', ')').strip()

        # Variable declaration with Dim
        if re.match(r'^\s*Dim\s+', stripped, re.IGNORECASE):
            return self._convert_variable_declaration(line)

        # Convert Nothing to null
        line = re.sub(r'\bNothing\b', 'null', line)

        # Convert boolean literals
        line = re.sub(r'\bTrue\b', 'true', line)
        line = re.sub(r'\bFalse\b', 'false', line)

        # Convert Is to ==
        line = re.sub(r'\s+Is\s+', ' == ', line)
        line = re.sub(r'\s+IsNot\s+', ' != ', line)

        # Convert AndAlso/OrElse
        line = re.sub(r'\bAndAlso\b', '&&', line)
        line = re.sub(r'\bOrElse\b', '||', line)
        line = re.sub(r'\bAnd\b', '&', line)
        line = re.sub(r'\bOr\b', '|', line)
        line = re.sub(r'\bNot\b', '!', line)

        # Convert string concatenation
        line = re.sub(r'\s+&\s+', ' + ', line)

        # Convert CType, DirectCast
        line = re.sub(r'\bCType\((.+?),\s*(.+?)\)', r'(\2)(\1)', line)
        line = re.sub(r'\bDirectCast\((.+?),\s*(.+?)\)', r'(\2)(\1)', line)
        line = re.sub(r'\bTryCast\((.+?),\s*(.+?)\)', r'(\1 as \2)', line)

        # Convert TypeOf...Is
        line = re.sub(r'\bTypeOf\s+(.+?)\s+Is\s+(.+?)(\s|$)', r'\1 is \2\3', line)

        # Add semicolon if needed (and not already a control structure)
        if not any(stripped.lower().startswith(kw) for kw in ['if', 'else', 'for', 'while', 'do', 'switch', 'case', 'try', 'catch', 'finally', 'using', '{', '}', 'namespace', 'class', 'public', 'private', 'protected', 'internal']):
            if not stripped.endswith((';', '{', '}')):
                line = line.rstrip() + ';'

        return line

    def _convert_variable_declaration(self, line: str) -> str:
        """Convert variable declaration"""
        match = re.match(r'^(\s*)Dim\s+(\w+)\s+As\s+(.+?)\s*=\s*(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            var_name = match.group(2)
            var_type = self._convert_type(match.group(3))
            value = self._convert_expression(match.group(4))
            return f'{indent}{var_type} {var_name} = {value};'

        match = re.match(r'^(\s*)Dim\s+(\w+)\s+As\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            var_name = match.group(2)
            var_type = self._convert_type(match.group(3))
            return f'{indent}{var_type} {var_name};'

        # Inferred type (As New)
        match = re.match(r'^(\s*)Dim\s+(\w+)\s+As\s+New\s+(.+)$', line, re.IGNORECASE)
        if match:
            indent = match.group(1)
            var_name = match.group(2)
            var_type = self._convert_type(match.group(3))
            return f'{indent}var {var_name} = new {var_type};'

        return line

    def _convert_parameters(self, params: str) -> str:
        """Convert parameter list"""
        if not params.strip():
            return ''

        cs_params = []
        for param in params.split(','):
            param = param.strip()

            # Optional parameters
            optional_match = re.match(r'Optional\s+(.+)', param, re.IGNORECASE)
            if optional_match:
                param = optional_match.group(1)

            # ByVal/ByRef
            param = re.sub(r'\bByVal\s+', '', param, flags=re.IGNORECASE)
            param = re.sub(r'\bByRef\s+', 'ref ', param, flags=re.IGNORECASE)

            # Parameter name and type
            match = re.match(r'(\w+)\s+As\s+(.+?)(\s*=\s*(.+))?$', param, re.IGNORECASE)
            if match:
                param_name = match.group(1)
                param_type = self._convert_type(match.group(2))
                default_value = match.group(4)

                if default_value:
                    default_value = self._convert_expression(default_value)
                    cs_params.append(f'{param_type} {param_name} = {default_value}')
                else:
                    cs_params.append(f'{param_type} {param_name}')
            else:
                cs_params.append(param)

        return ', '.join(cs_params)

    def _convert_type(self, vb_type: str) -> str:
        """Convert VB.NET type to C# type"""
        vb_type = vb_type.strip()

        type_map = {
            'Integer': 'int',
            'Long': 'long',
            'Short': 'short',
            'Byte': 'byte',
            'Boolean': 'bool',
            'Single': 'float',
            'Double': 'double',
            'Decimal': 'decimal',
            'String': 'string',
            'Char': 'char',
            'Date': 'DateTime',
            'Object': 'object',
            'Variant': 'object'
        }

        # Handle arrays
        array_match = re.match(r'(.+?)\(\s*\)', vb_type)
        if array_match:
            base_type = self._convert_type(array_match.group(1))
            return f'{base_type}[]'

        # Handle generics
        generic_match = re.match(r'(.+?)\(Of\s+(.+)\)', vb_type, re.IGNORECASE)
        if generic_match:
            base_type = generic_match.group(1)
            generic_params = generic_match.group(2)
            generic_params_cs = ', '.join([self._convert_type(t.strip()) for t in generic_params.split(',')])
            return f'{base_type}<{generic_params_cs}>'

        return type_map.get(vb_type, vb_type)

    def _convert_expression(self, expr: str) -> str:
        """Convert VB.NET expression to C#"""
        expr = expr.strip()

        # Convert Nothing to null
        expr = re.sub(r'\bNothing\b', 'null', expr)

        # Convert boolean literals
        expr = re.sub(r'\bTrue\b', 'true', expr)
        expr = re.sub(r'\bFalse\b', 'false', expr)

        # Convert string concatenation
        expr = re.sub(r'\s+&\s+', ' + ', expr)

        # Convert operators
        expr = re.sub(r'\bAndAlso\b', '&&', expr)
        expr = re.sub(r'\bOrElse\b', '||', expr)
        expr = re.sub(r'\bMod\b', '%', expr)

        # Convert New keyword
        expr = re.sub(r'\bNew\s+', 'new ', expr)

        return expr

    def _convert_visibility(self, line: str) -> str:
        """Convert visibility modifiers"""
        line = re.sub(r'\bPublic\b', 'public', line, flags=re.IGNORECASE)
        line = re.sub(r'\bPrivate\b', 'private', line, flags=re.IGNORECASE)
        line = re.sub(r'\bFriend\b', 'internal', line, flags=re.IGNORECASE)
        line = re.sub(r'\bProtected\b', 'protected', line, flags=re.IGNORECASE)
        return line

    def _convert_visibility_keyword(self, keyword: str) -> str:
        """Convert a single visibility keyword"""
        if not keyword:
            return 'private'

        keyword = keyword.lower()
        if keyword == 'public':
            return 'public'
        elif keyword == 'private':
            return 'private'
        elif keyword == 'friend':
            return 'internal'
        elif keyword == 'protected':
            return 'protected'
        else:
            return 'private'

    def _get_indent(self, line: str) -> str:
        """Get indentation from a line"""
        match = re.match(r'^(\s*)', line)
        return match.group(1) if match else ''


def convert_vb_file_to_cs(vb_file_path: str, output_dir: str):
    """Convert a single VB.NET file to C#"""
    try:
        with open(vb_file_path, 'r', encoding='utf-8-sig') as f:
            vb_content = f.read()
    except UnicodeDecodeError:
        with open(vb_file_path, 'r', encoding='windows-1252') as f:
            vb_content = f.read()

    converter = VBToCSharpConverter()
    cs_content = converter.convert_file(vb_content, os.path.basename(vb_file_path))

    # Determine output path
    relative_path = os.path.relpath(vb_file_path, start=os.path.dirname(output_dir))
    cs_file_path = os.path.join(output_dir, relative_path.replace('.vb', '.cs'))

    # Create directory if needed
    os.makedirs(os.path.dirname(cs_file_path), exist_ok=True)

    # Write C# file
    with open(cs_file_path, 'w', encoding='utf-8') as f:
        f.write(cs_content)

    print(f'Converted: {vb_file_path} -> {cs_file_path}')
    return cs_file_path


def convert_project(source_dir: str, output_dir: str):
    """Convert all VB.NET files in a directory"""
    vb_files = list(Path(source_dir).rglob('*.vb'))

    print(f'Found {len(vb_files)} VB.NET files to convert')

    for vb_file in vb_files:
        try:
            convert_vb_file_to_cs(str(vb_file), output_dir)
        except Exception as e:
            print(f'Error converting {vb_file}: {e}')
            import traceback
            traceback.print_exc()


if __name__ == '__main__':
    if len(sys.argv) < 3:
        print('Usage: python vb_to_csharp_converter.py <source_dir> <output_dir>')
        sys.exit(1)

    source_dir = sys.argv[1]
    output_dir = sys.argv[2]

    convert_project(source_dir, output_dir)
    print('Conversion complete!')
