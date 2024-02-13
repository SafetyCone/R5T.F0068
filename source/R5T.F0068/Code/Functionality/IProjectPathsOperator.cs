using System;

using R5T.T0132;


namespace R5T.F0068
{
	[FunctionalityMarker]
	public partial interface IProjectPathsOperator : IFunctionalityMarker,
		F0040.IProjectPathsOperator,
		F0052.IProjectPathsOperator
	{
		public string GetExtensionsDirectoryPath(string projectFilePath)
        {
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var extensionsDirectoryPath = Instances.PathOperator.Get_DirectoryPath(
				codeDirectoryPath,
				Instances.DirectoryNames.Extensions);

			return extensionsDirectoryPath;
		}

		public string GetFunctionalityDirectoryPath(string projectFilePath)
        {
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var functionalityDirectoryPath = Instances.PathOperator.Get_DirectoryPath(
				codeDirectoryPath,
				Instances.DirectoryNames.Functionality);

			return functionalityDirectoryPath;
        }

		public string GetGeneratedIServiceCollectionExtensionsCodeFilePath(string projectFilePath)
		{
			var extensionsDirectoryPath = this.GetExtensionsDirectoryPath(projectFilePath);

			var generatedIServiceCollectionExtensionsOperatorCodeFilePath = Instances.PathOperator.Get_FilePath(
				extensionsDirectoryPath,
				Instances.FileNames.GeneratedIServiceCollectionExtensions);

			return generatedIServiceCollectionExtensionsOperatorCodeFilePath;
		}

		public string GetGeneratedIServiceActionOperatorCodeFilePath(string projectFilePath)
        {
			var functionalityDirectoryPath = this.GetFunctionalityDirectoryPath(projectFilePath);

			var generatedIServiceActionOperatorCodeFilePath = Instances.PathOperator.Get_FilePath(
				functionalityDirectoryPath,
				Instances.FileNames.GeneratedIServiceActionOperator);

			return generatedIServiceActionOperatorCodeFilePath;
        }
	}
}