namespace Fixbookings;

public class Reservations
{

    public static readonly List<ReservationModel> ReservationList = new(); // True database
    public static List<ReservationModel> GetReservations() => new() // Mock data 
    {
        new(14, 30,7,"Ola Nordmann", "99887766", "Bord B"),
        new(14,00,2,"Kåre Olsen","99667788", "Bord C"),
        new(14,30,7,"Per Hansen", "99778866", "Bord D")
    };

    
    public static void AddReservation(int hour, int minute, int amount, string name, string number, string table)
    {
        var reservation = new ReservationModel(hour, minute, amount, name, number, table);
        ReservationList.Add(reservation);
    }
}