using System;

using R5T.T0131;


namespace R5T.F0068
{
	[DraftValuesMarker]
	public partial interface IDirectoryPaths : IDraftValuesMarker
	{
		/// <summary>
		/// Also see:
		///	* R5T.S0046.IDirectoryPaths.NuGetAssemblies
		/// * R5T.S0041.IDirectoryPaths.NuGetAssemblies
		/// </summary>
		public string NuGetAssemblies => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries\Nuget Assemblies\";
	}
}