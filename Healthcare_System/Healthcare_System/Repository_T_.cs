using System;

namespace HealthcareSystem;

// Generic repository for entity management
public class Repository<T> where T : class
{
    private readonly List<T> items = new();

    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        items.Add(item);
    }

    public List<T> GetAll()
    {
        // return a copy to preserve encapsulation
        return new List<T>(items);
    }

    // Return first match or null
    public T? GetById(Func<T, bool> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        return items.FirstOrDefault(predicate);
    }

    // Remove the first item matching predicate, return true if removed
    public bool Remove(Func<T, bool> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        var item = items.FirstOrDefault(predicate);
        if (item == null) return false;
        return items.Remove(item);
    }
}