namespace Fixbookings;

public class Reservation
{
    public int ReservationTimeHour { get; }
    public int ReservationTimeMinute { get; }
    public int ReservationAmountOfPeople { get; }
    public string ReservationOwnerName { get; }
    public string ReservationOwnerPhoneNumber { get; }
    public string ReservedTable { get; }

    public Reservation(int reservationTimeHour, int reservationTimeMinute, int reservationAmountOfPeople, string reservationOwnerName, string reservationOwnerPhoneNumber, string reservedTable)
    {
        ReservationTimeHour = reservationTimeHour;
        ReservationTimeMinute = reservationTimeMinute;
        ReservationAmountOfPeople = reservationAmountOfPeople;
        ReservationOwnerName = reservationOwnerName;
        ReservationOwnerPhoneNumber = reservationOwnerPhoneNumber;
        ReservedTable = reservedTable;
    }
}
