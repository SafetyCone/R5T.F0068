using System;


namespace R5T.F0068
{
    public static class Instances
    {
        public static L0066.IAssemblyOperator AssemblyOperator => L0066.AssemblyOperator.Instance;
        public static L0066.ICharacterOperator CharacterOperator => L0066.CharacterOperator.Instance;
        public static IDirectoryNames DirectoryNames => F0068.DirectoryNames.Instance;
        public static IDirectoryPaths DirectoryPaths => F0068.DirectoryPaths.Instance;
        public static L0066.IEnumerableOperator EnumerableOperator => L0066.EnumerableOperator.Instance;
        public static IFileNames FileNames => F0068.FileNames.Instance;
        public static L0066.IFileOperator FileOperator => L0066.FileOperator.Instance;
        public static Z0015.IFilePaths FilePaths => Z0015.FilePaths.Instance;
        public static L0066.IFileSystemOperator FileSystemOperator => L0066.FileSystemOperator.Instance;
        public static L0066.INamespacedTypeNameOperator NamespacedTypeNameOperator => L0066.NamespacedTypeNameOperator.Instance;
        public static Z0006.INamespacedTypeNames NamespacedTypeNames => Z0006.NamespacedTypeNames.Instance;
        public static INamespaces Namespaces => F0068.Namespaces.Instance;
        public static IOperations Operations => F0068.Operations.Instance;
        public static L0066.IPathOperator PathOperator => L0066.PathOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator => F0068.ProjectPathsOperator.Instance;
        public static F0018.IReflectionOperator ReflectionOperator => F0018.ReflectionOperator.Instance;
        public static F0024.ISolutionFileOperator SolutionFileOperator => F0024.SolutionFileOperator.Instance;
        public static L0066.IStrings Strings => L0066.Strings.Instance;
        public static F0018.ITypeOperator TypeOperator => F0018.TypeOperator.Instance;
    }
}