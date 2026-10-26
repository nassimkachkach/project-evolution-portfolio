using System.Text.Json;
using System.Text.Json.Serialization;

namespace Milestone02_OOP.Models;

// This class has only one job: read and write the tasks to a JSON file.
public class JsonTaskStorage
{
    private readonly string _filePath;

    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true,
        // Save the enum as text ("High") instead of a number (3).
        Converters = { new JsonStringEnumConverter() }
    };

    public JsonTaskStorage(string filePath)
    {
        _filePath = filePath;
    }

    public List<TaskItem> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TaskItem>();
        }

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json, _options) ?? new List<TaskItem>();
    }

    public void Save(IEnumerable<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, _options);
        File.WriteAllText(_filePath, json);
    }
}
