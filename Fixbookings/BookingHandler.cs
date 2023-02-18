using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Fixbookings;

public class BookingHandler
{
    private string? TableJsonFileName { get; set; }
    private string? ReservationJsonFileName { get; set; }
    private int BookingHour { get; set; }
    private int BookingMinute { get; set; }
    private int _numberOfPeople = 0;
    private string _name = "";
    private string _phoneNumber = "";

    public List<Table> TableList = new();
    private List<Reservation> _reservationList = new();

    private const string WrongInputsMessage = @"
For å sjekke reservasjoner: <program.exe> <filnavn> <filnavn> <time> <minutt '00' eller '30'>
Eksempel: 
Fixbookings.exe bord.json bookings.json 16 00

For å legge til reservasjon: <program.exe> <filnavn> <filnavn> <time> <minutt '00' eller '30'> <antall personer> <navn> <tlf.nr>
Eksempel:
Fixbookings.exe bord.json bookings.json 16 00 4 ""Ola Nordmann"" ""99112233"".
";
    
    public BookingHandler(string?[] commands)
    {
        if (commands.Length == 4)
        {
            this.Constructor(commands[0], commands[1], commands[2], commands[3], null,null,null);
        }
        else if (commands.Length == 7)
        {
            this.Constructor(commands[0], commands[1], commands[2], commands[3], commands[4], commands[5],
                commands[6]);
        }
        else
        {
            Console.WriteLine(WrongInputsMessage);
            Console.ReadLine();
            
            throw new ArgumentException("Feil antall inputs ble tastet inn.");
        }
    }


    private void Constructor(string tableFile, string bookingFile, string hourString, string minuteString,
        string? numberOfPeople, string? name, string? phoneNumber)
    {
        // If you do not want to add tables.json in /bin/Debug/net6.0/
        // Then uncomment (remove "//") at the beginning of the line below.
        // tableFile = $"../../../{tableFile}";
        TableJsonFileName = tableFile;
        ReservationJsonFileName = bookingFile;

        if (File.Exists(ReservationJsonFileName))
        {
            var reservationsJsonString = File.ReadAllText(ReservationJsonFileName);
            if (!string.IsNullOrWhiteSpace(reservationsJsonString))
            {
                try
                {
                    _reservationList = JsonSerializer.Deserialize<List<Reservation>>(reservationsJsonString);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Kunne ikke lese av {ReservationJsonFileName}...");
                    Console.ReadLine();
                    
                    throw;
                }
            }
        }

        if (numberOfPeople != null && name != null && phoneNumber != null)
        {
            int.TryParse(numberOfPeople, out _numberOfPeople);
            _name = name;
            _phoneNumber = phoneNumber;
            if (!File.Exists(TableJsonFileName))
            {
                Console.WriteLine("Finner ikke fil med bord-informasjon...");
                Console.ReadLine();
                
                return;
            }
            var tableJsonString = File.ReadAllText(TableJsonFileName);
            if (!string.IsNullOrWhiteSpace(tableJsonString))
            {
                try
                {
                    TableList = JsonSerializer.Deserialize<List<Table>>(tableJsonString);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Kunne ikke lese av {TableJsonFileName}...");
                    throw;
                }
            }
        }

        int.TryParse(hourString, out var hour);
        int.TryParse(minuteString, out var minute);
        BookingHour = hour;
        BookingMinute = minute;
    }
    
    
    public void AddReservation()
    {
        Table chosenTable;
        var tables = GetMatchingTables().OrderBy(t => t.Capacity).ToList();
        
        if (tables.Count == 0)
        {
            Console.WriteLine("Ingen bord passer med ønsket antall personer i bestillingen.");
            return;
        }

        if (_reservationList.Count == 0)
        {
            chosenTable = tables[0];
        }
        else
        {
            var availableTables = new List<Table>(GetAvailableTables(tables)).OrderBy(t => t.Capacity).ToList();
            if (availableTables.Count == 0)
            {
                Console.WriteLine("Ingen ledige bord til denne tiden som samsvarer med antall personer.");
                return;
            }
            chosenTable = availableTables[0];
        }
        Console.WriteLine($"Booket bord til {_numberOfPeople} personer kl. {BookingHour}:{BookingMinute:00} - {BookingHour+2}:{BookingMinute:00}");
        _reservationList.Add(new Reservation(BookingHour, BookingMinute, _numberOfPeople, _name, _phoneNumber, chosenTable.Name));
    }
    
    private List<Table> GetAvailableTables(List<Table> tables)
    {
        var list = GetReservationsInSameTimeFrame();
        var tableList = new List<Table>(tables);
    
        for (var i = list.Count - 1; i >= 0; i--)
        {
            var r = list[i];
            foreach (var t in tables.Where(t => r.ReservedTable == t.Name))
            {
                tableList.Remove(t);
            }
        }
        return tableList;
    }
    
    private List<Table> GetMatchingTables()
    {
        return new List<Table>(TableList.Where(t => t.Capacity >= _numberOfPeople).ToList());
    }

    private List<Reservation> GetReservationsInSameTimeFrame()
    {
        return new List<Reservation>(_reservationList.Where(r => r.ReservationTimeHour == BookingHour && r.ReservationTimeMinute == BookingMinute));
    }
    
    public void ShowReservations()
    {
        if (_reservationList.Count > 0)
        {
            if ((BookingHour is >= 0 and <= 24) && (BookingMinute is 00 or 30))
            {
                Console.WriteLine($"Viser alle reservasjoner kl.{BookingHour}:{BookingMinute:00}");
                foreach (var b in _reservationList.Where(
                             b => b.ReservationTimeHour == BookingHour && b.ReservationTimeMinute == BookingMinute))
                {
                    Console.WriteLine(
                        $"{b.ReservedTable}: {b.ReservationOwnerName}, tlf. {b.ReservationOwnerPhoneNumber} - {b.ReservationTimeHour}:{b.ReservationTimeMinute:00} - {b.ReservationTimeHour + 2}:{b.ReservationTimeMinute:00}.");
                }
            }
            else
            {
                Console.WriteLine("Klokkeslettet som er tastet inn er feil.");
                Console.WriteLine(WrongInputsMessage);
            }
        }
        else
        {
                Console.WriteLine("Ingen reservasjoner funnet.");
        }
    }

    public void SaveToFile()
    {
        var options = new JsonSerializerOptions() { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(_reservationList, options);

        if (string.IsNullOrEmpty(ReservationJsonFileName) || !ReservationJsonFileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
        {
            ReservationJsonFileName = "reservationDefaultName.json";
        }

        File.WriteAllText(ReservationJsonFileName, jsonString);
    }
}

