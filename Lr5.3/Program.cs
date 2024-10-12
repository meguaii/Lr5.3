using System;
using System.Collections;

public class MyDictionary<TKey, TValue> : IEnumerable
{
    private TKey[] keys;
    private TValue[] values;
    private int count;

    public MyDictionary()
    {
        keys = new TKey[4];
        values = new TValue[4];
        count = 0;
    }

    public int Count => count;

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
        {
            throw new ArgumentException("An element with the same key already exists.");
        }

        if (count == keys.Length)
        {
            Resize();
        }

        keys[count] = key;
        values[count] = value;
        count++;
    }

    public TValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < count; i++)
            {
                if (keys[i].Equals(key))
                {
                    return values[i];
                }
            }

            throw new KeyNotFoundException("Key not found.");
        }
        set
        {
            for (int i = 0; i < count; i++)
            {
                if (keys[i].Equals(key))
                {
                    values[i] = value;
                    return;
                }
            }

            throw new KeyNotFoundException("Key not found.");
        }
    }

    private void Resize()
    {
        int newSize = keys.Length * 2;
        Array.Resize(ref keys, newSize);
        Array.Resize(ref values, newSize);
    }

    public bool ContainsKey(TKey key)
    {
        for (int i = 0; i < count; i++)
        {
            if (keys[i].Equals(key))
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }
}

class Program
{
    static void Main()
    {
        MyDictionary<string, int> myDict = new MyDictionary<string, int>();

        myDict.Add("one", 1);
        myDict.Add("two", 2);
        myDict.Add("three", 3);

        Console.WriteLine("Count: " + myDict.Count);


        foreach (var item in myDict)
        {
            var pair = (KeyValuePair<string, int>)item;
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        Console.WriteLine("Value for 'two': " + myDict["two"]);

        myDict["two"] = 22;
        Console.WriteLine("Updated value for 'two': " + myDict["two"]);
    }
}
