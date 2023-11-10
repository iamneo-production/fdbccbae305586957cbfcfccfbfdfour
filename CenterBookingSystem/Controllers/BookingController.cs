using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using CenterBookingSystem.Models;
using CenterBookingSystem.Data;
using CenterBookingSystem.Models;

namespace CenterBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        // Write your BookingController here...
        // Index() - returns view
        // Create(int spaceId, DateTime eventDate, TimeSpan timeSlot, string organizerID) 
        //      - Handle EventBookingException "Space is not available for the selected date."
        //      - return RedirectTo "Confirmation" page with BookingID
        // Confirmation(int bookingId) - returns View(booking)
    }
}
