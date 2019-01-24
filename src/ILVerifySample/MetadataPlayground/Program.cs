using System;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Reflection.Metadata.Ecma335;
namespace MetadataPlayground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach (string fileName in new string[] { @"C:\git\corert\bin\Windows_NT.x64.Debug\ILVerification.Tests\Tests\InterfaceImplementation.dll" })
            {
                Console.WriteLine("Lib: " + fileName);
                PEReader peReader = new PEReader(File.OpenRead(fileName));
                MetadataReader metadataReader = peReader.GetMetadataReader(MetadataReaderOptions.None);
                Console.WriteLine("IsAssembly " + metadataReader.IsAssembly);
                foreach (TypeDefinitionHandle td in metadataReader.TypeDefinitions)
                {
                    TypeDefinition typeDefinition = metadataReader.GetTypeDefinition(td);
                    if (metadataReader.GetString(typeDefinition.Name) != "InvalidReturnTypeM1_InvalidType_InterfaceMethodNotImplemented")
                    {
                        continue;
                    }
                    Console.WriteLine("TypeDefHandle token:" + metadataReader.GetToken(td).ToString("X8"));
                    Console.WriteLine("TypeDefinition name: " + metadataReader.GetString(typeDefinition.Name) + ((((int)typeDefinition.Attributes & 0x00000020) == 0x00000020) ? " [IsInterface]" : ""));
                    Console.WriteLine("Attributes name: " + typeDefinition.Attributes);
                    foreach (InterfaceImplementationHandle interfaceImplementation in typeDefinition.GetInterfaceImplementations())
                    {
                        InterfaceImplementation im = metadataReader.GetInterfaceImplementation(interfaceImplementation);
                        Console.WriteLine("InterfaceImplementationHandle:" + metadataReader.GetToken(im.Interface).ToString("X8"));
                    }
                    foreach (MethodDefinitionHandle mdh in typeDefinition.GetMethods())
                    {
                        MethodDefinition md = metadataReader.GetMethodDefinition(mdh);
                        Console.WriteLine(" Method name: " + metadataReader.GetString(md.Name));
                        foreach (ParameterHandle ph in md.GetParameters())
                        {
                            Parameter parameterDef = metadataReader.GetParameter(ph);
                            Console.WriteLine("     Param name: " + metadataReader.GetString(parameterDef.Name) + (parameterDef.SequenceNumber == 0 ? "- return value" : ""));
                        }
                        Console.WriteLine("MethodDefinitionHandle:" + metadataReader.GetToken(mdh).ToString("X8"));
                    }
                    Console.WriteLine("----");
                }
            }

        }
    }
}
