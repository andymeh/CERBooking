using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CERBookingSystemDAL;

namespace CERBookingSystemBLL
{
    public class RouteBLL
    {
        public static void addRoute(Route newRoute)
        {
            using (var db = new DALDataContext())
            {
                db.Routes.InsertOnSubmit(newRoute);
                db.SubmitChanges();
            }
        }
        public static Route getRoute(int routeId)
        {
            using (var db = new DALDataContext())
            {
                Route route = new Route();
                route = db.Routes.FirstOrDefault(x => x.RouteId == routeId);
                return route;
            }
        }

        public static List<Route> getAllRoutes()
        {
            using(var db = new DALDataContext())
            {
                List<Route> allRoutes = db.Routes.AsEnumerable().ToList();
                return allRoutes;
            }
        }

        public static List<Route> getRoutes(City source, City destination)
        {
            using (var db = new DALDataContext())
            {
                List<Route> route = new List<Route>();
                route.AddRange(db.Routes.Where(x => x.Source == source.CityId && x.Destination == destination.CityId));
                return route;
            }
        }
    }
}
