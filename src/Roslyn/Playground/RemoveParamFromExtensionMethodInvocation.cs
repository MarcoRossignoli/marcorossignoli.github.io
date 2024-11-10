using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Playground
{
    public class RemoveParamFromExtensionMethodInvocation
    {
        public static void Demo()
        {
            Compilation compilation = CreateTestCompilation();
            Debug.Assert(compilation.SyntaxTrees.Count() == 1);

            SyntaxTree tree = compilation.SyntaxTrees.First();
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            SemanticModel semanticModel = compilation.GetSemanticModel(tree);

            IEnumerable<MemberAccessExpressionSyntax> memberAccess =
                root.DescendantNodes().OfType<MemberAccessExpressionSyntax>()
                .Where(x => x.IsKind(SyntaxKind.SimpleMemberAccessExpression));

            INamedTypeSymbol integer = compilation.GetTypeByMetadataName("System.Int32")
                ?? throw new Exception("System.Int32 not found in the compilation");
            INamedTypeSymbol? iintType = compilation.GetTypeByMetadataName("Sample.IInt`1") 
                ?? throw new Exception("Sample.IInt not found in the compilation");
            INamedTypeSymbol iintTypeOfInt =  iintType.Construct(integer);

            foreach (MemberAccessExpressionSyntax access in memberAccess)
            {
                // Identify the extension method call
                SymbolInfo symbolInfo = semanticModel.GetSymbolInfo(access);
                if (symbolInfo.Symbol is IMethodSymbol methodSymbol &&
                    methodSymbol.IsExtensionMethod &&
                    methodSymbol.Name == "AddExt" &&
                    SymbolEqualityComparer.Default.Equals(methodSymbol.ReceiverType, iintTypeOfInt))
                {

                }
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
