using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fixbookings;

public class SetupHandler
{

    public static List<ReservationModel> PopulateReservationList()
    {
        var reservationList = new List<ReservationModel>();
        reservationList.Add(new ReservationModel(14, 30, 7, "Ola Nordmann", "99887766", "Bord B"));
        reservationList.Add(new ReservationModel(14, 00, 2, "Kåre Olsen", "99667788", "Bord E"));
        reservationList.Add(new ReservationModel(14, 30, 7, "Per Hansen", "99778866", "Bord B"));
        return reservationList;
    }

    public static List<TableModel> CreateTables()
    {
        var tables = new List<TableModel>();
        tables.Add(new TableModel("Bord A", 6));
        tables.Add(new TableModel("Bord B", 8));
        tables.Add(new TableModel("Bord C", 4));
        tables.Add(new TableModel("Bord D", 2));

        return tables;

    }
}

// public static void CreateTestReservations(string filename) // Mock data.
    // {
    //     var reservations = Reservations.GetReservations();
    //     WriteToFile(reservations, "reservations.json");
    // }
    
    // public static void CreateReservation(string[] commands, string filename)
    // {
    //     var tableFileName = commands[0];
    //     var reservationFileName = commands[1];
    //     var bookingHour = Convert.ToInt32(commands[2]);
    //     var bookingMinute = Convert.ToInt32(commands[3]);
    //     var bookingAmount = Convert.ToInt32(commands[4]);
    //     var bookingPerson = commands[5];
    //     var bookingContactNumber = commands[6];
    //
    //     var table = "Test A";
    //
    //     if (commands.Length > 4)
    //     {
    //         Reservations.AddReservation(bookingHour, bookingMinute, bookingAmount, bookingPerson, bookingContactNumber, table);
    //         WriteToFile(Reservations.ReservationList, "test.json");
    //     }
    // }

