using Playground;

// https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/get-started/syntax-transformation

// SyntaxTreeSample.Demo();
// CSharpSyntaxWalkerSample.Demo();
// SemanticAnalysis.Demo();
// SyntaxTransformation.Demo();
// SyntaxRewriters.Demo();
// Console.WriteLine(Helper.GetSolutionRoot());
RemoveParamFromExtensionMethodInvocation.Demo();


public static class Helper
{
    public static string GetSolutionRoot()
    {
        string root = Directory.GetCurrentDirectory();
        while(true)
        {
            if(File.Exists(Path.Combine(root, "Roslyn.sln")))
            {
                return root;
            }

            if (Directory.GetParent(root) == null)
            {
                break;
            }

            root = Directory.GetParent(root).FullName;
        }

        throw new Exception("Solution root not found");
    }
}
