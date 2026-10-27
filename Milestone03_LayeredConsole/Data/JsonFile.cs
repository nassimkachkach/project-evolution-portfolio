using System.Text.Json;
using System.Text.Json.Serialization;

namespace Milestone03_LayeredConsole.Data;

// Small helper that reads and writes any list to a JSON file.
public static class JsonFile
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static List<T> Load<T>(string path)
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }

    public static void Save<T>(string path, List<T> items)
    {
        File.WriteAllText(path, JsonSerializer.Serialize(items, Options));
    }
}
