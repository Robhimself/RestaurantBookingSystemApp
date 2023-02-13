/*
 * Booke bord, med args.
 *
 * argument 1 er en .json-fil. med oversikt over bordnavn og kapasitet.
 * Feks: [
         {name: 'Bord A', capacity: 6},
         {name: 'Bord B', capacity: 8},
         {name: 'Bord C', capacity: 4},
         {name: 'Bord D', capacity: 2},
         ]
         
 * Argument 2 er en fil med eksisterende reservasjoner. (Skal lage denne filen hvis den ikke eksisterer).
 * "Programmet forholder seg ikke til ulike restauranter og ulike datoer, men man kan bruke ulike filer og få det til på den måten. "
 *
 * argument 3 og 4 er klokkeslett. Hvis kun 4 args gis skal reservasjoner vises.
 * Vise timer og minutter, antatt tid per reservasjon er 2timer.
 *
 * resten av args er antall personer, navn reservasjonen ligger under og telefonnummer til personen.
 *
 *
 * Funksjoner: mer enn 4 args betyr å reservere bord. Derfra må programmet sjekke om det er et ledig bord og
 * gi en tilbakemelding på det ble booket et bord eller ikke...
 *
 *
 * bruk json serialisering og deserialisering.
 *
 *
 * Gode klasseforslag:  Tables(navn og kapasitet)
 *                      Reservation ( bord, antall personer, klokkeslett ) + navn og telefonnummer privat. 
 *
 * Disse må plasseres et sted: navn og telefonnummer.
 *
 * -------------------
 * -------------------
 * -------------------
 *
 * C:\...\> Fixbookings tables.json reservations.json 14 30 7 "Ola Nordmann" "99887766"
Booket bord til 7 personer kl. 14:30 - 16:30
C:\...\> Fixbookings tables.json reservations.json 14 00 2 "Kåre Olsen" "99667788"
Booket bord til 2 personer kl. 14:00 - 16:00
C:\...\> Fixbookings tables.json reservations.json 14 30 7 "Per Hansen" "99778866"
Har ikke ledig bord til 7 personer kl. 14:30.
C:\...\> Fixbookings tables.json reservations.json 15 00
Viser alle reservasjoner kl. 15:00: 
Bord B: Ola Nordmann, tlf 99887766 - 14:30 - 16:30
Bord D: Kåre Olsen, tlf 99667788 - 14:00 - 16:00

 */

using Fixbookings;

// Getting arguments in cleartext during development
var tableFileName = args[0];
var reservationFileName = args[1];
var bookingHour = args[2];
var bookingMinute = args[3];
var bookingAmount = args[4];
var bookingPerson = args[5];
var bookingContactNumber = args[6];

var jsonDataString = $@""; // temp

foreach (var arg in args) // checking input
{
    Console.WriteLine(arg);
}


//  Setting up the tables, and creating our reference-file in a json-format. 
ProcessData.CreateTables(tableFileName); // Add a method for adding more tables in the restaurant?
ProcessData.ReadFromFile(tableFileName);

//  Add a reservation
if (args.Length == 4)
{
    // Check current reservations.
    
    // ShowReservations(reservationFileName, bookingHour, bookingMinute);
}
else
{
    // Check if time and table are available.
    
    /*
     * if (TableIsAvailable)
     * {
     *
     * }
     * else
     * {
     *      HandleUnavailableBooking();
     * }
     */
    
    // Create reservation.
    ProcessData.CreateTestReservations(reservationFileName); // Mock data
    ProcessData.CreateReservation(args, reservationFileName); // Remember to remove "test.json" string from method.
    
    // Self check
    ProcessData.ReadFromFile(reservationFileName);
    ProcessData.ReadFromFile("test.json");
    
}



// A polite way to exit the program =)
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();