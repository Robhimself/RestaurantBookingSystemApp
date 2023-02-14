namespace Fixbookings;

public class TableModel
{
    public string Name { get; set; }
    public int Capacity { get; set; }

    
    public TableModel(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
    }
}