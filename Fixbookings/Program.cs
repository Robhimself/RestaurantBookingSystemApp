using Fixbookings;

var booking = new BookingHandler(args);

if (args.Length == 4)
{
    booking.ShowReservations();
}

if (args.Length == 7)
{
    booking.AddReservation();
    booking.SaveToFile();
}


// A polite way to exit the program =)
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();