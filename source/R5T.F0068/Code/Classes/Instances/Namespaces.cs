using System;


namespace R5T.F0068
{
	public class Namespaces : INamespaces
	{
		#region Infrastructure

	    public static INamespaces Instance { get; } = new Namespaces();

	    private Namespaces()
	    {
        }

	    #endregion
	}
}