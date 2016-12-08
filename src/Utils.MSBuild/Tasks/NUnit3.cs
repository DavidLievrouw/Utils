using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Win32;

namespace DavidLievrouw.Utils.MSBuild.Tasks {
  /// <summary>
  /// Run NUnit 3.x on a group of assemblies.
  /// </summary>
  /// <example>Run NUnit tests.
  /// <code><![CDATA[
  /// <ItemGroup>
  ///     <TestAssembly Include="C:\Program Files\NUnit.org\*.tests.dll" />
  /// </ItemGroup>
  /// <Target Name="NUnit3">
  ///     <NUnit3 Assemblies="@(TestAssembly)" 
  ///             ToolPath="$(NUnitPath)"
  ///             ProjectConfiguration="Debug"
  ///             ContinueOnError="true"
  ///             Process="Multiple" 
  ///	            TestTimeout="1000" 
  ///             Framework="v4.5" 
  ///	            Force32Bit="true" 
  ///	            Workers="10" 
  ///	            EnableShadowCopy="true" 
  ///	            OutputXmlFile="MyTestOutput.xml"
  ///	            WorkingDirectory="./"
  ///	            ShowLabels="All"
  ///	            InternalTrace="Verbose"
  ///	            NoHeader="true"
  ///	            NoColor="true"
  ///	            Verbose="true"
  ///	            ReportProgressToTeamCity="true"
  ///             AdditionalArguments="--teamcity" />
  /// </Target>
  /// ]]></code>
  /// </example>
  public class NUnit3 : ToolTask {
    const string DefaultNunitDirectory = @"NUnit.org\nunit-console";
    const string InstallDirKey = @"HKEY_CURRENT_USER\Software\NUnit.org";

    /// <summary>
    /// Gets or sets the assemblies.
    /// </summary>
    /// <value>The assemblies.</value>
    [Required]
    public ITaskItem[] Assemblies { get; set; }

    /// <summary>
    /// Gets or sets any additional arguments, as a raw string.
    /// </summary>
    /// <value>The additional arguments.</value>
    public string AdditionalArguments { get; set; }

    /// <summary>
    /// Determines whether progress is reported to TeamCity listeners during test execution.
    /// </summary>
    /// <remarks>Progress reporting is disabled by default. If you want to report progress for TeamCity, you must set this property to <c>True</c>.</remarks>
    public bool ReportProgressToTeamCity { get; set; }

    /// <summary>
    /// Gets or sets the output XML file.
    /// </summary>
    /// <value>The output XML file.</value>
    public string OutputXmlFile { get; set; }

    /// <summary>
    /// The file to receive test error details.
    /// </summary>
    public string ErrorOutputFile { get; set; }

    /// <summary>
    /// The file to redirect standard output to.
    /// </summary>
    public string TextOutputFile { get; set; }

    /// <summary>
    /// Gets or sets the working directory.
    /// </summary>
    /// <value>The working directory.</value>
    /// <returns>
    /// The directory in which to run the executable file, or a null reference (Nothing in Visual Basic) if the executable file should be run in the current directory.
    /// </returns>
    public string WorkingDirectory { get; set; }

    /// <summary>
    /// Determines whether assemblies are copied to a shadow folder during testing.
    /// </summary>
    /// <remarks>Shadow copying is disabled by default. If you want to test the assemblies "in the shadow folder",
    /// you must set this property to <c>True</c>.</remarks>
    public bool EnableShadowCopy { get; set; }

    /// <summary>
    /// The project configuration to run.
    /// </summary>
    /// <remarks>Only applies when a project file is used. The default is the first configuration, usually Debug.</remarks>
    public string ProjectConfiguration { get; set; }

    private bool? _testInNewThread = null;

    /// <summary>
    /// Allows tests to be run in a new thread, allowing you to take advantage of ApartmentState and ThreadPriority settings in the config file.
    /// </summary>
    public bool TestInNewThread {
      get { return _testInNewThread ?? true; }
      set { _testInNewThread = value; }
    }

