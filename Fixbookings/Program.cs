
using System.Text.Json;
using Fixbookings;

//  Setting up the tables, and creating our reference-file in a json-format. 
var tables = SetupHandler.CreateTables();
FileHandler.WriteToFile(tables, args[0]);
var reservationList = SetupHandler.PopulateReservationList();




// Version 2.0!

var tableListString = FileHandler.ReadFile(args[0]);
var tablesJson = JsonSerializer.Deserialize<List<TableModel>>(tableListString);

for (var index = 0; index < tablesJson.Count; index++)
{
    var table = tables[index];
    Console.WriteLine($"Table {index+1}: {table.Name}, Capacity: {table.Capacity}");
}

if (args.Length == 4)
{
    BookingHandler.ShowReservations(reservationList, 15,00);
}


if (args.Length == 7) // args[] = table-filename, reservationlist-filename, hour, minute, peopleCount, name, number.
{
    if (BookingHandler.IsBookingAvailable(reservationList, 17,00) > 0)
    {
        Console.WriteLine("Tidspunktet er ledig!");
        reservationList.Add(BookingHandler.AddReservation(17, 00, 2, "Bob Olav", "55599999", "TestBord 1(før bordsjekk)"));
    }
    else
    {
        BookingHandler.HandleUnavailableBooking();
        BookingHandler.ShowReservations(reservationList, 15,00);
    }
}


Console.WriteLine("Printer ut alle reservasjoner: ");

foreach (var b in reservationList)
{
    Console.WriteLine($"{b.ReservedTable}: {b.ReservationOwnerName}, tlf {b.ReservationOwnerPhoneNumber} - {b.ReservationTimeHour}:{b.ReservationTimeMinute.ToString("00")} - {b.ReservationTimeHour+2}:{b.ReservationTimeMinute.ToString("00")}.");
}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine(@"Vil du lagre data?

[Y]es
[N]o

");
var saveInput = Console.ReadKey();

if (saveInput.Key == ConsoleKey.Y)
{
    Console.Clear();
    Console.WriteLine($"You pressed {saveInput.Key.ToString()}");
    Console.WriteLine("Saving data...");
    FileHandler.WriteToFile(reservationList, "newlySavedReservasions.json");
}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine(@"Vil du vise data som string?

[Y]es
[N]o

");
var showInput = Console.ReadKey();
if (showInput.Key == ConsoleKey.Y)
{
    Console.Clear();
    Console.WriteLine($"You pressed {showInput.Key.ToString()}");
    Console.WriteLine();
    FileHandler.PrintBookings("newlySavedReservasions.json");
}


// A polite way to exit the program =)
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();