using BookingService.ViewModels;

namespace BookingService.Interfaces
{
    public interface IBookingService
    {
        Task<BookingRes> AddBooking(ReqBooking request);
        Task<BookingRes> EditBooking(ReqBooking request);
        Task<BookingRes> DeleteBooking(ReqIdBooking request);
        Task<BookingRes> GetBookingById(ReqIdBooking request);
        Task<BookingResList> GetBookingList(ReqIdBooking request);
    }
}