    /// <summary>
    /// Determines whether the tests are run in a 32bit process on a 64bit OS.
    /// </summary>
    public bool Force32Bit { get; set; }

    /// <summary>
    /// Determines the framework to run aganist.
    /// </summary>
    public string Framework { get; set; }

    /// <summary>
    /// Whether or not to show test labels in output.
    /// On - Labels are shown in the output.
    /// Off - Labels are not shown in the output.
    /// All - 
    /// </summary>
    public string ShowLabels { get; set; }

    /// <summary>
    /// The --process option controls how NUnit loads tests in processes. The following values are recognized.
    /// Single - All the tests are run in the nunit-console process. This is the default.
    /// Separate - A separate process is created to run the tests.
    /// Multiple - A separate process is created for each test assembly, whether specified on the command line or listed in an NUnit project file.
    /// Note: This option is not available using the .NET 1.1 build of nunit-console.
    /// </summary>
    public string Process { get; set; }

    /// <summary>
    /// The --domain option controls of the creation of AppDomains for running tests. The following values are recognized:
    /// None - No domain is created - the tests are run in the primary domain. This normally requires copying the NUnit assemblies into the same directory as your tests.
    /// Single - A test domain is created - this is how NUnit worked prior to version 2.4
    /// Multiple - A separate test domain is created for each assembly
    /// The default is to use multiple domains if multiple assemblies are listed on the command line. Otherwise a single domain is used.
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// The --apartment option may be used to specify the ApartmentState (STA or MTA) of the test runner thread. Since the default is MTA, the option is only needed to force execution in the Single Threaded Apartment.
    /// Note: If a given test must always run in a particular apartment, as is the case with many Gui tests, you should use an attribute on the test rather than specifying this option at the command line.
    /// </summary>
    public string Apartment { get; set; }

    /// <summary>
    /// The --where option may be used to specify an expression indicating which tests to run. 
    /// It may specify test names, classes, methods, catgories or properties comparing them to actual values with the operators ==, !=, =~ and !~. 
    /// See NUnit 3.0 Test Selection Language for a full description of the syntax.
    /// </summary>
    /// <remarks></remarks>
    public string Where { get; set; }

    /// <summary>
    /// The --timeout option may be used to specify the timeout for each test case in MILLISECONDS.
    /// </summary>
    /// <remarks></remarks>
    public string TestTimeout { get; set; }

    /// <summary>
    /// The --workers option may be used to specify the number of worker threads to be used in running tests.
    /// </summary>
    /// <remarks></remarks>
    public string Workers { get; set; }

    /// <summary>
    /// The --trace option may be used to specify the internal trace level. 
    /// Off
    /// Error
    /// Warning
    /// Info
    /// Verbose (Debug)
    /// </summary>
    /// <remarks></remarks>
    public string InternalTrace { get; set; }

    /// <summary>
    /// The --noheader suppress display of program information at start of run.
    /// </summary>
    /// <remarks></remarks>
    public bool NoHeader { get; set; }

    /// <summary>
    /// The --nocolor displays console output without color.
    /// </summary>
    /// <remarks></remarks>
    public bool NoColor { get; set; }

    /// <summary>
    /// The --verbose displays additional information as the test runs.
    /// </summary>
    /// <remarks></remarks>
    public bool Verbose { get; set; }

