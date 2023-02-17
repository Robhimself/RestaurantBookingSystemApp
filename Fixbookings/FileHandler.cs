using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fixbookings;

public class FileHandler
{
    private static readonly JsonSerializerOptions Options = new()
        { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public static string ReadFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }


    public static void WriteToFile(object obj, string fileName)
    {
        var options = new JsonSerializerOptions(Options)
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(obj, options);

        File.WriteAllText(fileName, jsonString);
    }
}