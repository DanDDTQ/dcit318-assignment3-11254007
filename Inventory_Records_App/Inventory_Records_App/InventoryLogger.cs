using System;
using System.Text.Json;

namespace InventoryRecordsApp;
public class InventoryLogger<T> where T : IInventoryEntity
{
    private readonly List<T> _log = new();
    private readonly string _filePath;

    public InventoryLogger(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("filePath is required", nameof(filePath));

        _filePath = filePath;
    }

    // Add item (prevent duplicate Ids)
    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        if (_log.Any(x => x.Id == item.Id))
            throw new InvalidOperationException($"An item with Id {item.Id} already exists in the log.");

        _log.Add(item);
    }

    // Return a copy of all items
    public List<T> GetAll() => new(_log);

    // Persist to file as JSON
    public void SaveToFile()
    {
        try
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var options = new JsonSerializerOptions { WriteIndented = true };

            // Use FileStream + using
            using var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            JsonSerializer.Serialize(fs, _log, options);
        }
        catch (UnauthorizedAccessException uex)
        {
            Console.WriteLine($"Permission error while saving file: {uex.Message}");
            throw;
        }
        catch (IOException ioex)
        {
            Console.WriteLine($"I/O error while saving file: {ioex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while saving file: {ex.Message}");
            throw;
        }
    }

    // Load from file (replaces current _log contents)
    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"Data file not found: {_filePath}");
                _log.Clear();
                return;
            }

            using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var items = JsonSerializer.Deserialize<List<T>>(fs, options);
            _log.Clear();

            if (items != null)
            {
                _log.AddRange(items);
            }
        }
        catch (JsonException jex)
        {
            Console.WriteLine($"Data format error while loading file: {jex.Message}");
            throw;
        }
        catch (UnauthorizedAccessException uex)
        {
            Console.WriteLine($"Permission error while loading file: {uex.Message}");
            throw;
        }
        catch (IOException ioex)
        {
            Console.WriteLine($"I/O error while loading file: {ioex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while loading file: {ex.Message}");
            throw;
        }
    }

    // Clear the log (helper for simulating new sessions)
    public void Clear() => _log.Clear();
}