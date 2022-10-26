using System;

using R5T.T0131;


namespace R5T.F0068
{
	[ValuesMarker]
	public partial interface IFileNames : IValuesMarker
	{
		public string GeneratedIServiceActionOperator => "IServiceActionOperator-Generated.cs";
		public string GeneratedIServiceCollectionExtensions => "IServiceCollectionExtensions-Generated.cs";
	}
}