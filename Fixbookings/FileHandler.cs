using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fixbookings;

public class FileHandler
{
    private readonly JsonSerializerOptions _options = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    
    public bool FileExists(string file)
    {
        return File.Exists(file);
    }
    
    public List<T> ReadFile<T>(string fileName)
    {
        var jsonString = File.ReadAllText(fileName);

        try
        {
            var json = JsonConvert.DeserializeObject<List<T>>(jsonString);
            return json;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Kunne ikke lese av {jsonString}... " + ex.Message);
            return null;
        }
    }

    public void WriteToFile(object obj, string fileName)
    {
        var options = new JsonSerializerOptions(_options) { WriteIndented = true };
        
        
        try
        {
            var jsonString = JsonSerializer.Serialize(obj, options);
            if (!fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) fileName = "reservations.json";
            
            File.WriteAllText(fileName, jsonString);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Kunne ikke skrive til {fileName}... " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("There was an I/O error while reading or writing the file: " + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("You do not have permission to read or write the file: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }
}