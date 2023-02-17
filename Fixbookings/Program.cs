using System.Text.Json;
using Fixbookings;

var errormessage = @"Feil input gitt...
<filnavn> <filnavn> <time> <minutt '00' eller '30'> <antall personer> <navn> <tlf.nr>
Eksempel: bord.json bookings.json 16 00 4 ""Ola Nordmann"" ""99112233"".";
if (args.Length is not (4 or 7))
{
    Console.WriteLine(errormessage);
    return;
}

var path = $"C:\\Users\\rober\\RiderProjects\\RestaurantBookingSystemApp\\Fixbookings\\";
var tablePath = path + args[0];
var reservationPath = path + args[1];
var tables = JsonSerializer.Deserialize<List<Table>>(FileHandler.ReadFile(tablePath)) ??
             throw new ArgumentNullException(
                 $"JsonSerializer.Deserialize<List<Table>>(FileHandler.ReadFile(tablePath))");

if (args.Length == 4)
{
    if (File.Exists(reservationPath)) BookingHandler.ShowReservations(args, reservationPath);
    else
    {
        Console.WriteLine($"Ingen reservasjoner funnet.");
        Console.ReadLine();
        return;
    }
}

if (args.Length == 7)
{
    List<Reservation> reservations;
    var matchingTables = tables.Where(t => t.Capacity >= Convert.ToInt32(args[4])).ToList();
    if (matchingTables.Count == 0)
    {
        Console.WriteLine("Ingen bord matcher denne bestillingen.");
        Console.ReadLine();
        
        return;
    }

    if (File.Exists(reservationPath))
    {
        try
        {
            reservations = JsonSerializer.Deserialize<List<Reservation>>(FileHandler.ReadFile(reservationPath)) ??
                           throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine("Klarte ikke hente data fra JSON-fil.");
            throw;
        }
    }
    else
    {
        var newReservasion = BookingHandler.AddReservation(args, matchingTables);
        if (newReservasion != null)
        {
            reservations = new List<Reservation> { newReservasion };
            FileHandler.WriteToFile(reservations, reservationPath);
        }
        return;
    }
    
    var availableTables = BookingHandler.GetAvailableTables(reservations, matchingTables, args);
    if (availableTables == null || availableTables.Count == 0)
    {
        Console.WriteLine("Ingen ledige bord til denne tiden som samsvarer med antall personer.");
    }
    else
    {
        var newReservation = BookingHandler.AddReservation(args, availableTables);
        if (newReservation != null)
        {
            reservations.Add(newReservation);
            FileHandler.WriteToFile(reservations, reservationPath);
        }
        else
        {
            return;
        }
    }
}



// A polite way to exit the program =)
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();