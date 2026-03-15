namespace CrudExample.Domain.ValueObjects;

public class NamedValuesCollection(Dictionary<string, object?> values)
{
    public T? Get<T>(string key) => (T?)values[key];
    public bool ContainsValue(string name) => values.ContainsKey(name);
}