namespace Fixbookings;

public class Table
{
    public string Name { get; set; }
    public int Capacity { get; set; }

    
    public Table(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
    }
}