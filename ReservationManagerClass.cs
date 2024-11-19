using System;
using System.Collections.Generic;
using System.IO;

namespace TableReservationApp
{
    public class ReservationManagerClass : IRestaurantManager, ITableBooking, IRestaurantRepository
    {
        private List<RestaurantClass> restaurants;

        public ReservationManagerClass()
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

        // Load Restaurants From
        // File
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
                        throw new FormatException($"Invalid format in line: \"{line}\". Expected fomat: \"RestaurantNAme,CountOfTables\"");
                    }
                    
                    AddRestaurant(parts[0], tableCount);
                }
            }
            catch (FileNotFoundException FNFEx)
            { 
                Console.WriteLine($"Error: \n{FNFEx.Message}");
                Environment.Exit(1);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine($"Error: \n{FEx.Message}");
                Environment.Exit(2);
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"An unexpected error occured: \n{Ex.Message}");
                Environment.Exit(3);
            }
        }

        public void SaveRestaurantsToFile(string file)
        {
            var lines = restaurants.Select(r => $"{r.name},{r.tables.Length}");
            File.WriteAllLines(file, lines);
        }

        //Find All Free Tables
        public List<string> FindAllFreeTables(DateTime dateTime)
        {
            List<string> freeTables = new List<string>();
            foreach (var res in restaurants)
            {
                for (int i = 0; i < res.tables.Length; i++)
                {
                    if (!res.tables[i].IsBooked(dateTime))
                    {
                            freeTables.Add($"{res.name} - Table {i + 1}");
                    }
                }
            }
            return freeTables;
        }

        public bool BookTable(string resName, DateTime date, int tableNumber)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.name == resName);
            if (restaurant == null || tableNumber < 0 || tableNumber >= restaurant.tables.Length)
            {
                throw new ArgumentException("Invalid restaurant name or table number");
            }
            return restaurant.tables[tableNumber].Book(date);
        }

        public void SortRestaurantsByAvailabilityForUsers(DateTime dateTime)
        {
            try
            {
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < restaurants.Count - 1; i++)
                    {
                        int available_tables_current = CountOfAvailableTablesInARestaurant(restaurants[i], dateTime);
                        int available_tables_next = CountOfAvailableTablesInARestaurant(restaurants[i + 1], dateTime);

                        if (available_tables_current < available_tables_next)
                        {
                            // Swap restaurants
                            var temp = restaurants[i];
                            restaurants[i] = restaurants[i + 1];
                            restaurants[i + 1] = temp;
                            swapped = true;
                        }
                    }
                } while (swapped);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error");
            }
        }

        public int CountOfAvailableTablesInARestaurant(RestaurantClass restaurant, DateTime dateTime)
        {
            try
            {
                int count = 0;
                foreach (var table in restaurant.tables)
                {
                    if (!table.IsBooked(dateTime))
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error");
                return 0;
            }
        }
    }

}
