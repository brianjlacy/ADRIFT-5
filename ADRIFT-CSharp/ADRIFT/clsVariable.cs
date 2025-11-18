using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
public class clsVariable
{
    Inherits clsItem;

    private string sName;
    'Private sKey As String
    Private iIntValue(0) As Integer
    Private sStringValue(0) As String
    private int iLength = 1;

public enum VariableTypeEnum
    {
        Numeric;
        Text;
    }
    private VariableTypeEnum eVariableType;

    'Public Property Key() As String
    '    Get
    '        Return sKey
    '    End Get
    '    Set(ByVal Value As String)
    '        If Not KeyExists(Value) Then
    '            sKey = Value
    '        Else
    '            Throw New Exception("Key " & sKey & " already exists")
    '        End If
    '    End Set
    'End Property


    public string Name { get; set; }
        {
            get
            {
            return sName;
        }
set(ByVal Value As String)
            sName = Value
        }
    }

    'Private bIsLibrary As Boolean
    'Public Property IsLibrary() As Boolean
    '    Get
    '        Return bIsLibrary
    '    End Get
    '    Set(ByVal value As Boolean)
    '        bIsLibrary = value
    '    End Set
    'End Property

    'Private dtLastUpdated As Date
    'Friend Property LastUpdated() As Date
    '    Get
    '        If dtLastUpdated > Date.MinValue Then
    '            Return dtLastUpdated
    '        Else
    '            Return Now
    '        End If
    '    End Get
    '    Set(ByVal value As Date)
    '        dtLastUpdated = value
    '    End Set
    'End Property

    Public Overrides ReadOnly Property Clone() As clsItem
        {
            get
            {
            return CType(Me.MemberwiseClone, clsVariable);
        }
    }

    public int Length { get; set; }
        {
            get
            {
            return iLength;
        }
set(ByVal Integer)
            if (Type = VariableTypeEnum.Numeric)
            {
                ReDim iIntValue(value - 1);
                ReDim sStringValue(0);
                sStringValue(0) = "";
            Else
                ReDim sStringValue(value - 1);
                Erase iIntValue;
            }
            iLength = value
        }
    }


    Public Property IntValue(Optional ByVal iIndex As Integer = 1) As Integer
        {
            get
            {
            if (iIndex > 0 && iIntValue IsNot null && iIndex < iIntValue.Length + 1)
            {
                return iIntValue(iIndex - 1) ' 1 - based indices;
            Else
                if (iIntValue IsNot null)
                {
                    ErrMsg("Attempting to read index " + iIndex + " outside bounds of array of variable " + Me.Name + IIf(iIntValue.Length > 1, "(" + iIntValue.Length + ")", "").ToString);
                Else
                    ErrMsg("Attempting to read index " + iIndex + " outside bounds of array of variable (0)");
                }
                return 0;
            }
        }
set(ByVal Value As Integer)
            if (iIndex > 0 && iIndex < iIntValue.Length + 1)
            {
                iIntValue(iIndex - 1) = Value;
            Else
                ErrMsg("Attempting to set index " + iIndex + " outside bounds of array of variable " + Me.Name + IIf(iIntValue.Length > 1, "(" + iIntValue.Length + ")", "").ToString);
            }
        }
    }


    Friend Property StringValue(Optional ByVal iIndex As Integer = 1) As String
        {
            get
            {
            if (iIndex > 0 && iIndex < sStringValue.Length + 1)
            {
                return sStringValue(iIndex - 1);
            Else
                ErrMsg("Attempting to read index " + iIndex + " outside bounds of array of variable " + Me.Name + IIf(sStringValue.Length > 1, "(" + sStringValue.Length + ")", "").ToString);
                return "";
            }
        }
set(ByVal Value As String)
            if (iIndex > 0 && iIndex < sStringValue.Length + 1)
            {
                sStringValue(iIndex - 1) = Value;
            ElseIf iIndex <= 0 Then
                ErrMsg("Attempting to set index " + iIndex + " outside bounds of array of variable " + Me.Name + IIf(sStringValue.Length > 1, "(" + sStringValue.Length + ")", "").ToString + ".  ADRIFT arrays start at 1, not 0.");
            Else
                ErrMsg("Attempting to set index " + iIndex + " outside bounds of array of variable " + Me.Name + IIf(sStringValue.Length > 1, "(" + sStringValue.Length + ")", "").ToString);
            }
        }
    }

    public VariableTypeEnum Type { get; set; }
        {
            get
            {
            return eVariableType;
        }
set(ByVal Value As VariableTypeEnum)
            eVariableType = Value
            switch (Value)
            {
                case VariableTypeEnum.Numeric:
                    {
                    ReDim iIntValue(0);
                    ReDim sStringValue(0);
                    sStringValue(0) = "";
                case VariableTypeEnum.Text:
                    {
                    ReDim sStringValue(0);
                    Erase iIntValue;
            }
        }
    }


#Region "Expressions"

    Structure TokenType;
        private string Token;
        private string Value;
        private int Left;
        private int Right;
    }
    Private Token() As TokenType
    private int iToken;

    private string sParseString;
    private string sParseToken;


    private string GetToken(ref text As String)
    {

        private int n;

        GetToken = Nothing

        switch (sLeft(text, 1))
        {
            case "*":
            case "/":
            case "+":
            case "-":
            case "^":
            case "(":
            case ")":
            case ":
            case ":
                {
                GetToken = sLeft(text, 1)
                sParseString = sRight(sParseString, sParseString.Length - 1)
                Exit Function;
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9" '0 To 9:
                {
                n = 1
                private bool bDP = false;
                while (IsNumeric(sMid(text, n, 1)) || (Not bDP && sMid(text, n, 1) = "."))
                {
                    If sMid(text, n, 1) = "." Then bDP = true
                    n = n + 1
                }
                n = n - 1
                GetToken = sLeft(text, n)
                sParseString = sRight(sParseString, sParseString.Length - n)
            case Chr(34):
                {
                if (sInstr(2, text, Chr(34)) > 0)
                {
                    GetToken = "vlu" & sMid(text, 2, sInstr(2, text, Chr(34)) - 2)
                    sParseString = sRight(sParseString, sParseString.Length - GetToken.Length + 1)
                }
            case "'":
                {
                if (sInstr(2, text, "'") > 0)
                {
                    GetToken = "vlu" & sMid(text, 2, sInstr(2, text, "'") - 2)
                    sParseString = sRight(sParseString, sParseString.Length - GetToken.Length + 1)
                }
            case "%":
                {
                For Each var As clsVariable In Adventure.htblVariables.Values
                    if (var.Length > 1 && text.ToLower.StartsWith("%" + var.Name.ToLower + "["))
                    {
                        private string sArgs = GetFunctionArgs(text.Substring(InStr(text.ToLower, "%" + var.Name.ToLower + "[") + var.Name.Length));
                        GetToken = "var-" & var.Key & "[" & ReplaceFunctions(sArgs) & "]"
                        sParseString = sParseString.Replace("%" & var.Name & "[" & sArgs & "]%", "") ' sRight(sParseString, sParseString.Length - var.Name.Length - 2)
                        Exit Function;
                    }
                    if (sLeft(text, var.Name.Length + 2) = "%" + var.Name + "%")
                    {
                        GetToken = "var-" & var.Key
                        sParseString = sRight(sParseString, sParseString.Length - var.Name.Length - 2)
                        Exit Function;
                    }
                Next;
                'For n = 0 To num_var - 1
                '    With variable(n)
                '        If sLeft(text, Len(.name) + 2) = "%" & .name & "%" Then
                '            GetToken = "var" & n
                '            sParseString = sRight(sParseString, sParseString.Length - Len(.name) - 2)
                '            Exit Function
                '        End If
                '    End With
                'Next n
                if (sLeft(text.ToLower, 6) = "%loop%")
                {
                    GetToken = "lop"
                    sParseString = sRight(sParseString, sParseString.Length - 6)
                }
                if (sLeft(text.ToLower, 10) = "%maxscore%")
                {
                    GetToken = "mxs"
                    sParseString = sRight(sParseString, sParseString.Length - 10)
                }
                if (sLeft(text.ToLower, 8) = "%number%")
                {
                    GetToken = "num"
                    sParseString = sRight(sParseString, sParseString.Length - 8)
                }
                if (sLeft(text.ToLower, 7) = "%score%")
                {
                    GetToken = "sco"
                    sParseString = sRight(sParseString, sParseString.Length - 7)
                }
                if (sLeft(text.ToLower, 6) = "%time%")
                {
                    GetToken = "tim"
                    sParseString = sRight(sParseString, sParseString.Length - 6)
                }
                if (sLeft(text.ToLower, 7) = "%turns%")
                {
                    GetToken = "trn"
                    sParseString = sRight(sParseString, sParseString.Length - 7)
                }
                if (sLeft(text.ToLower, 9) = "%version%")
                {
                    GetToken = "ver"
                    sParseString = sRight(sParseString, sParseString.Length - 9)
                }
                if (sLeft(text.ToLower, 6) = "%text%")
                {
                    GetToken = "txt"
                    sParseString = sRight(sParseString, sParseString.Length - 6)
                }
                if (sLeft(text.ToLower, 8) = "%player%")
                {
                    GetToken = "ply"
                    sParseString = sRight(sParseString, sParseString.Length - 8)
                }
                'If sLeft(text.ToLower, 15) = "%propertyvalue[" AndAlso text.ToLower.Contains("]%") Then
                '    GetToken = "prv-" & sLeft(text, sInstr(text.ToLower, "]%") + 2)
                '    sParseString = sRight(sParseString, sParseString.Length - sInstr(text.ToLower, "]%") - 1)
                'End If
                'If LCase(sLeft(text, 11)) = "%popupinput" AndAlso text.ToLower.Contains("]%") Then
                '    GetToken = "pin-" & sLeft(text, sInstr(text.ToLower, "]%") + 2)
                '    sParseString = sRight(sParseString, sParseString.Length - sInstr(text.ToLower, "]%") - 1)
                '    Exit Function
                'End If
                'If LCase(sLeft(text, 12)) = "%popupchoice" AndAlso text.ToLower.Contains("]%") Then
                '    GetToken = "pch-" & sLeft(text, sInstr(text.ToLower, "]%") + 2)
                '    sParseString = sRight(sParseString, sParseString.Length - sInstr(text.ToLower, "]%") - 1)
                '    Exit Function
                'End If
                For Each fn As String In FunctionNames()
                    if (LCase(text).StartsWith("%" + fn.ToLower + "[") && text.ToLower.Contains("]%"))
                    {
                        private string sArgs = GetFunctionArgs(text.Substring(InStr(text.ToLower, "%" + fn + "[") + fn.Length + 1));
                        GetToken = "fun-%" & fn & "[" & sArgs & "]%"
                        'sParseString = sRight(sParseString, sParseString.Length - sInstr(text.ToLower, "]%") - 1)
                        sParseString = sParseString.Replace(GetToken.Substring(4), "")
                        Exit Function;
                    }
                Next;
                For Each ref As String In ReferenceNames()
                    if (sLeft(text.ToLower, ref.Length) = ref.ToLower)
                    {
                        GetToken = "ref-" & ref
                        sParseString = sRight(sParseString, sParseString.Length - ref.Length)
                        Exit For;
                    }
                Next;

            case "|":
                {
                if (sLeft(text, 2) = "||")
                {
                    GetToken = "OR"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                GetToken = "OR"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case "&":
                {
                if (sLeft(text, 2) = "&&")
                {
                    GetToken = "AND"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                GetToken = "AND"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case "=":
                {
                if (sLeft(text, 2) = "==")
                {
                    GetToken = "EQ"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                GetToken = "EQ"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case "<":
                {
                if (sLeft(text, 2) = "<>")
                {
                    GetToken = "NE"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                if (sLeft(text, 2) = "<=")
                {
                    GetToken = "LE"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                GetToken = "LT"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case ">":
                {
                if (sLeft(text, 2) = ">=")
                {
                    GetToken = "GE"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                GetToken = "GT"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case "!":
                {
                if (sLeft(text, 2) = "!=")
                {
                    GetToken = "NE"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                GetToken = "!"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case "a":
            case "A":
                {
                if (LCase(sLeft(text, 3)) = "and")
                {
                    GetToken = "AND"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                if (LCase(sLeft(text, 3)) = "abs")
                {
                    GetToken = "abs"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                'GetToken = "a"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "e":
            case "E":
                {
                if (LCase(sLeft(text, 6)) = "either")
                {
                    GetToken = "either"
                    sParseString = sRight(sParseString, sParseString.Length - 6)
                    Exit Function;
                }
                'GetToken = "e"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "i":
            case "I":
                {
                if (LCase(sLeft(text, 2)) = "if")
                {
                    GetToken = "if"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                if (LCase(sLeft(text, 5)) = "instr")
                {
                    GetToken = "ist"
                    sParseString = sRight(sParseString, sParseString.Length - 5)
                    Exit Function;
                }
                'GetToken = "i"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "l":
            case "L":
                {
                if (LCase(sLeft(text, 5)) = "lower" Or LCase(sLeft(text, 5)) = "lcase")
                {
                    GetToken = "lwr"
                    sParseString = sRight(sParseString, sParseString.Length - 5)
                    Exit Function;
                }
                if (LCase(sLeft(text, 4)) = "left")
                {
                    GetToken = "lft"
                    sParseString = sRight(sParseString, sParseString.Length - 4)
                    Exit Function;
                }
                if (LCase(sLeft(text, 3)) = "len")
                {
                    GetToken = "len"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                'GetToken = "l"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "m":
            case "M":
                {
                if (LCase(sLeft(text, 3)) = "max")
                {
                    GetToken = "max"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                if (LCase(sLeft(text, 3)) = "min")
                {
                    GetToken = "min"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                if (LCase(sLeft(text, 3)) = "mod")
                {
                    GetToken = "mod"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                if (LCase(sLeft(text, 3)) = "mid")
                {
                    GetToken = "mid"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                'GetToken = "m"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "o":
            case "O":
                {
                if (LCase(sLeft(text, 2)) = "or")
                {
                    GetToken = "OR"
                    sParseString = sRight(sParseString, sParseString.Length - 2)
                    Exit Function;
                }
                'GetToken = "o"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "p":
            case "P":
                {
                if (LCase(sLeft(text, 6)) = "proper" Or LCase(sLeft(text, 5)) = "pcase")
                {
                    GetToken = "ppr"
                    sParseString = sRight(sParseString, sParseString.Length - 5)
                    Exit Function;
                }
                'GetToken = "p"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "r":
            case "R":
                {
                if (LCase(sLeft(text, 4)) = "rand")
                {
                    GetToken = "rand"
                    sParseString = sRight(sParseString, sParseString.Length - 4)
                    Exit Function;
                }
                if (LCase(sLeft(text, 5)) = "right")
                {
                    GetToken = "rgt"
                    sParseString = sRight(sParseString, sParseString.Length - 5)
                    Exit Function;
                }
                'GetToken = "r"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "s":
            case "S":
                {
                if (LCase(sLeft(text, 3)) = "str")
                {
                    GetToken = "str"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                'GetToken = "s"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            case "u":
            case "U":
                {
                if (LCase(sLeft(text, 5)) = "upper" Or LCase(sLeft(text, 5)) = "ucase")
                {
                    GetToken = "upr"
                    sParseString = sRight(sParseString, sParseString.Length - 5)
                    Exit Function;
                }
                GetToken = "u"
                sParseString = sRight(sParseString, sParseString.Length - 1)
            case "v":
            case "V":
                {
                if (LCase(sLeft(text, 3)) = "val")
                {
                    GetToken = "val"
                    sParseString = sRight(sParseString, sParseString.Length - 3)
                    Exit Function;
                }
                'GetToken = "v"
                'sParseString = sRight(sParseString, sParseString.Length - 1)
            default:
                {
                'GetToken = sLeft(text, 1)
                'sParseString = sRight(sParseString, sParseString.Length - 1)
                'Exit Function
        }

        If GetToken == null Then GetToken = ""
        if (GetToken = "")
        {
            ' Treat the token like a string
            for (int i = 0; i <= sParseString.Length - 1; i++)
            {
                switch (sParseString(i))
                {
                    case "A"c To "Z"c:
                    case "a"c To "z"c:
                    case "0"c To "9"c:
                        {
                        GetToken &= sParseString(i);
                    default:
                        {
                        sParseString = sRight(sParseString, sParseString.Length - GetToken.Length)
                        GetToken = "vlu" & GetToken
                        Exit Function;
                }
            Next;
        }

    }


    private void PrintTokens()
    {

        private int iToken = GetFirstToken() '0;
        while (iToken <> -1)
        {
            iToken = PrintToken(iToken)
        }
        Debug.WriteLine("");

    }


    private int PrintToken(int iToken)
    {

        With Token(iToken);
            Debug.Write("[" + iToken + ": " + .Token + "/" + .Value + "] ");
            return .Right;
        }

    }


    private int GetFirstToken()
    {

        for (int i = Token.Length - 1; i <= 0; i += -1)
        {
            If Token(i).Left = -1 Then Return i
        Next;

    }


    public void SetToExpression(string sExpression, int iIndex = 1, bool bThrowExceptionOnBadExpression = false)
    {

        try
        {
            sExpression = sExpression.Replace("%Loop%", iIndex.ToString)
            sExpression = ReplaceFunctions(sExpression, True)
            ' Otherwise IF(%object%="Object1", "a", "b") becomes IF(Object1="Object1"...
            ' But it is needed so we can evaluate expressions like %object%.Weight+%object%.Children.Weight

            ' Remove spaces
            sExpression = sExpression.Replace("\""", "~@&#~")

            sParseString = StripRedundantSpaces(sExpression) ' Replace(expression, " ", "")
            private string sPrevious = "972398";

            ' Set up initial tokens
            iToken = 0
            while (sParseString <> "" && sParseString <> sPrevious)
            {
                sPrevious = sParseString
                ReDim Preserve Token(iToken + 1);
                If iToken > 0 Then Token(iToken - 1).Right = iToken
                With Token(iToken);
                    .Token = GetToken(sParseString);
                    If .Token != null Then .Token = .Token.Replace("~@&#~", """")
                    .Value = "0";
                    .Left = -1;
                    .Right = -1;
                    If iToken > 0 Then .Left = iToken - 1
                }
                iToken = iToken + 1
            }

            private int n;

            If iToken = 0 Then Exit Sub

            ' Set initial tokens to normal ones
            For n = 0 To iToken - 1
                With Token(n);
                    if (IsNumeric(.Token))
                    {
                        .Value = .Token;
                        .Token = "expr";
                    Else
                        switch (.Token)
                        {
                            case "*":
                            case "/":
                            case "+":
                            case "-":
                            case "^":
                            case "mod":
                                {
                                .Value = .Token;
                                .Token = "op";
                            case "(":
                                {
                                .Value = .Token;
                                .Token = "lp";
                            case ")":
                                {
                                .Value = .Token;
                                .Token = "rp";
                            case ":
                            case ":
                                {
                                .Value = .Token;
                                .Token = "comma";
                            case "txt":
                                {
                                .Value = "" 'reftext;
#if Runner
                                For Each ref As clsNewReference In UserSession.NewReferences
                                    if (ref.ReferenceType = ReferencesType.Text)
                                    {
                                        if (ref.Items.Count = 1)
                                        {
                                            if (ref.Items(0).MatchingPossibilities.Count = 1)
                                            {
                                                .Value = ref.Items(0).MatchingPossibilities(0);
                                            }
                                        }
                                    }
                                Next;
#endif
                                .Token = "expr";
                            case "min":
                            case "max":
                            case "if":
                            case "rand":
                            case "either":
                            case "abs":
                            case "upr":
                            case "lwr":
                            case "ppr":
                            case "rgt":
                            case "lft":
                            case "mid":
                            case "ist":
                            case "len":
                            case "val":
                            case "str":
                                {
                                .Value = .Token;
                                .Token = "funct";
                            case "EQ":
                            case "GT":
                            case "LT":
                            case "LE":
                            case "GE":
                            case "NE":
                                {
                                .Value = .Token;
                                .Token = "TESTOP";
                            case "AND":
                            case "OR":
                                {
                                .Value = .Token;
                                .Token = "LOGIC";
                            case "lop":
                                {
                                .Value = iIndex.ToString;
                                .Token = "expr";
                            case "num":
                                {
                                .Value = "" ' refnumber;
#if Runner
                                For Each ref As clsNewReference In UserSession.NewReferences
                                    if (ref.ReferenceType = ReferencesType.Number)
                                    {
                                        if (ref.Items.Count = 1)
                                        {
                                            if (ref.Items(0).MatchingPossibilities.Count = 1)
                                            {
                                                .Value = ref.Items(0).MatchingPossibilities(0);
                                            }
                                        }
                                    }
                                Next;
#endif
                                .Token = "expr";
                            case "ver":
                                {
                                .Value = Application.ProductVersion ' sLeft(Application.ProductVersion.Replace(".", ""), 4) ' App.Major + App.Minor + App.Revision + Format(build, "0000");
                                private string[] sVer = Application.ProductVersion.Split("."c);
                                .Value = sVer(0) + CInt(sVer(1)).ToString("00") + CInt(sVer(2)).ToString("0000");
                                .Token = "expr";
                            case "sco":
                                {
                                .Value = Adventure.Score.ToString ' scor;
                                .Token = "expr";
                            case "mxs":
                                {
                                .Value = Adventure.MaxScore.ToString;
                                .Token = "expr";
                            case "ply":
                                {
#if Runner
                                .Value = Adventure.Player.Name;
                                .Token = "expr";
#endif
                            case "trn":
                                {
                                .Value = Adventure.Turns.ToString;
                                .Token = "expr";
                            case "tim":
                                {
                                .Value = "0" 'Int(Timer) - starttime;
                                .Token = "expr";
                            default:
                                {
                                if (sLeft(.Token, 3) = "var")
                                {
                                    private string sVar = .Token.Replace("var-", "");
                                    private int iVarIndex = 1;
                                    if (sVar.Contains("["))
                                    {
                                        private string sIndex = sVar.Substring(InStr(sVar, "["));
                                        sIndex = sLeft(sIndex, sIndex.Length - 1)
                                        iVarIndex = SafeInt(sIndex)
                                        sVar = sVar.Replace("[" & sIndex & "]", "")
                                    }
                                    private clsVariable var = Adventure.htblVariables(sVar);
                                    if (var.Type = VariableTypeEnum.Text)
                                    {
                                        .Value = var.StringValue(iVarIndex).ToString;
                                    Else
                                        .Value = var.IntValue(iVarIndex).ToString;
                                    }
                                    .Token = "expr";
                                ElseIf sLeft(.Token, 3) = "vlu" Then
                                    .Value = sRight(.Token, Len(.Token) - 3);
                                    .Token = "expr";
                                ElseIf sLeft(.Token, 3) = "prv" Then
                                    .Value = ReplaceFunctions(.Token.Substring(4));
#if Generator
                                    ' ReplaceFunctions may not be able to evaluate value if refs %object%
                                    if (.Value = "")
                                    {
                                        ' Grab out the property, and assign a default value for the datatype
                                        private string sPropKey = .Token.Split(","c)(1).Replace("]%", "");
                                        if (Adventure.htblAllProperties.ContainsKey(sPropKey))
                                        {
                                            private clsProperty prop = Adventure.htblAllProperties(sPropKey);
                                            switch (prop.Type)
                                            {
                                                case clsProperty.PropertyTypeEnum.Integer:
                                                    {
                                                    .Value = "0";
                                                default:
                                                    {
                                                    .Value = "x";
                                            }
                                        }
                                    }
#endif
                                    .Token = "expr";
                                ElseIf sLeft(.Token, 3) = "pin" Then
                                    .Value = ReplaceFunctions(.Token.Substring(4));
                                    .Token = "expr";
                                ElseIf sLeft(.Token, 3) = "pch" Then
                                    .Value = ReplaceFunctions(.Token.Substring(4));
                                    .Token = "expr";
                                ElseIf sLeft(.Token, 3) = "ref" Then
                                    .Value = ReplaceFunctions(.Token.Substring(4));
                                    .Token = "expr";
                                ElseIf sLeft(.Token, 4) = "fun-" Then
                                    .Value = ReplaceFunctions(.Token.Substring(4));
                                    .Token = "expr";
                                Else
                                    throw New Exception("Bad token: " & .Token)
                                }
                        }
                    }
                }
            Next n;

            private TokenType, optoken As TokenType, righttoken As TokenType lefttoken;
            private TokenType, lptoken As TokenType, rptoken As TokenType, functoken As TokenType currtoken;
            private TokenType, right2token As TokenType op2token;
            private bool changed;
            private bool badexp;
            private int run;


            currtoken = Token(GetFirstToken) '0)
            run = 1

            ' Do while more than one token
            while (Not (currtoken.Left = -1 And currtoken.Right = -1))
            {

                changed = False

                ' If at least two tokens
                if (currtoken.Right <> -1)
                {

                    ' If at least three tokens
                    if (Token(currtoken.Right).Right <> -1)
                    {

                        if (Token(Token(currtoken.Right).Right).Right <> -1)
                        {
                            ' Four tokens - for abs(a)
                            functoken = currtoken
                            lptoken = Token(currtoken.Right)
                            lefttoken = Token(lptoken.Right)
                            rptoken = Token(lefttoken.Right)

                            If functoken.Token = "funct" + lptoken.Token = "lp" + _
                                lefttoken.Token = "expr" + rptoken.Token = "rp" Then;
                                switch (functoken.Value)
                                {
                                    case "abs":
                                        {
                                        addtoken("expr", Math.Abs(CInt(Val(lefttoken.Value))).ToString, functoken.Left, rptoken.Right);
                                    case "upr":
                                        {
                                        addtoken("expr", UCase(lefttoken.Value), functoken.Left, rptoken.Right);
                                    case "lwr":
                                        {
                                        addtoken("expr", LCase(lefttoken.Value), functoken.Left, rptoken.Right);
                                    case "ppr":
                                        {
                                        addtoken("expr", ToProper(lefttoken.Value), functoken.Left, rptoken.Right);
                                    case "len":
                                        {
                                        addtoken("expr", lefttoken.Value.Length.ToString, functoken.Left, rptoken.Right);
                                    case "val":
                                        {
                                        addtoken("expr", SafeInt(Val(lefttoken.Value)).ToString, functoken.Left, rptoken.Right);
                                    case "str":
                                        {
                                        addtoken("expr", Str(lefttoken.Value), functoken.Left, rptoken.Right);
                                }
                                If functoken.Left <> -1 Then Token(functoken.Left).Right = iToken - 1
                                If rptoken.Right <> -1 Then Token(rptoken.Right).Left = iToken - 1
                                changed = True
                            }

                            if (Token(Token(Token(currtoken.Right).Right).Right).Right <> -1)
                            {
                                if (Token(Token(Token(Token(currtoken.Right).Right).Right).Right).Right <> -1)
                                {

                                    if (Token(Token(Token(Token(Token(currtoken.Right).Right).Right).Right).Right).Right <> -1)
                                    {
                                        if (Token(Token(Token(Token(Token(Token(currtoken.Right).Right).Right).Right).Right).Right).Right <> -1)
                                        {
                                            ' Eight tokens - for if(test, a, b)
                                            functoken = currtoken
                                            lptoken = Token(currtoken.Right)
                                            lefttoken = Token(lptoken.Right)
                                            optoken = Token(lefttoken.Right)
                                            righttoken = Token(optoken.Right)
                                            op2token = Token(righttoken.Right)
                                            right2token = Token(op2token.Right)
                                            rptoken = Token(right2token.Right)

                                            If functoken.Token = "funct" && lptoken.Token = "lp" && (lefttoken.Token = "test" || lefttoken.Token = "expr") _
                                                && optoken.Token = "comma" && righttoken.Token = "expr" && op2token.Token = "comma" _;
                                                && right2token.Token = "expr" && rptoken.Token = "rp" Then;

                                                switch (functoken.Value)
                                                {
                                                    case "if":
                                                        {
                                                        If Val(lefttoken.Value) > 0 Then ' true
                                                            addtoken("expr", righttoken.Value, functoken.Left, rptoken.Right);
                                                        Else ' false
                                                            addtoken("expr", right2token.Value, functoken.Left, rptoken.Right);
                                                        }
                                                }
                                                If functoken.Left <> -1 Then Token(functoken.Left).Right = iToken - 1
                                                If rptoken.Right <> -1 Then Token(rptoken.Right).Left = iToken - 1
                                                changed = True
                                            }

                                            If functoken.Token = "funct" + lptoken.Token = "lp" + lefttoken.Token = "expr" _
 + optoken.Token = "comma" + righttoken.Token = "expr" + op2token.Token = "comma" _;
 + right2token.Token = "expr" + rptoken.Token = "rp" Then;

                                                switch (functoken.Value)
                                                {
                                                    case "mid":
                                                        {
                                                        addtoken("expr", sMid(lefttoken.Value, CInt(Val(righttoken.Value)), CInt(Val(right2token.Value))), functoken.Left, rptoken.Right);
                                                }
                                                If functoken.Left <> -1 Then Token(functoken.Left).Right = iToken - 1
                                                If rptoken.Right <> -1 Then Token(rptoken.Right).Left = iToken - 1
                                                changed = True
                                            }
                                        }
                                    }

                                    ' Six tokens - for funct(a,b)
                                    functoken = currtoken
                                    lptoken = Token(currtoken.Right)
                                    lefttoken = Token(lptoken.Right)
                                    optoken = Token(lefttoken.Right)
                                    righttoken = Token(optoken.Right)
                                    rptoken = Token(righttoken.Right)

                                    If functoken.Token = "funct" + lptoken.Token = "lp" + lefttoken.Token = "expr" _
 + optoken.Token = "comma" + righttoken.Token = "expr" + rptoken.Token = "rp" Then;
                                        switch (functoken.Value)
                                        {
                                            case "max":
                                                {
                                                addtoken("expr", Math.Max(CInt(Val(lefttoken.Value)), CInt(Val(righttoken.Value))).ToString, functoken.Left, rptoken.Right);
                                            case "min":
                                                {
                                                addtoken("expr", Math.Min(CInt(Val(lefttoken.Value)), CInt(Val(righttoken.Value))).ToString, functoken.Left, rptoken.Right);
                                            case "either":
                                                {
                                                if (Random(1) = 1 Then ' Int(Rnd() * 2) = 1)
                                                {
                                                    addtoken("expr", lefttoken.Value, functoken.Left, rptoken.Right);
                                                Else
                                                    addtoken("expr", righttoken.Value, functoken.Left, rptoken.Right);
                                                }
                                            case "rand":
                                                {
                                                addtoken("expr", Random(SafeInt(lefttoken.Value), SafeInt(righttoken.Value)).ToString, functoken.Left, rptoken.Right) ' CInt((Rnd() * (Val(righttoken.Value) - Val(lefttoken.Value))) + Val(lefttoken.Value)).ToString;
                                            case "lft":
                                                {
                                                addtoken("expr", sLeft(lefttoken.Value, CInt(Val(righttoken.Value))), functoken.Left, rptoken.Right);
                                            case "rgt":
                                                {
                                                addtoken("expr", sRight(lefttoken.Value, CInt(Val(righttoken.Value))), functoken.Left, rptoken.Right);
                                            case "ist":
                                                {
                                                addtoken("expr", sInstr(lefttoken.Value, righttoken.Value).ToString, functoken.Left, rptoken.Right);
                                        }
                                        If functoken.Left <> -1 Then Token(functoken.Left).Right = iToken - 1
                                        If rptoken.Right <> -1 Then Token(rptoken.Right).Left = iToken - 1
                                        changed = True
                                    }

                                }
                            }
                        }

                        lefttoken = currtoken
                        optoken = Token(currtoken.Right)
                        righttoken = Token(optoken.Right)

                        'Debug.Print lefttoken.token & "[" & lefttoken.value & "]   " & _
                        '     optoken.token & "[" & optoken.value & "]   " & _
                        '     righttoken.token & "[" & righttoken.value & "]"

                        ' expr op expr
                        if (lefttoken.Token = "expr" And optoken.Token = "op" And righttoken.Token = "expr")
                        {
                            switch (optoken.Value)
                            {
                                case "^":
                                    {
                                    addtoken("expr", (Val(lefttoken.Value) ^ Val(righttoken.Value)).ToString, lefttoken.Left, righttoken.Right);
                                case "*":
                                    {
                                    addtoken("expr", (Val(lefttoken.Value) * Val(righttoken.Value)).ToString, lefttoken.Left, righttoken.Right);
                                case "/":
                                    {
                                    'addtoken("expr", (Math.Round((Val(lefttoken.Value) / Val(righttoken.Value)) + 0.000001)).ToString, lefttoken.Left, righttoken.Right)
                                    addtoken("expr", (Math.Round((Val(lefttoken.Value) / Val(righttoken.Value)), MidpointRounding.AwayFromZero)).ToString, lefttoken.Left, righttoken.Right);
                                    ' Val(lefttoken.value) / Val(righttoken.value)
                                case "+":
                                    {
                                    if (IsNumeric(lefttoken.Value) And IsNumeric(righttoken.Value))
                                    {
                                        If run = 2 Then addtoken("expr", (Val(lefttoken.Value) + Val(righttoken.Value)).ToString, lefttoken.Left, righttoken.Right)
                                    Else
                                        addtoken("expr", lefttoken.Value + righttoken.Value, lefttoken.Left, righttoken.Right);
                                        run = 2
                                    }
                                case "-":
                                    {
                                    If run = 2 Then addtoken("expr", (Val(lefttoken.Value) - Val(righttoken.Value)).ToString, lefttoken.Left, righttoken.Right)
                                case "mod":
                                    {
                                    If run = 2 Then addtoken("expr", (Val(lefttoken.Value) Mod Val(righttoken.Value)).ToString, lefttoken.Left, righttoken.Right)

                            }
                            if (run = 2 Or optoken.Value = "*" Or optoken.Value = "/")
                            {
                                If lefttoken.Left <> -1 Then Token(lefttoken.Left).Right = iToken - 1
                                If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                                changed = True
                            }
                        }

                        if (lefttoken.Token = "expr" And optoken.Value = "AND" And righttoken.Token = "expr" And run = 2)
                        {
                            addtoken("expr", lefttoken.Value + righttoken.Value, lefttoken.Left, righttoken.Right);
                            If lefttoken.Left <> -1 Then Token(lefttoken.Left).Right = iToken - 1
                            If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                            changed = True
                        }

                        if (lefttoken.Token = "test" And optoken.Token = "LOGIC" And righttoken.Token = "test")
                        {
                            switch (optoken.Value)
                            {
                                case "AND":
                                    {
                                    if (Val(lefttoken.Value) > 0 And Val(righttoken.Value) > 0)
                                    {
                                        addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                    Else
                                        addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                    }
                                case "OR":
                                    {
                                    if (Val(lefttoken.Value) > 0 Or Val(righttoken.Value) > 0)
                                    {
                                        addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                    Else
                                        addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                    }
                            }
                            If lefttoken.Left <> -1 Then Token(lefttoken.Left).Right = iToken - 1
                            If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                            changed = True
                        }

                        ' expr TESTOP expr
                        ' Do this on run 3 in case we have expr TESTOP expr OP expr, to ensure the expr OP expr reduces to expr first
                        if (lefttoken.Token = "expr" And optoken.Token = "TESTOP" And righttoken.Token = "expr" And run = 3)
                        {
                            switch (optoken.Value)
                            {
                                case "EQ":
                                    {
                                    if (IsNumeric(lefttoken.Value) && IsNumeric(righttoken.Value))
                                    {
                                        if (Val(lefttoken.Value) = Val(righttoken.Value))
                                        {
                                            addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                        Else
                                            addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                        }
                                    Else
                                        if (lefttoken.Value = righttoken.Value)
                                        {
                                            addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                        Else
                                            addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                        }
                                    }
                                case "NE":
                                    {
                                    if (IsNumeric(lefttoken.Value) && IsNumeric(righttoken.Value))
                                    {
                                        if (Val(lefttoken.Value) <> Val(righttoken.Value))
                                        {
                                            addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                        Else
                                            addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                        }
                                    Else
                                        if (lefttoken.Value <> righttoken.Value)
                                        {
                                            addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                        Else
                                            addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                        }
                                    }
                                case "GT":
                                    {
                                    if (Val(lefttoken.Value) > Val(righttoken.Value))
                                    {
                                        addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                    Else
                                        addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                    }
                                case "LT":
                                    {
                                    if (Val(lefttoken.Value) < Val(righttoken.Value))
                                    {
                                        addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                    Else
                                        addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                    }
                                case "GE":
                                    {
                                    if (Val(lefttoken.Value) >= Val(righttoken.Value))
                                    {
                                        addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                    Else
                                        addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                    }
                                case "LE":
                                    {
                                    if (Val(lefttoken.Value) <= Val(righttoken.Value))
                                    {
                                        addtoken("test", "1", lefttoken.Left, righttoken.Right);
                                    Else
                                        addtoken("test", "0", lefttoken.Left, righttoken.Right);
                                    }
                            }
                            If lefttoken.Left <> -1 Then Token(lefttoken.Left).Right = iToken - 1
                            If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                            changed = True
                        }

                        ' ( expr )
                        if (lefttoken.Token = "lp" And optoken.Token = "expr" And righttoken.Token = "rp")
                        {
                            if (IsNumeric(optoken.Value))
                            {
                                addtoken("expr", Val(optoken.Value).ToString, lefttoken.Left, righttoken.Right);
                            Else
                                addtoken("expr", optoken.Value, lefttoken.Left, righttoken.Right);
                            }

                            If lefttoken.Left <> -1 Then Token(lefttoken.Left).Right = iToken - 1
                            If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                            changed = True
                        }

                        ' ( test )
                        if (lefttoken.Token = "lp" And optoken.Token = "test" And righttoken.Token = "rp")
                        {
                            if (IsNumeric(optoken.Value))
                            {
                                addtoken("test", Val(optoken.Value).ToString, lefttoken.Left, righttoken.Right);
                            Else
                                addtoken("test", optoken.Value, lefttoken.Left, righttoken.Right);
                            }

                            If lefttoken.Left <> -1 Then Token(lefttoken.Left).Right = iToken - 1
                            If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                            changed = True
                        }

                    }

                    ' Only two tokens
                    optoken = currtoken
                    righttoken = Token(optoken.Right)

                    'Debug.Print optoken.token & "[" & optoken.value & "]   " & _
                    '    righttoken.token & "[" & righttoken.value & "]"

                    ' op expr
                    if (optoken.Token = "op" And righttoken.Token = "expr")
                    {
                        switch (optoken.Value)
                        {
                            case "-":
                                {
                                If run = 2 Then addtoken("expr", (-Val(righttoken.Value)).ToString, optoken.Left, righttoken.Right)
                        }
                        if (run = 2)
                        {
                            If optoken.Left <> -1 Then Token(optoken.Left).Right = iToken - 1
                            If righttoken.Right <> -1 Then Token(righttoken.Right).Left = iToken - 1
                            changed = True
                        }

                    }

                }

                if (changed)
                {
                    ' Move pointer to far left
                    currtoken = Token(iToken - 1)
                    while (currtoken.Left <> -1)
                    {
                        currtoken = Token(currtoken.Left)
                    }
                    badexp = False
                    run = 1
                Else
                    if (currtoken.Right = -1)
                    {
                        if (badexp)
                        {
                            if (run = 1)
                            {
                                run = 2
                                currtoken = Token(iToken - 1)
                                while (currtoken.Left <> -1)
                                {
                                    currtoken = Token(currtoken.Left)
                                }
                                badexp = False
                                GoTo nxt;
                            ElseIf run = 2 Then
                                run = 3
                                currtoken = Token(iToken - 1)
                                while (currtoken.Left <> -1)
                                {
                                    currtoken = Token(currtoken.Left)
                                }
                                badexp = False
                                GoTo nxt;
                            Else
                                if (bThrowExceptionOnBadExpression)
                                {
                                    throw New Exception("Bad expression: " & sExpression)
                                Else
                                    ErrMsg("Bad Expression: " + sExpression);
                                }
                                Exit Sub;
                            }
                        }
                        badexp = True
                    Else
                        ' Move token one to right
                        currtoken = Token(currtoken.Right)
                    }
                }

nxt:;
            }

            if (Type = VariableTypeEnum.Numeric)
            {
                IntValue(iIndex) = CInt(Val(Token(iToken - 1).Value));
                'StringValue(iIndex) = IntValue(iIndex).ToString ' Used to evaluate datatype
                ' But we can't set indexed string value because it won't exist, only the int array...
            Else
                StringValue(iIndex) = Token(iToken - 1).Value;
            }

        }
        catch (Exception ex)
        {
            If bThrowExceptionOnBadExpression Then Throw New Exception("Error in SetToExpression for variable " + Me.Name, ex)
        }

    }

    private void addtoken(string tokenv, string value, int left, int right)
    {

        ReDim Preserve Token(iToken + 1);

        With Token(iToken);
            .Token = tokenv;
            .Value = value;
            .Left = left;
            .Right = right;
        }
        iToken = iToken + 1

    }


    private string StripRedundantSpaces(string strExpression)
    {

        private bool bolOkToStrip;
        bolOkToStrip = True
        private string strChunk;
        private int intPos;


        StripRedundantSpaces = ""

        while (strExpression <> "")
        {
            intPos = sInstr(1, strExpression, Chr(34))
            if (intPos > 0)
            {
                strChunk = sLeft(strExpression, intPos)
                strExpression = sRight(strExpression, Len(strExpression) - intPos)
                If bolOkToStrip Then strChunk = Replace(strChunk, " ", "").Replace(vbLf, "")
                bolOkToStrip = Not bolOkToStrip
            Else
                strChunk = strExpression
                If bolOkToStrip Then strChunk = Replace(strChunk, " ", "").Replace(vbLf, "")
                strExpression = ""
            }

            Debug.Print("Chunk: " + strChunk + " - " + bolOkToStrip);
            StripRedundantSpaces = StripRedundantSpaces & strChunk
        }

    }


#End Region

    Public Overrides ReadOnly Property CommonName() As String
        {
            get
            {
            return Name;
        }
    }


    Friend Overrides ReadOnly Property AllDescriptions() As System.Collections.Generic.List(Of SharedModule.Description);
        {
            get
            {
            private New Generic.List<Description> all;
            return all;
        }
    }


    internal override object FindStringLocal(string sSearchString, string sReplace = null, bool bFindAll = true, ref iReplacements As Integer = 0)
    {
        private int iCount = iReplacements;
        iReplacements += MyBase.FindStringInStringProperty(Me.sName, sSearchString, sReplace, bFindAll);
        return iReplacements - iCount;
    }


    public override void EditItem()
    {
#if Generator
        private New frmVariable(Me, True) fVariable;
#endif
    }


    public override int ReferencesKey(string sKey)
    {

        private int iCount = 0;
        For Each d As Description In AllDescriptions
            iCount += d.ReferencesKey(sKey);
        Next;
        If Adventure.htblVariables.ContainsKey(sKey) && Me.StringValue.Contains("%" + Adventure.htblVariables(sKey).Name + "%") Then iCount += 1

        return iCount;

    }


    public override bool DeleteKey(string sKey)
    {

        For Each d As Description In AllDescriptions
            If ! d.DeleteKey(sKey) Then Return false
        Next;
        if (Adventure.htblVariables.ContainsKey(sKey) && Me.StringValue.Contains("%" + Adventure.htblVariables(sKey).Name + "%"))
        {
            if (Adventure.htblVariables(sKey).Type = VariableTypeEnum.Numeric)
            {
                StringValue = StringValue.Replace("%" & Adventure.htblVariables(sKey).Name & "%", "0")
            Else
                StringValue = StringValue.Replace("%" & Adventure.htblVariables(sKey).Name & "%", """""")
            }
        }

        return true;

    }


}

}