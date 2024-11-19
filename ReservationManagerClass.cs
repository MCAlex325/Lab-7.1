using System;
using System.Collections.Generic;
using System.IO;

namespace TableReservationApp
{
    public class RestaurantService : IRestaurantService
    {
        private List<RestaurantClass> restaurants;

        public RestaurantService()
        {
            restaurants = new List<RestaurantClass>();
        }

        public void AddRestaurant(string name, int tables)
        {
            RestaurantClass restaurant = new RestaurantClass
            {
                name = name,
                tables = new RestaurantTableClass[tables]
            };

            for (int i = 0; i < tables; i++)
            {
                restaurant.tables[i] = new RestaurantTableClass();
            }
            restaurants.Add(restaurant);
        }

        public List<string> FindAllFreeTables(DateTime dateTime)
        {
            List<string> freeTables = new List<string>();
            foreach (var restaurant in restaurants)
            {
                for (int i = 0; i < restaurant.tables.Length; i++)
                {
                    if (!restaurant.tables[i].IsBooked(dateTime))
                    {
                        freeTables.Add($"{restaurant.name} - Table {i + 1}");
                    }
                }
            }
            return freeTables;
        }

        public void SortRestaurantsByAvailability(DateTime dateTime)
        {
            restaurants = restaurants
                .OrderByDescending(r => r.tables.Count(t => !t.IsBooked(dateTime)))
                .ToList();
        }

        public List<RestaurantClass> GetRestaurants()
        {
            return restaurants;
        }
    }
    public class TableBookingService : ITableBookingService
    {
        private List<RestaurantClass> restaurants;

        public TableBookingService(List<RestaurantClass> restaurantList)
        {
            restaurants = restaurantList;
        }

        public bool BookTable(string restaurantName, DateTime date, int tableNumber)
        {
            foreach (var res in restaurants)
            {
                Console.WriteLine($"Checking restaurant: '{res.name}'");
                if (res.name == restaurantName.Trim())
                {
                    if (tableNumber < 0 || tableNumber >= res.tables.Length)
                    {
                        throw new Exception("Invalid table number");
                    }
                    return res.tables[tableNumber].Book(date);
                }
            }
            throw new Exception("Invalid restaurant name or table number");
        }
    }
    public class RestaurantFileRepository : IRestaurantRepository
    {
        private IRestaurantService _restaurantService;

        public RestaurantFileRepository(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public void LoadRestaurantsFromFile(string file)
        {
            try
            {
                if (!File.Exists(file))
                {
                    throw new FileNotFoundException($"File not found: {file}");
                }

                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length != 2 || !int.TryParse(parts[1], out int tableCount))
                    {
                        throw new FormatException($"Invalid format in line: \"{line}\". Expected format: \"RestaurantName,TableCount\"");
                    }

                    _restaurantService.AddRestaurant(parts[0].Trim(), tableCount);
                    Console.WriteLine($"Restaurant added: {parts[0].Trim()}, Tables: {tableCount}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading restaurants: {ex.Message}");
            }
        }
    }
}
