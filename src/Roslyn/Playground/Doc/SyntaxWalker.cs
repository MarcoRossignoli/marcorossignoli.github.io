using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Playground
{
    public class CSharpSyntaxWalkerSample
    {
        public static void Demo()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(Path.Combine(Helper.GetSolutionRoot(), "Sample\\Program.cs")));
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var collector = new UsingCollector();
            collector.Visit(root);
            foreach (var directive in collector.Usings)
            {
                Console.WriteLine($"{directive.Name}");
            }
        }


        class UsingCollector : CSharpSyntaxWalker
        {
            public ICollection<UsingDirectiveSyntax> Usings { get; } = new List<UsingDirectiveSyntax>();

            public override void VisitUsingDirective(UsingDirectiveSyntax node)
            {
                WriteLine($"\tVisitUsingDirective called with {node.Name}.");
                if (node.Name.ToString() != "System" &&
                    !node.Name.ToString().StartsWith("System."))
                {
                    WriteLine($"\t\tSuccess. Adding {node.Name}.");
                    this.Usings.Add(node);
                }
            }

            static void WriteLine(string message)
             => Console.WriteLine(message);
        }
    }
}
