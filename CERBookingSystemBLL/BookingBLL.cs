using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystemBLL
{
    class BookingBLL
    {
        public void addBooking(Booking newBooking)
        {
            using (var dc = new DALDataContext())
            {
                dc.Bookings.InsertOnSubmit(newBooking);
                dc.SubmitChanges();
            }
        }

        public Booking getBooking(int bookingId)
        {
            Booking booking = new Booking();
            using (var dc = new DALDataContext())
            {
                booking = dc.Bookings.FirstOrDefault(x => x.BookingId == bookingId);
            }
            return booking;
        }

        public List<Booking> getAllBookingForUser(string UserId)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.UserId.Equals(UserId)).ToList();
                return userBookings;
            }
        }
    }
}
