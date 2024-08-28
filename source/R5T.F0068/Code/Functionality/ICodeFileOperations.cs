using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;
using R5T.T0149;


namespace R5T.F0068
{
	[FunctionalityMarker]
	public partial interface ICodeFileOperations : IFunctionalityMarker
	{
		public void CreateAddXMethods_ForAllSolutionProjectReferences(string solutionFilePath)
        {
			var projectReferenceFilePaths = Instances.SolutionFileOperator.Get_ProjectReferenceFilePaths(solutionFilePath);

            foreach (var projectReferenceFilePath in projectReferenceFilePaths)
            {
				this.CreateAddXMethods_ForProject(projectReferenceFilePath);
            }
        }

		public void CreateAddXMethods_ForProject(string projectFilePath)
        {
			var assemblyFilePath = Instances.ProjectPathsOperator.GetAssemblyFilePathForProjectFilePath(projectFilePath);

			var serviceImplementations = this.SurveyAssemblyFile(assemblyFilePath);

			// If there are no service implementations, then do nothing in the project.
			if(serviceImplementations.None())
            {
				return;
            }

			var projectNamespaceName = F0020.ProjectFileOperator.Instance.GetDefaultNamespaceName(projectFilePath);

			var iServiceActionOperatorFilePath = Instances.ProjectPathsOperator.GetGeneratedIServiceActionOperatorCodeFilePath(projectFilePath);

			this.CreateAddXActionMethods(
				serviceImplementations,
				projectNamespaceName,
				iServiceActionOperatorFilePath);

			var iServiceCollectionExtensionsFilePath = Instances.ProjectPathsOperator.GetGeneratedIServiceCollectionExtensionsCodeFilePath(projectFilePath);

			this.CreateAddXMethods(
				serviceImplementations,
				projectNamespaceName,
				iServiceCollectionExtensionsFilePath);
		}

		public void CreateAddXActionMethods(
			IEnumerable<ServiceImplementationInformation> serviceImplementations,
			string projectNamespaceName,
			string outputFilePath)
		{
			/// Run.
			var usingLines = Instances.Operations.GetUsingLines(
				projectNamespaceName,
				serviceImplementations,
				new[]
				{
					Instances.Namespaces.R5T_T0132,
					Instances.Namespaces.R5T_T0147,
				});

			var namespaceLines = new[]
			{
				$"namespace {projectNamespaceName}",
				"{",
			}
			.AppendRange(Instances.Operations.GetIServiceActionOperatorInterfaceLines(serviceImplementations))
			.AppendRange(new[]
			{
				"}"
			})
			.Now();

			var lines = usingLines
				.Append(Instances.Strings.Empty)
				.Append(Instances.Strings.Empty)
				.AppendRange(namespaceLines);

			Instances.FileSystemOperator.Ensure_DirectoryExists_ForFilePath(
				outputFilePath);

			Instances.FileOperator.Write_Lines_Synchronous(
				outputFilePath,
				lines);
		}

		public void CreateAddXMethods(
			IEnumerable<ServiceImplementationInformation> serviceImplementations,
			string projectNamespaceName,
			string outputFilePath)
		{
			var usingLines = Instances.Operations.GetUsingLines(
				projectNamespaceName,
				serviceImplementations,
				Instances.EnumerableOperator.From(Instances.Namespaces.R5T_T0147));

			var namespaceLines = new[]
			{
				$"namespace {projectNamespaceName}",
				"{",
			}
			.AppendRange(Instances.Operations.GetIServiceCollectionExtensionsClassLines(serviceImplementations))
			.AppendRange(new[]
			{
				"}"
			})
			.Now();

			var lines = usingLines
				.Append(Instances.Strings.Empty)
				.Append(Instances.Strings.Empty)
				.AppendRange(namespaceLines);

			Instances.FileSystemOperator.Ensure_DirectoryExists_ForFilePath(
				outputFilePath);

			Instances.FileOperator.Write_Lines_Synchronous(
				outputFilePath,
				lines);
		}

		public ServiceImplementationInformation[] SurveyAssemblyFile(string assemblyFilePath)
		{
			var serviceImplementations = new List<ServiceImplementationInformation>();

			var isServiceDefinitionType = Instances.TypeOperator.GetTypeByHasAttributeOfNamespacedTypeNamePredicate(
				Instances.NamespacedTypeNames.ServiceDefinitionMarkerAttribute);
			var isServiceImplementationType = Instances.TypeOperator.GetTypeByHasAttributeOfNamespacedTypeNamePredicate(
				Instances.NamespacedTypeNames.ServiceImplementationMarkerAttribute);

			Instances.ReflectionOperator.InAssemblyContext(
				assemblyFilePath,
				Instances.EnumerableOperator.From(Instances.DirectoryPaths.NuGetAssemblies),
				assembly =>
				{
					Instances.AssemblyOperator.ForTypes(
						assembly,
						isServiceImplementationType,
						typeInfo =>
						{
							var serviceImplementationNamespacedTypeName = Instances.TypeOperator.GetNamespacedTypeName_ForTypeInfo(typeInfo);

							var directlyImplementedInterfaces = Instances.TypeOperator.GetOnlyDirectlyImplementedInterfaces(typeInfo);

							// Use the first.
							var serviceDefinitionNamespacedTypeName = directlyImplementedInterfaces
								.Where(@interface => isServiceDefinitionType(@interface))
								.Select(x => Instances.TypeOperator.Get_NamespacedTypeName(x))
								.First();

							var serviceDefinitionProperties = typeInfo.DeclaredProperties
								.Where(property => isServiceDefinitionType(property.PropertyType))
								.Now();

							var serviceDependencyNamespacedTypeNames = serviceDefinitionProperties
								.Select(property => Instances.TypeOperator.GetNamespacedTypeName_ForType(property.PropertyType))
								.Now();

							var serviceImplementationInformation = new ServiceImplementationInformation
							{
								ImplementationNamespacedTypeName = serviceImplementationNamespacedTypeName,
								DefinitionNamespacedTypeName = serviceDefinitionNamespacedTypeName,
								DependencyDefinitionNamespacedTypeNames = serviceDependencyNamespacedTypeNames,
							};

							serviceImplementations.Add(serviceImplementationInformation);
						});
				});

			return serviceImplementations.ToArray();
		}
	}
}