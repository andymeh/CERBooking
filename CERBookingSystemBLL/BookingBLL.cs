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

        public List<Booking> getAllBookingsForUser(string UserId)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.UserId.Equals(UserId)).ToList();
                return userBookings;
            }
        }

        public int numberCancelledBookings(DateTime startDate, DateTime endDate)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.statusOfBooking == "Cancelled" && x.DateCancelled >= startDate && x.DateCancelled <= endDate).ToList();
                return userBookings.Count;
            }
        }

        public int numberOfActiveBookings(DateTime startDate, DateTime endDate)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.statusOfBooking == "Active" && x.DateBooked >= startDate && x.DateBooked <= endDate).ToList();
                return userBookings.Count;
            }
        }
        public int numberOfPendingBookings(DateTime startDate, DateTime endDate)
        {
            using (var dc = new DALDataContext())
            {
                List<Booking> userBookings = new List<Booking>();
                userBookings = dc.Bookings.Where(x => x.statusOfBooking == "Pending" && x.DateBooked >= startDate && x.DateBooked <= endDate).ToList();
                return userBookings.Count;
            }
        }
    }
}
