using System.Collections;

namespace Pet4U.Domain.Shared;

public class ValueObjectList<T> : IReadOnlyList<T>
{
    public IReadOnlyList<T> Values { get; }
    public T this[int index] => Values[index];

    public int Count => Values.Count;


    public ValueObjectList() { }
    public ValueObjectList(IEnumerable<T> list)
    {
        Values = new List<T>(list).AsReadOnly();
    }


    public IEnumerator<T> GetEnumerator() =>
        Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        Values.GetEnumerator();


    public static implicit operator ValueObjectList<T>(List<T> list) =>
        new ValueObjectList<T>(list);

    public static implicit operator List<T>(ValueObjectList<T> list) =>
        list.Values.ToList();
}