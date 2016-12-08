# Utils
Reusable utilities for other NuGet packages or products.
[TBA]

# Utils.ForTesting
[TBA]

# Utils.MSBuild

### General
| Spec | Value | 
| --------- | --------- | 
| Package | DavidLievrouw.MSBuildTasks | 
| Initial release date | TBA | 

### Tasks
* GetVersionParts
  * This task accepts a Major.Minor.Build.Revision version number string, and splits it up into four integer properties. 
    * Input properties
      * [VersionNumber] - A valid version number string, that consists of (major).(minor).(build).(revision). Internally System.Version.Parse(string) is used to validate the input.
    * Output properties
      * [MajorVersion] - An integer that contains the Major version number.
      * [MinorVersion] - An integer that contains the Minor version number.
      * [BuildVersion] - An integer that contains the Build version number.
      * [RevisionVersion] - An integer that contains the Revision version number.
* EncryptForLocalMachineScope
  * This task encrypts the specified input string, so that it can only be decrypted on the local machine.
    * Input properties
      * [StringToEncrypt] - The string to encrypt.
      * [Purposes] - An optional array of strings. These represent purposes for the data. The same purposes have to be passed to decrypt the data, later on. Empty, whitespace or null purposes are not used.
    * Output properties
      * [EncryptedString] - The BASE64 string that represents the encrypted data.
* DecryptForLocalMachineScope
  * This task decrypts the specified input string, that was encrypted on the local machine.
    * Input properties
      * [StringToDecrypt] - The BASE64 cypher to decrypt.
      * [Purposes] - An optional array of strings. These represent purposes for the data. These should be the same purposes that were passed to encrypt the data, earlier. Empty, whitespace or null purposes are not used.
    * Output properties
      * [DecryptedString] - The original decrypted string.

### Installation instructions
* Get the latest version at [Nuget.org](https://www.nuget.org/packages/DavidLievrouw.Utils.MSBuild/).
* Install by executing:
```sh
PM> Install-Package DavidLievrouw.Utils.MSBuild
```
* Include the .targets file in your MSBuild scripts:
```xml
<Import Project="$(DLMSBuildTasksPath)\DavidLievrouw.Utils.MSBuild.Tasks.targets"/>
```
* Use any of the included tasks in your MSBuild scripts, e.g.:
```xml
<Target Name="MyTarget">
  <PropertyGroup>
    <VersionNumber>12.2.1.31255</VersionNumber>
  </PropertyGroup>
  <GetVersionParts VersionNumber="$(VersionNumber)">
    <Output TaskParameter="MajorVersion" PropertyName="Major" />
    <Output TaskParameter="MinorVersion" PropertyName="Minor" />
    <Output TaskParameter="BuildVersion" PropertyName="Build" />
    <Output TaskParameter="RevisionVersion" PropertyName="Revision" />
  </GetVersionParts>
  <Message Text="$(VersionNumber) consists of $(Major)-$(Minor)-$(Build)-$(Revision)." />
</Target>
```

### License
MIT
