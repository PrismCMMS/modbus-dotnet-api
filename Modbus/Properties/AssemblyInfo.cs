using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Modbus Modern Api")]
[assembly: AssemblyDescription("A Modern api for Modbus protocol")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Daacoworks")]
[assembly: AssemblyProduct("Daacoworks Modbus Modern Api")]
[assembly: AssemblyCopyright("Copyright © Daacoworks 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("6560f0fb-54cd-4c1f-b5be-b81ed8cec7a2")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0")]
[assembly: AssemblyFileVersion("1.0")]
#if TRIAL
[assembly: AssemblyInformationalVersion("1.0 Trial")]
#else
[assembly: AssemblyInformationalVersion("1.0")]
#endif
[assembly: InternalsVisibleTo("ModbusTest")]
