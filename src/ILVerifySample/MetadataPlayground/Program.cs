using System;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace MetadataPlayground
{
    internal class Program
    {
        private static void Main(System.String[] args)
        {
            foreach (String fileName in new String[] { "netcoreappinterface.dll", "netcoreapplib.dll" })
            {
                Console.WriteLine("Lib: " + fileName);
                PEReader peReader = new PEReader(File.OpenRead(fileName));
                MetadataReader metadataReader = peReader.GetMetadataReader(MetadataReaderOptions.None);
                Console.WriteLine("IsAssembly " + metadataReader.IsAssembly);
                foreach (TypeDefinitionHandle td in metadataReader.TypeDefinitions)
                {
                    TypeDefinition typeDefinition = metadataReader.GetTypeDefinition(td);
                    Console.WriteLine("TypeDefinition name: " + metadataReader.GetString(typeDefinition.Name) + ((((Int32)typeDefinition.Attributes & 0x00000020) == 0x00000020) ? " [IsInterface]" : ""));
                    Console.WriteLine("Attributes name: " + typeDefinition.Attributes);
                    foreach (MethodDefinitionHandle mdh in typeDefinition.GetMethods())
                    {
                        MethodDefinition md = metadataReader.GetMethodDefinition(mdh);
                        Console.WriteLine(" Method name: " + metadataReader.GetString(md.Name));
                        foreach (ParameterHandle ph in md.GetParameters())
                        {
                            Parameter parameterDef = metadataReader.GetParameter(ph);
                            Console.WriteLine("     Param name: " + metadataReader.GetString(parameterDef.Name) + (parameterDef.SequenceNumber == 0 ? "- return value" : ""));
                        }
                    }
                    Console.WriteLine("----");
                }
            }

        }
    }
}
