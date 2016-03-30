using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystemBLL
{
    public class CitiesBLL
    {
        public static List<City> getAllCities()
        {
            using(var db = new DALDataContext())
            {
                List<City> cities = new List<City>();
                cities = db.Cities.AsEnumerable().ToList();
                return cities;
            }
        }

        public static City getCity(string _cityName)
        {
            using (var db = new DALDataContext())
            {
                City city = db.Cities.FirstOrDefault(x => x.CityName.Equals(_cityName));
                return city;
            }
        }
        public static City getCity(int _cityId)
        {
            using (var db = new DALDataContext())
            {
                City city = db.Cities.FirstOrDefault(x => x.CityId == _cityId);
                return city;
            }
        }
    }
}
