using System;

using R5T.F0000;
using R5T.F0002;
using R5T.F0018;
using R5T.F0024;
using R5T.Z0006;
using R5T.Z0015;


namespace R5T.F0068
{
    public static class Instances
    {
        public static IAssemblyOperator AssemblyOperator { get; } = F0000.AssemblyOperator.Instance;
        public static IDirectoryNames DirectoryNames { get; } = F0068.DirectoryNames.Instance;
        public static IDirectoryPaths DirectoryPaths { get; } = F0068.DirectoryPaths.Instance;
        public static IEnumerableOperator EnumerableOperator { get; } = F0000.EnumerableOperator.Instance;
        public static IFileNames FileNames { get; } = F0068.FileNames.Instance;
        public static IFileOperator FileOperator { get; } = F0000.FileOperator.Instance;
        public static IFilePaths FilePaths { get; } = Z0015.FilePaths.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static INamespacedTypeNames NamespacedTypeNames { get; } = Z0006.NamespacedTypeNames.Instance;
        public static INamespaces Namespaces { get; } = F0068.Namespaces.Instance;
        public static IOperations Operations { get; } = F0068.Operations.Instance;
        public static F0002.IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = F0068.ProjectPathsOperator.Instance;
        public static IReflectionOperator ReflectionOperator { get; } = F0018.ReflectionOperator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = F0024.SolutionFileOperator.Instance;
        public static F0018.ITypeOperator TypeOperator { get; } = F0018.TypeOperator.Instance;
    }
}