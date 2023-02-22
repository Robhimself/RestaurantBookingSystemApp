namespace Fixbookings;

public class BookingHandler
{
    private readonly string _tableJsonFileName;
    private readonly string _reservationJsonFileName;
    private readonly int _hour;
    private readonly int _minute;
    private readonly int _numberOfPeople;
    private readonly string? _name;
    private readonly string? _phoneNumber;

    private readonly FileHandler _fileHandler = new();
    private List<Table>? _tableList;
    private List<Reservation> _reservationList = new();

    
    public BookingHandler(string tableFileName, string reservationFileName, int hour, int minute)
    {
        _tableJsonFileName = tableFileName;
        _reservationJsonFileName = reservationFileName;
        _hour = hour;
        _minute = minute;
        LoadReservationData();
    }
    
    public BookingHandler(string tableFileName, string reservationFileName, int hour, int minute, int amount, string? name,
        string? phoneNumber)
    {
        _tableJsonFileName = tableFileName;
        _reservationJsonFileName = reservationFileName;
        _hour = hour;
        _minute = minute;
        _numberOfPeople = amount;
        _name = name;
        _phoneNumber = phoneNumber;
        LoadReservationData();
        LoadTableData();
    }
    
    public void ShowReservations()
    {
        if (_reservationList is { Count: > 0 })
        {
            foreach (var r in _reservationList.Where(r => r.ReservationHour == _hour && r.ReservationMinute == _minute))
            {
                r.Show();
            }
            return;
        }
        Console.WriteLine("Ingen reservasjoner funnet.");
    }
    
    public void AddReservation()
    {
        var availableTables = GetAvailableTables().OrderBy(t => t.Capacity).ToList();
        if (availableTables.Count < 1)
        {
            Console.WriteLine("Ingen bord ledig.");
            return;
        }
        
        var chosenTable = availableTables[0];
        _reservationList.Add(new Reservation(_hour, _minute, _numberOfPeople, _name, _phoneNumber, chosenTable.Name));

        Console.WriteLine($@"Booket ""{chosenTable.Name}"" til {_numberOfPeople} personer kl. {_hour}:{_minute:00} - {_hour+2}:{_minute:00}");
        
        // Saving file in the same folder as the .exe-file is running.
        _fileHandler.WriteToFile(_reservationList, _reservationJsonFileName);

    }
    
    private List<Table>? GetAvailableTables()
    {
        var reservations = _reservationList.Where(r => r.ReservationHour > _hour-2 && r.ReservationHour < _hour+2).ToList();

        if (reservations.Count < 1)
        {
            return _tableList.Where(t => t.Capacity >= _numberOfPeople).ToList();
        }
        
        var reservedTableNames = reservations.Select(r => r.Table);
        return _tableList.Where(t => !reservedTableNames.Contains(t.Name) && t.Capacity >= _numberOfPeople).ToList();
    }
    
    private void LoadReservationData()
    {
        if (!_fileHandler.FileExists(_reservationJsonFileName))
        {
            Console.WriteLine("Ingen reservasjoner funnet.");
            return;
        }
        
        var list = _fileHandler.ReadFile<Reservation>(_reservationJsonFileName);
        if (list.Count == 0) return;
        _reservationList = new List<Reservation>(list);
    }

    private void LoadTableData()
    {
        // Current file path as .exe-file is running.
        if (!_fileHandler.FileExists(_tableJsonFileName))
        {
            Console.WriteLine($"Sjekk om filnavnet({_tableJsonFileName}) eller plasseringen er riktig.");
            Console.ReadLine();
            
            //This file should exist, therefor the program will exit if the wrong file path or file name is provided.
            Environment.Exit(1);
        }
        
        var tables = _fileHandler.ReadFile<Table>(_tableJsonFileName);
        _tableList = new List<Table>(tables);
    }
}