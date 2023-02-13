using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Fixbookings;

public class ProcessData
{
    
    private static readonly JsonSerializerOptions _options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };


    private static void WriteToFile(object obj, string fileName)
    {
        var options = new JsonSerializerOptions(_options) 
        { 
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(obj, options);
        
        File.WriteAllText(fileName, jsonString);
    }



    
    
    public static void ReadFromFile(string fileName)
    {
        try
        {
            using (var sr = new StreamReader(fileName))
            {
                // Read the stream as a string, and write the string to the console.
                Console.WriteLine(sr.ReadToEnd());
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    
    
    public static void CreateTables(string filename)
    {
        var tables = Tables.GetTables();
        WriteToFile(tables, filename);
    }

    public static void CreateTestReservations(string filename) // Mock data.
    {
        var reservations = Reservations.GetReservations();
        WriteToFile(reservations, "reservations.json");
    }
    
    public static void CreateReservation(string[] commands, string filename)
    {
        var tableFileName = commands[0];
        var reservationFileName = commands[1];
        var bookingHour = Convert.ToInt32(commands[2]);
        var bookingMinute = Convert.ToInt32(commands[3]);
        var bookingAmount = Convert.ToInt32(commands[4]);
        var bookingPerson = commands[5];
        var bookingContactNumber = commands[6];

        var table = "Test A";

        if (commands.Length > 4)
        {
            Reservations.AddReservation(bookingHour, bookingMinute, bookingAmount, bookingPerson, bookingContactNumber, table);
            WriteToFile(Reservations.ReservationList, "test.json");
        }
    }

    public void ShowReservations(string filename, int hour, int minute)
    {
        ReadFromFile(filename);
        // Console.WriteLine($"Viser alle reservasjoner kl.{hour}:{minute}");
        
        /* foreach (var booking in bookings)
         * {
         *      Console.WriteLine($"{bordnavn}: {personnavn}, tlf {tlfnr} - {fraTid} - {tilTid}");
         */
        
    }

    public void HandleUnavailableBooking()
    {
        Console.WriteLine("Ingen ledige bord til denne tiden som samsvarer med antall personer.");
    }
    
}