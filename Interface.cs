using System;

namespace TableReservationApp
{
    public interface IRestaurantManager
    {
        void AddRestaurant(string name, int tables);
        List<string> FindAllFreeTables(DateTime dateTime);
    }
    public interface ITableBooking
    {
        bool BookTable(string resName, DateTime date, int tableNumber);
    }
    public interface IRestaurantRepository
    {
        void LoadRestaurantsFromFile(string file);
        void SaveRestaurantsToFile(string file);
    }
}