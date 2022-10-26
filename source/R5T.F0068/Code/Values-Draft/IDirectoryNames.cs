using System;

using R5T.T0131;


namespace R5T.F0068
{
	[DraftValuesMarker]
	public partial interface IDirectoryNames : IDraftValuesMarker
	{
		public string Extensions => "Extensions";
		public string Functionality => "Functionality";
	}
}