namespace Fixbookings;

public class Tables
{
    public static List<TableModel> GetTables() => new()
    {
        new("Bord A", 6),
        new("Bord B", 8),
        new("Bord C", 4),
        new("Bord D", 2)
    };
}