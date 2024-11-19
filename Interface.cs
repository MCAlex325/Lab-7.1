using System;

namespace TableReservationApp
{
    public interface IRestaurantService
    {
        void AddRestaurant(string name, int tables);
        List<string> FindAllFreeTables(DateTime dateTime);
        void SortRestaurantsByAvailability(DateTime dateTime);
    }
    public interface ITableBookingService
    {
        bool BookTable(string restaurantName, DateTime date, int tableNumber);
    }
    public interface IRestaurantRepository
    {
        void LoadRestaurantsFromFile(string file);
    }
}