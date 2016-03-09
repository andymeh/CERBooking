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
        public void addTrain(Train newTrain)
        {
            using (var dc = new DALDataContext())
            {
                dc.Trains.InsertOnSubmit(newTrain);
                dc.SubmitChanges();
            }
        }

        public Train getTrain(int trainId)
        {
            Train train = new Train();
            using (var dc = new DALDataContext())
            {
                train = dc.Trains.FirstOrDefault(x => x.TrainId == trainId);
            }
            return train;
        }

        public void editTrain(Train newTrain)
        {
            using (var dc = new DALDataContext())
            {
                //dc.Trains.ed
                dc.SubmitChanges();
            }
        }


    }
}