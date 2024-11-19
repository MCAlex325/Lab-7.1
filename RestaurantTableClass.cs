using System;
using System.Collections.Generic;


namespace TableReservationApp
{
    public class RestaurantTableClass
    {
        private List<DateTime> booked_dates;
        public RestaurantTableClass()
        {
            booked_dates = new List<DateTime>();
        }

        
        public bool Book(DateTime date)
        {
            try
            {
                if (booked_dates.Contains(date))
                {
                    return false;
                }
                booked_dates.Add(date);
                return true;
            }
            catch (Exception exeption)
            {
                Console.WriteLine("Error");
                return false;
            }
        }
        public bool IsBooked(DateTime date)
        {
            return booked_dates.Contains(date);
        }
    }
}
