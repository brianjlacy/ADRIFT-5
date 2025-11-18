using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace ADRIFT
{

' General Information about an assembly is controlled through the following
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

#if Generator
<Assembly: AssemblyTitle("ADRIFT Developer")>;
<Assembly: AssemblyProduct("ADRIFT Developer")>;
#else
<Assembly: AssemblyTitle("ADRIFT Runner")>;
<Assembly: AssemblyProduct("ADRIFT Runner")>;
#endif
<Assembly: AssemblyDescription("Interactive Fiction Development System for Windows")>;
<Assembly: AssemblyCompany("Campbell Wild")>;
<Assembly: AssemblyCopyright("© Campbell Wild 1998-2022")>;
<Assembly: AssemblyTrademark("")>;
<Assembly: CLSCompliant(true)>;

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("64B7CFEE-FB09-4F21-9205-EAD65A7208B7")>;

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers
' by using the '*' as shown below:

<Assembly: AssemblyVersion("5.0.36.6")>;

}