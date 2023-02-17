using System.Text.Json;

namespace Fixbookings;

public class BookingHandler
{
    public static Reservation? AddReservation(string[] args, List<Table> matchingTables)
    {
        var tables = matchingTables.OrderBy(t => t.Capacity).ToList();
        if (int.TryParse(args[2], out var hour) && int.TryParse(args[3], out var minute) &&
            int.TryParse(args[4], out var amount))
        {
        var name = args[5];
        var number = args[6];

        Console.WriteLine($"Booket bord til {amount} personer kl. {hour}:{minute:00} - {hour+2}:{minute:00}");
        return new Reservation(hour, minute, amount, name, number, tables[0].Name);
            
        }

        Console.WriteLine("Input i bestilling inneholder feil format.");
        Console.WriteLine(@"Eks: bord.json reservasjon.json 15 00 2 ""Fullt Navn"" ""tlfnr"".");
        Console.ReadLine();
        
        return null;
    }

    public static void ShowReservations(string[] args, string path)
    {
        
        var reservations = JsonSerializer.Deserialize<List<Reservation>>(FileHandler.ReadFile(path)) ??
                           throw new ArgumentNullException(
                               $"JsonSerializer.Deserialize<List<Reservation>>(FileHandler.ReadFile(path))");
        
        if (int.TryParse(args[2], out var hour) && int.TryParse(args[3], out var minute))
        {
            Console.WriteLine($"Viser alle reservasjoner kl.{args[2]}:{args[3]}");
            foreach (var b in reservations.Where(
                         b => b.ReservationTimeHour == hour && b.ReservationTimeMinute == minute))
            {
                Console.WriteLine(
                    $"{b.ReservedTable}: {b.ReservationOwnerName}, tlf. {b.ReservationOwnerPhoneNumber} - {b.ReservationTimeHour}:{b.ReservationTimeMinute:00} - {b.ReservationTimeHour + 2}:{b.ReservationTimeMinute:00}.");
            }
        }
        else
        {
            Console.WriteLine("Bruk kun tall i klokkeslett. Eks: 16 00");
        }
    }


    
    
    public static List<Table>? GetAvailableTables(List<Reservation> reservations, List<Table> tables, string[] args)
    {
        if (int.TryParse(args[2], out var hour) && int.TryParse(args[3], out var minute))
        {
            var list = new List<Reservation>(reservations.Where(r => r.ReservationTimeHour == hour && r.ReservationTimeMinute == minute));
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

        Console.WriteLine("Bruk kun tall i klokkeslett. Eks: 16 00");
        return null;
    }
}

