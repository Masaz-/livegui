using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("livegui")]
[assembly: AssemblyDescription("GUI launcher for Livestreamer.\n\nLivestreamer ©Copyright 2011-2015 Christopher Rosell\n\nChange log:\n\n2.0.1 - 2015-10-13\n - Fixed bugs\n\n\n2.0.0 - 2015-09-24\n - Scheduler activation makes sense now\n - Options menu\n - Close launcher option moved to Options\n - livestreamer location option moved to Options\n - All sites supported by Livestreamer are now supported\n - Custom quality text\n - Edit window for Saved URLs, Plugins and Qualities\n - Custom launch options\n - New icon\n\n1.7.0 - 2015-08-09\n - Scheduler has now action options (alert and start stream)\n\n1.6.0 - 2015-07-12\n - Scheduler\n\n1.5.0 - 2015-06-27\n - Save launched URLs\n\n1.4.0 - 2015-02-20\n - Option to keep the launcher open")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("livegui")]
[assembly: AssemblyCopyright("Copyright ©2015 Martti Natunen")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
)]


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
[assembly: AssemblyVersion("2.0.1.0")]
[assembly: AssemblyFileVersion("2.0.1.0")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]
