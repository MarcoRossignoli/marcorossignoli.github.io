using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Console;
namespace Playground
{
    public class SyntaxTransformation
    {
        public static void Demo()
        {
            NameSyntax name = SyntaxFactory.IdentifierName("System");
            // WriteLine($"\tCreated the identifier {name}");

            name = SyntaxFactory.QualifiedName(name, SyntaxFactory.IdentifierName("Collections"));
            // WriteLine(name.ToString());

            name = SyntaxFactory.QualifiedName(name, SyntaxFactory.IdentifierName("Generic"));
            // WriteLine(name.ToString());

            SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(Path.Combine(Helper.GetSolutionRoot(), @"Sample\\Program.cs")));
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            UsingDirectiveSyntax oldUsing = root.Usings[1];
            UsingDirectiveSyntax newUsing = oldUsing.WithName(name);
            WriteLine(root.ToString());

            root = root.ReplaceNode(oldUsing, newUsing);
            WriteLine(root.ToString());
        }
    }
}
