using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;
using R5T.T0149;


namespace R5T.F0068
{
	[FunctionalityMarker]
	public partial interface IOperations : IFunctionalityMarker
	{
		public string[] GetIServiceActionOperatorInterfaceLines(IEnumerable<ServiceImplementationInformation> serviceImplementations)
		{
			var lines = new[]
			{
				"\t[FunctionalityMarker]",
				"\tpublic partial interface IServiceActionOperator : IFunctionalityMarker,",
				"\t\tR5T.T0147.IServiceActionOperator",
				"\t{"
			}
			.AppendRange(this.GetIServiceActionOperatorInterfaceBodyLines(serviceImplementations))
			.Append("\t}")
			.Now();

			return lines;
		}

		public string[] GetIServiceActionOperatorInterfaceBodyLines(IEnumerable<ServiceImplementationInformation> serviceImplementations)
		{
			var lines = serviceImplementations
				.SelectMany(implementation =>
				{
					var implementationTypeName = Instances.NamespacedTypeNameOperator.Get_TypeName(implementation.ImplementationNamespacedTypeName);
					var definitionTypeName = Instances.NamespacedTypeNameOperator.Get_TypeName(implementation.DefinitionNamespacedTypeName);

					var documentationLines = this.GetDocumentationLines(
						implementationTypeName,
						definitionTypeName);

					var dependencyServiceTypeNamesByVariableNames = this.GetServiceTypeNamesByVariableNames(
						implementation.DependencyDefinitionNamespacedTypeNames);

					var signatureLines = Instances.EnumerableOperator.From($"public IServiceAction<{definitionTypeName}> Add{implementationTypeName}Action(")
						.AppendRange(dependencyServiceTypeNamesByVariableNames
							.Select(pair =>
							{
								var line = $"IServiceAction<{pair.Value}> {pair.Key},";
								return line;
							})
							.Select(x => $"\t{x}"))
						.Now();

					if (dependencyServiceTypeNamesByVariableNames.Any())
					{
						signatureLines[^1] = signatureLines[^1][..^1];
					}
					signatureLines[^1] = signatureLines[^1] + ")";

					var bodyLines = new[]
					{
							"{",
							$"\tvar serviceAction = this.New<{definitionTypeName}>(services => services.Add{implementationTypeName}(",
					}
					.AppendRange(dependencyServiceTypeNamesByVariableNames
						.Select(x => $"\t\t{x.Key},"))
					.Now();

					if (dependencyServiceTypeNamesByVariableNames.Any())
					{
						bodyLines[^1] = bodyLines[^1][..^1];
					}
					bodyLines[^1] = bodyLines[^1] + "));";

					var allBodyLines = bodyLines
						.AppendRange(new[]
						{
                                Instances.Strings.Empty,
								"\treturn serviceAction;",
								"}"
						})
						.Now();

					var output = Instances.EnumerableOperator.Empty<string>()
						.Append(documentationLines)
						.Append(signatureLines)
						.Append(allBodyLines)
						.Now();

					return output;
				})
				.Select(x => $"\t\t{x}")
				.Now();

			return lines;
		}

		public string[] GetIServiceCollectionExtensionsClassLines(IEnumerable<ServiceImplementationInformation> serviceImplementations)
		{
			var lines = new[]
			{
					"\tpublic static partial class IServiceCollectionExtensions",
					"\t{"
				}
			.AppendRange(this.GetIServiceCollectionExtensionsClassBodyLines(serviceImplementations))
			.Append("\t}")
			.Now();

			return lines;
		}

		public string[] GetDocumentationLines(
			string implementationTypeName,
			string definitionTypeName)
		{
			var documentationLines = new[]
			{
					"/// <summary>",
					$"/// Adds the <see cref=\"{implementationTypeName}\"/> implementation of <see cref=\"{definitionTypeName}\"/> as a <see cref=\"ServiceLifetime.Singleton\"/>.",
					"/// </summary>"
			};

			return documentationLines;
		}

