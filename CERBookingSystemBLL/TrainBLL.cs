using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystemBLL
{
    public class TrainBLL
    {
        public static void addTrain(Train newTrain)
        {
            using (var dc = new DALDataContext())
            {
                dc.Trains.InsertOnSubmit(newTrain);
                dc.SubmitChanges();
            }
        }

        public static Train getTrain(int trainId)
        {
            Train train = new Train();
            using (var dc = new DALDataContext())
            {
                train = dc.Trains.FirstOrDefault(x => x.TrainId == trainId);
            }
            return train;
        }

        public static void editTrain(Train newTrain)
        {
            using (var dc = new DALDataContext())
            {
                //dc.Trains.ed
                dc.SubmitChanges();
            }
        }

        public static List<Train> getAllTrains()
        {
            using(var db = new DALDataContext())
            {
                List<Train> trains = db.Trains.AsEnumerable().ToList();
                return trains;
            }
        }
    }
}