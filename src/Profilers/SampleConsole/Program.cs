
using System.Collections;

foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
{
    if (item.Key.ToString()!.StartsWith("CORECLR"))
    {
        // Console.WriteLine(item.Key + " " + item.Value);
    }
}

Console.WriteLine("Hello, World!");
//Console.ReadKey();
