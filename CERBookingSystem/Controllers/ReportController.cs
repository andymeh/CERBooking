using CERBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystemBLL;

namespace CERBookingSystem.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            BookTicketReport ticketReport = getTicketReport(DateTime.Now.AddDays(-14), DateTime.Now);
            return View(ticketReport);
        }

        public BookTicketReport getTicketReport(DateTime from, DateTime to)
        {
            BookTicketReport ticketReport = new BookTicketReport();
            ticketReport.ListOfBookedTickets = new List<BookedTicketsDetails>();
            ticketReport.ListOfCancelledTickets = new List<CancelledTicketsDetails>();
            while (from <= to)
            {
                int activeBookings = BookingBLL.numberOfActiveBookings(from);
                ticketReport.ListOfBookedTickets.Add(new BookedTicketsDetails
                {
                    dateOfBooking = from,
                    numberOfTicketsBooked = activeBookings
                });
                int cancelledBooking = BookingBLL.numberCancelledBookings(from);
                ticketReport.ListOfCancelledTickets.Add(new CancelledTicketsDetails
                {
                    dateOfCancellation = from,
                    numberOfTicketsCancelled = cancelledBooking
                });
                from = from.AddDays(1);
            }
            ticketReport.ListOfBookedTickets.OrderBy(x => x.dateOfBooking);
            ticketReport.ListOfCancelledTickets.OrderBy(x => x.dateOfCancellation);
            return ticketReport;
        }




    }
}