		public Dictionary<string, string> GetServiceTypeNamesByVariableNames(IEnumerable<string> serviceDefinitionNamespacedTypeNames)
		{
			var serviceTypeNamesByVariableNames = serviceDefinitionNamespacedTypeNames
				.Select(dependencyDefinitionNamespacedTypeName =>
				{
					var dependencyDefinitionTypeName = Instances.NamespacedTypeNameOperator.Get_TypeName(dependencyDefinitionNamespacedTypeName);

					var nonInterfaceTypeName = dependencyDefinitionTypeName[1..]; // Skip the first 'I'.
					var variableName = Instances.CharacterOperator.ToLower(nonInterfaceTypeName[0]) + nonInterfaceTypeName[1..] + "Action";

					return (variableName, dependencyDefinitionTypeName);
				})
				.ToDictionary(
					x => x.variableName,
					x => x.dependencyDefinitionTypeName);

			return serviceTypeNamesByVariableNames;
		}

		public string[] GetIServiceCollectionExtensionsClassBodyLines(IEnumerable<ServiceImplementationInformation> serviceImplementations)
		{
			var lines = serviceImplementations
				.SelectMany(implementation =>
				{
					var implementationTypeName = Instances.NamespacedTypeNameOperator.Get_TypeName(implementation.ImplementationNamespacedTypeName);
					var definitionTypeName = Instances.NamespacedTypeNameOperator.Get_TypeName(implementation.DefinitionNamespacedTypeName);

					var documentationLines = this.GetDocumentationLines(
						implementationTypeName,
						definitionTypeName);

					var dependencyServiceTypeNamesByVariableNames = this.GetServiceTypeNamesByVariableNames(
						implementation.DependencyDefinitionNamespacedTypeNames);

					var signatureLines = Instances.EnumerableOperator.From($"public static IServiceCollection Add{implementationTypeName}(this IServiceCollection services,")
						.AppendRange(dependencyServiceTypeNamesByVariableNames
							.Select(pair =>
							{
								var line = $"IServiceAction<{pair.Value}> {pair.Key},";
								return line;
							})
							.Select(x => $"\t{x}"))
						.Now();

					signatureLines[^1] = signatureLines[^1][..^1] + ")";

					var bodyLines = new[]
					{
							"{",
							"\tservices",
					}
					.AppendRange(dependencyServiceTypeNamesByVariableNames
						.Select(x => $"\t\t.Run({x.Key})"))
					.AppendRange(new[]
					{
							$"\t\t.AddSingleton<{definitionTypeName}, {implementationTypeName}>();",
                            Instances.Strings.Empty,
							"\treturn services;",
							"}"
					})
					.Now();

					var output = Instances.EnumerableOperator.Empty<string>()
						.Append(documentationLines)
						.Append(signatureLines)
						.Append(bodyLines)
						.Now();

					return output;
				})
				.Select(x => $"\t\t{x}")
				.Now();

			return lines;
		}

		public string[] GetAllRequiredNamespaceNames(
			string projectNamespaceName,
			IEnumerable<ServiceImplementationInformation> serviceImplementations,
			IEnumerable<string> extraNamespaceNames)
		{
			var allRequiredNamespaceNames = serviceImplementations
				.SelectMany(implementation => Instances.EnumerableOperator.From(
                    Instances.NamespacedTypeNameOperator.Get_NamespaceName(implementation.ImplementationNamespacedTypeName))
					.Append(Instances.NamespacedTypeNameOperator.Get_NamespaceName(implementation.DefinitionNamespacedTypeName))
					.AppendRange(implementation.DependencyDefinitionNamespacedTypeNames
						.Select(value => Instances.NamespacedTypeNameOperator.Get_NamespaceName(value))))
				.AppendRange(extraNamespaceNames)
				.Distinct()
				.Except(projectNamespaceName)
				.OrderAlphabetically()
				.Now();

			return allRequiredNamespaceNames;
		}

		public string[] GetUsingLines(IEnumerable<string> allRequiredNamespaceNames)
		{
			var usingLines = new[]
			{
					"using System;",
                    Instances.Strings.Empty,
					"using Microsoft.Extensions.DependencyInjection;",
                    Instances.Strings.Empty,
				}
			.AppendRange(allRequiredNamespaceNames
				.Select(namespaceName => $"using {namespaceName};"))
			.Now();

			return usingLines;
		}

		public string[] GetUsingLines(
			string projectNamespaceName,
			IEnumerable<ServiceImplementationInformation> serviceImplementations,
			IEnumerable<string> extraNamespaceNames)
		{
			var allRequiredNamespaceNames = this.GetAllRequiredNamespaceNames(
				projectNamespaceName,
				serviceImplementations,
				extraNamespaceNames);

			var usingLines = this.GetUsingLines(allRequiredNamespaceNames);
			return usingLines;
		}
	}
}