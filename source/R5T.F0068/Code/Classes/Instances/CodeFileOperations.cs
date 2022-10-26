using System;


namespace R5T.F0068
{
	public class CodeFileOperations : ICodeFileOperations
	{
		#region Infrastructure

	    public static ICodeFileOperations Instance { get; } = new CodeFileOperations();

	    private CodeFileOperations()
	    {
        }

	    #endregion
	}
}