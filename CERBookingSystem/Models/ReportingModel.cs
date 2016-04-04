using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERBookingSystem.Models
{

    public class BookTicketReport
    {
        public List<BookedTicketsDetails> ListOfBookedTickets { get; set; }
        public List<CancelledTicketsDetails> ListOfCancelledTickets { get; set; }

    }
    public class BookedTicketsDetails
    {

        public int numberOfTicketsBooked { get; set; }

        public DateTime dateOfBooking { get; set; }


    }

    public class CancelledTicketsDetails
    {

        public int numberOfTicketsCancelled { get; set; }

        public DateTime dateOfCancellation { get; set; }


    }
}