namespace Fixbookings;

public record ReservationModel(
     int ReservationTimeHour,
     int ReservationTimeMinute,
     int ReservationAmountOfPeople,
     string ReservationOwnerName,
     string ReservationOwnerPhoneNumber,
     string ReservedTable);