using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CERBookingSystemDAL;

namespace CERBookingSystemBLL
{
    public class TrainBLL
    {
        public void addTrain(Train newTrain)
        {
            using(var dc = new DALDataContext())
            {
                dc.Trains.InsertOnSubmit(newTrain);
                dc.SubmitChanges();
            }
        }


    }
}
