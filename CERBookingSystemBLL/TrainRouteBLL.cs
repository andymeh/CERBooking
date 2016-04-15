using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystemBLL
{
    public class TrainRouteBLL
    {
        public static void addTrainRoute(TrainRoute newTrainRoute)
        {
            using (var dc = new DALDataContext()) {
                dc.TrainRoutes.InsertOnSubmit(newTrainRoute);
                dc.SubmitChanges();
            }
        }
        public static void updateSeatNumber(bool firstClass, int noPassengers, int TrainRouteId)
        {
            using(var db = new DALDataContext())
            {
                var tr = db.TrainRoutes.FirstOrDefault(x => x.TrainRouteId == TrainRouteId);
                if (firstClass)
                {
                    tr.FirstClassSeats = tr.FirstClassSeats - noPassengers;
                }
                else
                {
                    tr.EconomySeats = tr.EconomySeats - noPassengers;
                }
                db.SubmitChanges();
            }
        }
        public static void addTrainRoute(TrainRoute newTrainRoute, DateTime startDate, DateTime endDate)
        {
            using (var dc = new DALDataContext())
            {
                List<TrainRoute> lstTrainRoutes = new List<TrainRoute>();
                while(startDate <= endDate)
                {
                    TrainRoute tobeSubmitted = new TrainRoute
                    {
                        TrainId = newTrainRoute.TrainId,
                        RouteId = newTrainRoute.RouteId,
                        Date = startDate,
                        FirstClassSeats = newTrainRoute.FirstClassSeats,
                        EconomySeats = newTrainRoute.EconomySeats,
                        CostFirstClass = newTrainRoute.CostFirstClass,
                        CostEconomy = newTrainRoute.CostEconomy
                    };
                    lstTrainRoutes.Add(tobeSubmitted);
                    startDate = startDate.AddDays(1);
                }
                dc.TrainRoutes.InsertAllOnSubmit(lstTrainRoutes);
                dc.SubmitChanges();
            }
        }

        public static List<TrainRoute> GetTrainRoutes(int routeId, DateTime? date)
        {
            using(var db = new DALDataContext())
            {
                List<TrainRoute> lstTrainRoutes = new List<TrainRoute>();
                if (date != null)
                {
                    DateTime nDate = date.Value;
                    lstTrainRoutes.AddRange(db.TrainRoutes.Where(x => x.RouteId == routeId && x.Date.Date == nDate.Date)); 
                }
                return lstTrainRoutes;
            }
        }
        public static TrainRoute GetTrainRoute(int TrainRouteId)
        {
            using (var db = new DALDataContext())
            {
                TrainRoute TRoute = new TrainRoute();
                TRoute = (db.TrainRoutes.FirstOrDefault(x => x.TrainRouteId == TrainRouteId));
                return TRoute;
            }
        }
    }
}
