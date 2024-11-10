using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Playground
{
    public class RemoveParamFromExtensionMethodInvocation
    {
        public static void Demo()
        {
            Compilation compilation = CreateTestCompilation();
            Debug.Assert(compilation.SyntaxTrees.Count() == 1);

            foreach (SyntaxTree sourceTree in compilation.SyntaxTrees)
            {
                SemanticModel model = compilation.GetSemanticModel(sourceTree);

                RemoveParameterForAdd rewriter = new(model);

                SyntaxNode newSource = rewriter.Visit(sourceTree.GetRoot());

                if (newSource != sourceTree.GetRoot())
                {
                    // File.WriteAllText(sourceTree.FilePath, newSource.ToFullString());
                    Console.WriteLine(newSource.ToString());
                }
            }

            //SyntaxTree tree = compilation.SyntaxTrees.First();
            //SyntaxNode root = tree.GetRoot();
            //// Console.WriteLine("=== BEFORE ===\n\n" + root.ToString());
            //SemanticModel semanticModel = compilation.GetSemanticModel(tree);

            //IEnumerable<MemberAccessExpressionSyntax> memberAccess =
            //    root.DescendantNodes().OfType<MemberAccessExpressionSyntax>()
            //    .Where(x => x.IsKind(SyntaxKind.SimpleMemberAccessExpression));

            //INamedTypeSymbol integer = compilation.GetTypeByMetadataName("System.Int32")
            //    ?? throw new Exception("System.Int32 not found in the compilation");
            //INamedTypeSymbol? iintType = compilation.GetTypeByMetadataName("Sample.IInt`1")
            //    ?? throw new Exception("Sample.IInt not found in the compilation");
            //INamedTypeSymbol iintTypeOfInt = iintType.Construct(integer);

            //foreach (MemberAccessExpressionSyntax originalMemberAccess in memberAccess)
            //{
            //    if (originalMemberAccess.Parent is not InvocationExpressionSyntax invocationExpressionSyntax)
            //    {
            //        continue;
            //    }

            //    // Identify the extension method call
            //    SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(originalMemberAccess);
            //    if (symbolInfo.Symbol is IMethodSymbol methodSymbol &&
            //        methodSymbol.IsExtensionMethod &&
            //        methodSymbol.Name == "AddExt" &&
            //        methodSymbol.Parameters.Length > 0 &&
            //        SymbolEqualityComparer.Default.Equals(methodSymbol.ReceiverType, iintTypeOfInt))
            //    {
            //        // Console.WriteLine("=== OLD EXPRESSION ===\n\n" + invocationExpressionSyntax.ToString());

            //        // Remove arguments from the invocation
            //        InvocationExpressionSyntax newInvokationExpression = invocationExpressionSyntax
            //            .WithArgumentList(SyntaxFactory.ArgumentList());
            //        newInvokationExpression = newInvokationExpression.WithTriviaFrom(invocationExpressionSyntax);

            //        // Console.WriteLine("=== NEW EXPRESSION ===\n\n" + newInvokationExpression.ToString());

            //        Console.WriteLine("=== BEFORE TO APPLY ===\n\n" + root.ToString());

            //        root = root.ReplaceNode(invocationExpressionSyntax, newInvokationExpression);

            //        Console.WriteLine("=== AFTER ===\n\n" + root.ToString());

            //        Console.Clear();
            //    }
            //}

            //Console.WriteLine("=== AFTER ===\n\n" + root.ToString());
        }

        public class RemoveParameterForAdd : CSharpSyntaxRewriter
        {
            private readonly SemanticModel _semanticModel;
            private readonly INamedTypeSymbol _integer;
            private readonly INamedTypeSymbol? _iintType;
            private readonly INamedTypeSymbol _iintTypeOfInt;

            public RemoveParameterForAdd(SemanticModel semanticModel)
            {
                _integer = semanticModel.Compilation.GetTypeByMetadataName("System.Int32")
                    ?? throw new Exception("System.Int32 not found in the compilation");
                _iintType = semanticModel.Compilation.GetTypeByMetadataName("Sample.IInt`1")
                    ?? throw new Exception("Sample.IInt not found in the compilation");
                _iintTypeOfInt = _iintType.Construct(_integer);
                _semanticModel = semanticModel;
            }

            public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
            {
                if (node.ChildNodes().FirstOrDefault() is MemberAccessExpressionSyntax memberAccessExpressionSyntax && memberAccessExpressionSyntax.IsKind(SyntaxKind.SimpleMemberAccessExpression))
                {
                    SymbolInfo symbolInfo = _semanticModel.GetSymbolInfo(memberAccessExpressionSyntax);
                    if (symbolInfo.Symbol is IMethodSymbol methodSymbol &&
                        methodSymbol.IsExtensionMethod &&
                        methodSymbol.Name == "AddExt" &&
                        methodSymbol.Parameters.Length > 0 &&
                        SymbolEqualityComparer.Default.Equals(methodSymbol.ReceiverType, _iintTypeOfInt))
                    {
                        // Remove arguments from the invocation
                        InvocationExpressionSyntax newInvokationExpression = node
                            .WithArgumentList(SyntaxFactory.ArgumentList());
                        newInvokationExpression = newInvokationExpression.WithTriviaFrom(node);
                        return newInvokationExpression;
                    }
                }

                return base.VisitInvocationExpression(node);
            }
        }


        private static Compilation CreateTestCompilation()
        {
            String programPath = Path.Combine(Helper.GetSolutionRoot(), @"Sample\Sample.cs");
            String programText = File.ReadAllText(programPath);
            SyntaxTree programTree =
                           CSharpSyntaxTree.ParseText(programText)
                                           .WithFilePath(programPath);

            SyntaxTree[] sourceTrees = { programTree };

            MetadataReference mscorlib =
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            MetadataReference[] references = { mscorlib };

            return CSharpCompilation.Create("TransformationCS",
                sourceTrees,
                references,
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));
        }
    }
}
