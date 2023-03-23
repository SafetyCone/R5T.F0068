using System;


namespace R5T.F0068
{
    public static class Instances
    {
        public static F0000.IAssemblyOperator AssemblyOperator => F0000.AssemblyOperator.Instance;
        public static IDirectoryNames DirectoryNames => F0068.DirectoryNames.Instance;
        public static IDirectoryPaths DirectoryPaths => F0068.DirectoryPaths.Instance;
        public static F0000.IEnumerableOperator EnumerableOperator => F0000.EnumerableOperator.Instance;
        public static IFileNames FileNames => F0068.FileNames.Instance;
        public static F0000.IFileOperator FileOperator => F0000.FileOperator.Instance;
        public static Z0015.IFilePaths FilePaths => Z0015.FilePaths.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator => F0000.FileSystemOperator.Instance;
        public static Z0006.INamespacedTypeNames NamespacedTypeNames => Z0006.NamespacedTypeNames.Instance;
        public static INamespaces Namespaces => F0068.Namespaces.Instance;
        public static IOperations Operations => F0068.Operations.Instance;
        public static F0002.IPathOperator PathOperator => F0002.PathOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator => F0068.ProjectPathsOperator.Instance;
        public static F0018.IReflectionOperator ReflectionOperator => F0018.ReflectionOperator.Instance;
        public static F0024.ISolutionFileOperator SolutionFileOperator => F0024.SolutionFileOperator.Instance;
        public static F0018.ITypeOperator TypeOperator => F0018.TypeOperator.Instance;
    }
}