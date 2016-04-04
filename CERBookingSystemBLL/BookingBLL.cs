using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystemBLL
{
    public class BookingBLL
    {
        public static void addBooking(Booking newBooking)
        {
            using (var dc = new DALDataContext())
            {
                dc.Bookings.InsertOnSubmit(newBooking);
                dc.SubmitChanges();
            }
        }

        public static Booking getBooking(int bookingId)
        {
            Booking booking = new Booking();
            using (var dc = new DALDataContext())
            {
                booking = dc.Bookings.FirstOrDefault(x => x.BookingId == bookingId);
            }
            return booking;
        }

        public static List<Booking> getAllBookingsForUser(string UserId)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.UserId.Equals(UserId)).ToList();
                return userBookings;
            }
        }

        public static int numberCancelledBookings(DateTime startDate)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.statusOfBooking == "Cancelled" && x.DateCancelled.Value.Date == startDate.Date).ToList();
                return userBookings.Count;
            }
        }

        public static int numberOfActiveBookings(DateTime startDate)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.statusOfBooking == "Active" && x.DateBooked.Date == startDate.Date).ToList();
                return userBookings.Count;
            }
        }
        public static int numberOfPendingBookings(DateTime startDate, DateTime endDate)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.statusOfBooking == "Pending" && x.DateBooked.Date >= startDate.Date).ToList();
                return userBookings.Count;
            }
        }
    }
}