    /// <summary>
    /// Returns a string value containing the command line arguments to pass directly to the executable file.
    /// </summary>
    /// <returns>
    /// A string value containing the command line arguments to pass directly to the executable file.
    /// </returns>
    protected override string GenerateCommandLineCommands() {
      var builder = new CommandLineBuilder();

      var c = Environment.OSVersion.Platform == PlatformID.Unix
        ? "-"
        : "--";

      if (EnableShadowCopy) builder.AppendSwitch(c + "shadowcopy");
      if (_testInNewThread.HasValue && !_testInNewThread.Value) builder.AppendSwitch(c + "nothread");
      if (Force32Bit) builder.AppendSwitch(c + "x86");
      if (NoHeader) builder.AppendSwitch(c + "noheader");
      if (NoColor) builder.AppendSwitch(c + "nocolor");
      if (Verbose) builder.AppendSwitch(c + "verbose");
      if (ReportProgressToTeamCity) builder.AppendSwitch(c + "teamcity");
      builder.AppendFileNamesIfNotNull(Assemblies, " ");
      builder.AppendSwitchIfNotNull(c + "config=", ProjectConfiguration);
      builder.AppendSwitchIfNotNull(c + "err=", ErrorOutputFile);
      builder.AppendSwitchIfNotNull(c + "out=", TextOutputFile);
      builder.AppendSwitchIfNotNull(c + "framework=", Framework);
      builder.AppendSwitchIfNotNull(c + "process=", Process);
      builder.AppendSwitchIfNotNull(c + "domain=", Domain);
      builder.AppendSwitchIfNotNull(c + "apartment=", Apartment);
      builder.AppendSwitchIfNotNull(c + "where=", Where);
      builder.AppendSwitchIfNotNull(c + "timeout=", TestTimeout);
      builder.AppendSwitchIfNotNull(c + "workers=", Workers);
      builder.AppendSwitchIfNotNull(c + "result=", OutputXmlFile);
      builder.AppendSwitchIfNotNull(c + "work=", WorkingDirectory);
      builder.AppendSwitchIfNotNull(c + "labels=", ShowLabels);
      builder.AppendSwitchIfNotNull(c + "trace=", InternalTrace);
      if(!string.IsNullOrWhiteSpace(AdditionalArguments)) builder.AppendTextUnquoted(" " + AdditionalArguments);

      return builder.ToString();
    }

    /// <summary>
    /// Returns the fully qualified path to the executable file.
    /// </summary>
    /// <returns>
    /// The fully qualified path to the executable file.
    /// </returns>
    protected override string GenerateFullPathToTool() {
      CheckToolPath();
      return Path.Combine(ToolPath, ToolName);
    }

    /// <summary>
    /// Logs the starting point of the run to all registered loggers.
    /// </summary>
    /// <param name="message">A descriptive message to provide loggers, usually the command line and switches.</param>
    protected override void LogToolCommand(string message) {
      Log.LogCommandLine(MessageImportance.Low, message);
    }

    /// <summary>
    /// Gets the name of the executable file to run.
    /// </summary>
    /// <value></value>
    /// <returns>The name of the executable file to run.</returns>
    protected override string ToolName => "nunit3-console.exe";

    /// <summary>
    /// Gets the <see cref="T:Microsoft.Build.Framework.MessageImportance"></see> with which to log errors.
    /// </summary>
    /// <value></value>
    /// <returns>The <see cref="T:Microsoft.Build.Framework.MessageImportance"></see> with which to log errors.</returns>
    protected override MessageImportance StandardOutputLoggingImportance => MessageImportance.Normal;

    /// <summary>
    /// Returns the directory in which to run the executable file.
    /// </summary>
    /// <returns>
    /// The directory in which to run the executable file, or a null reference (Nothing in Visual Basic) if the executable file should be run in the current directory.
    /// </returns>
    protected override string GetWorkingDirectory() {
      return string.IsNullOrEmpty(WorkingDirectory)
        ? base.GetWorkingDirectory()
        : WorkingDirectory;
    }

    void CheckToolPath() {
      var nunitPath = ToolPath?.Trim() ?? string.Empty;
      if (!string.IsNullOrEmpty(nunitPath)) {
        ToolPath = nunitPath;
        return;
      }

      nunitPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
      nunitPath = Path.Combine(nunitPath, DefaultNunitDirectory);

      if (Directory.Exists(nunitPath) == false) {
        nunitPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        nunitPath = Path.Combine(nunitPath, DefaultNunitDirectory);
      }

      try {
        var value = Registry.GetValue(InstallDirKey, "InstallDir", nunitPath) as string;
        if (!string.IsNullOrEmpty(value)) nunitPath = Path.Combine(value, "nunit-console");
      } catch (Exception ex) {
        Log.LogErrorFromException(ex);
      }
      ToolPath = nunitPath;
    }
  }
}