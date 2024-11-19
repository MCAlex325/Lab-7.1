using System;

namespace TableReservationApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var manager = new ReservationManagerClass();
            string filePath = "load.txt";
            manager.LoadRestaurantsFromFile(filePath);
            var app = new Table_Reservation_App(manager, manager);
            app.Run();
        }
    }
}
