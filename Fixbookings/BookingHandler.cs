namespace Fixbookings;

public class BookingHandler
{
    public static ReservationModel AddReservation(int hour, int minute, int amount, string name, string number, string table)
    {
        Console.WriteLine($"Booket bord til {amount} personer kl. {hour}:{minute.ToString("00")} - {hour+2}:{minute.ToString("00")}");
        return new ReservationModel(hour, minute, amount, name, number, table);
    }

    public static void ShowReservations(List<ReservationModel> list, int hour, int minute)
    {
        Console.WriteLine($"Viser alle reservasjoner innenfor kl.{hour}:{minute.ToString("00")}"); 
        
        // 14:00 - 16:00 (sjekke fra 12:30 til 15:30)
        // 14:30 - 16:30 (sjekke fra 13:00 - 16:00)
        
        foreach (var b in list)
        {
            // more logic needed in these if checks !!!
            
            if (minute == 30) // 14:30
            {
                if (b.ReservationTimeHour <= (hour+2) && b.ReservationTimeHour > (hour-2)) 
                {
                    Console.WriteLine($"{b.ReservedTable}: {b.ReservationOwnerName}, tlf. {b.ReservationOwnerPhoneNumber} - {b.ReservationTimeHour}:{b.ReservationTimeMinute.ToString("00")} - {b.ReservationTimeHour+2}:{b.ReservationTimeMinute.ToString("00")}.");
                }
            }
            else // 14:00
            {
                if (b.ReservationTimeHour < (hour+2) && b.ReservationTimeHour >= (hour-2))
                {
                    Console.WriteLine($"{b.ReservedTable}: {b.ReservationOwnerName}, tlf {b.ReservationOwnerPhoneNumber} - {b.ReservationTimeHour}:{b.ReservationTimeMinute.ToString("00")} - {b.ReservationTimeHour+2}:{b.ReservationTimeMinute.ToString("00")}.");
                }
            }

        }
    }

    public static void HandleUnavailableBooking()
    {
        Console.WriteLine("Ingen ledige bord til denne tiden som samsvarer med antall personer.");
    }

    
    public static int IsBookingAvailable(List<ReservationModel> list, int hour, int minute)
    {
        var trueCounter = 0;
        
        foreach (var booking in list)
        {
            // Need more logic in if statement...
            if (booking.ReservationTimeHour <= hour - 2 || booking.ReservationTimeHour > hour + 2) trueCounter++;
        }

        return trueCounter;
    }
}

