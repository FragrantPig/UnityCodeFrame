
using System.Collections.Generic;

public class Entry<T> where T : Entry<T>, new()
{
    public virtual List<T> GetEntries()
    {
        return null;
    }
}