using Fixbookings;

const string wrongInputsMessage = @"
For å sjekke reservasjoner: <program.exe> <filnavn> <filnavn> <time> <minutt>
Eksempel: 
Fixbookings.exe bord.json bookings.json 16 00

For å legge til reservasjon: <program.exe> <filnavn> <filnavn> <time> <minutt> <antall personer> <navn> <tlf.nr>
Eksempel:
Fixbookings.exe bord.json bookings.json 16 00 4 ""Ola Nordmann"" ""99112233"".
";

BookingHandler booking;

var hour = 0;
var minute = 0;

if (!int.TryParse(args[2], out hour) && !int.TryParse(args[3], out minute))
{
    Console.WriteLine(wrongInputsMessage);
    Console.ReadLine();

    return;
}

if (hour > 24 || hour < 0 || minute < 0 || minute > 60)
{
    Console.WriteLine("Klokkeslettet du tastet er ikke gyldig.");
    Console.WriteLine(wrongInputsMessage);
    Console.ReadLine();
    
    return;
}

if (args.Length is 4)
{
    booking = new BookingHandler(args[0], args[1], hour, minute);
    booking.ShowReservations();
}
else if (args.Length is 7)
{
    if (!int.TryParse(args[4], out var numberOfPeople))
    {
        Console.WriteLine(wrongInputsMessage);
        Console.ReadLine();

        return;
    }
    
    booking = new BookingHandler(args[0], args[1], hour, minute, numberOfPeople, args[5], args[6]);
    booking.AddReservation();
}
else
{
    Console.WriteLine(wrongInputsMessage);
}

// A polite way to exit the program =)
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();