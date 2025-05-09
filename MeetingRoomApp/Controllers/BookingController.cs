using BookingService.Interfaces;
using BookingService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController(IBookingService BookingService) : ControllerBase
    {
        public readonly IBookingService BookingService = BookingService;

        [Authorize(Roles = "admin, employee")]
        [HttpPost("AddBooking")]
        public async Task<BookingRes> AddBooking([FromBody] ReqBooking request)
        {
            return await BookingService.AddBooking(request);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost("EditBooking")]
        public async Task<BookingRes> EditBooking([FromBody] ReqBooking request)
        {
            return await BookingService.EditBooking(request);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost("DeleteBooking")]
        public async Task<BookingRes> DeleteBooking([FromBody] ReqIdBooking request)
        {
            return await BookingService.DeleteBooking(request);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost("GetBookingById")]
        public async Task<BookingRes> GetBookingById([FromBody] ReqIdBooking request)
        {
            return await BookingService.GetBookingById(request);
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPost("GetBookingList")]
        public async Task<BookingResList> GetBookingList([FromBody] ReqIdBooking request)
        {
            return await BookingService.GetBookingList(request);
        }

    }
}
