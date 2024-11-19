using System;
using System.Collections.Generic;

namespace TableReservationApp
{
    public class Table_Reservation_App
    {
        private readonly IRestaurantManager _restaurantManager;
        private readonly ITableBooking _tableBooking;

        public Table_Reservation_App(IRestaurantManager restaurantManager, ITableBooking tableBooking)
        {
            _restaurantManager = restaurantManager;
            _tableBooking = tableBooking;
        }

        public void Run()
        {
            ReservationManagerClass manager = new();
            manager.AddRestaurant("True", 10);
            manager.AddRestaurant("False", 5);

            Console.WriteLine(manager.BookTable("True", new DateTime(2023, 12, 25), 3));
            Console.WriteLine(manager.BookTable("False", new DateTime(2023, 12, 25), 3));
        }
    }
}

