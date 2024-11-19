using System;
using System.Collections.Generic;

namespace TableReservationApp
{
    public class TableReservationApp
    {
        public void Run()
        {
            var restaurantService = new RestaurantService();
            var tableBookingService = new TableBookingService(restaurantService.GetRestaurants());
            var repository = new RestaurantFileRepository(restaurantService);

            repository.LoadRestaurantsFromFile("load.txt");

            
        }
    }
    public static class Program
    {
        public static void Main(string[] args)
        {
            var app = new TableReservationApp();
            app.Run();
        }
    }
}

