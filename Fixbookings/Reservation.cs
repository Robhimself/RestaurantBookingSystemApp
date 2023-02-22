namespace Fixbookings;

public class Reservation
{
    public int ReservationHour { get; set; }
    public int ReservationMinute { get; set; }
    public int NumberOfPeople { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string Table { get; }

    public Reservation(int hour, int minute, int numberOfPeople, string? name, string? phoneNumber, string table)
    {
        ReservationHour = hour;
        ReservationMinute = minute;
        NumberOfPeople = numberOfPeople;
        Name = name;
        PhoneNumber = phoneNumber;
        Table = table;
    }

    public void Show()
    {
        Console.WriteLine(
            $"{Table}: {Name}, tlf. {PhoneNumber} - {ReservationHour}:{ReservationMinute:00} - {ReservationHour + 2}:{ReservationMinute:00}.");
    }
    
}
