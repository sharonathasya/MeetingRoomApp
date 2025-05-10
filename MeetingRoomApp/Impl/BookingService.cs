using BookingService.Interfaces;
using BookingService.ViewModels;
using Microsoft.EntityFrameworkCore;
using MeetingRoomApp.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MeetingRoomAppService.Helpers;

namespace BookingService.Impl
{
    public class BookingServices(dbContext context, ITokenManager tokenManager) : IBookingService
    {
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly dbContext _dbContext = context;

        #region Add Booking
        public async Task<BookingRes> AddBooking(ReqBooking request)
        {
            DateTime aDate = DateTime.Now;
            BookingRes BookingRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var checkdata = context.Booking.Where(x => x.MeetingRoomId == request.MeetingRoomId && x.StartBooking == request.StartBooking).FirstOrDefault();
                if (checkdata == null)
                {
                    Booking insertBooking = new Booking
                    {
                        UserId = int.Parse(currentUser.User_id),
                        MeetingRoomId = request.MeetingRoomId,
                        StartBooking = request.StartBooking,
                        EndBooking = request.EndBooking,
                        Description = request.Description,
                        Status = request.Status,
                        CreatedAt = aDate
                    };
                    context.Booking.Add(insertBooking);
                    context.SaveChanges();


                    BookingRes.STATUS = Constants.ResponseConstant.Success;
                    BookingRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    BookingRes.STATUS = Constants.ResponseConstant.Failed;
                    BookingRes.MESSAGE = Constants.ResponseConstant.BookingExist;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                BookingRes.STATUS = Constants.ResponseConstant.Failed;
                BookingRes.MESSAGE = errmsg;
            }

            return BookingRes;
        }
        #endregion

        #region Edit Booking
        public async Task<BookingRes> EditBooking(ReqBooking request)
        {
            DateTime aDate = DateTime.Now;
            BookingRes BookingRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var dataBooking = context.Booking.Where(x => x.Id == request.Id).FirstOrDefault();
                if (dataBooking != null)
                {
                    dataBooking.MeetingRoomId = request.MeetingRoomId;
                    dataBooking.StartBooking = request.StartBooking;
                    dataBooking.EndBooking = request.EndBooking;
                    dataBooking.Description = request.Description;
                    dataBooking.Status = request.Status;
                    context.Booking.Update(dataBooking);
                    context.SaveChanges();

                    BookingRes.STATUS = Constants.ResponseConstant.Success;
                    BookingRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    BookingRes.STATUS = Constants.ResponseConstant.Failed;
                    BookingRes.MESSAGE = Constants.ResponseConstant.SubmitFailed;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                BookingRes.STATUS = Constants.ResponseConstant.Failed;
                BookingRes.MESSAGE = errmsg;
            }

            return BookingRes;
        }
        #endregion

        #region Delete
        public async Task<BookingRes> DeleteBooking(ReqIdBooking request)
        {
            BookingRes BookingRes = new();
            DataBooking dataBooking = new();
            int BookingId = 0;
            if (request.Id != null)
            {
                BookingId = int.Parse(request.Id);
            }

            try
            {
                var getData = context.Booking.Where(x => x.Id == BookingId).FirstOrDefault();
                if (getData != null)
                {
                    context.Booking.Remove(getData);
                    context.SaveChanges();

                    BookingRes.STATUS = Constants.ResponseConstant.Success;
                    BookingRes.MESSAGE = Constants.ResponseConstant.DeleteSuccess;
                    BookingRes.RESULT = dataBooking;
                }
                else
                {
                    BookingRes.STATUS = Constants.ResponseConstant.NotFound;
                    BookingRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                BookingRes.STATUS = Constants.ResponseConstant.Failed;
                BookingRes.MESSAGE = errmsg;
            }
            return BookingRes;
        }
        #endregion

        #region Get Booking By Id
        public async Task<BookingRes> GetBookingById(ReqIdBooking request)
        {
            BookingRes BookingRes = new();
            DataBooking dataBooking = new();
            int BookingId = 0;
            if (request.Id != null) 
            { 
                BookingId = int.Parse(request.Id);
            }

            try
            {
                var getData = context.Booking.Where(x => x.Id == BookingId).FirstOrDefault();
                if (getData != null)
                {
                    dataBooking = new()
                    {
                        Id = getData.Id,
                        UserId = getData.UserId,
                        MeetingRoomId = getData.MeetingRoomId,
                        StartBooking = getData.StartBooking,
                        EndBooking = getData.EndBooking,
                        Description = getData.Description,
                        Status = getData.Status,
                        CreatedAt = getData.CreatedAt
                    };
                    BookingRes.STATUS= Constants.ResponseConstant.Success;
                    BookingRes.MESSAGE= Constants.ResponseConstant.DataFound;
                    BookingRes.RESULT = dataBooking;
                }
                else
                {
                    BookingRes.STATUS = Constants.ResponseConstant.NotFound;
                    BookingRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex) 
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                BookingRes.STATUS = Constants.ResponseConstant.Failed;
                BookingRes.MESSAGE = errmsg;
            }
            return BookingRes;
        }
        #endregion

       

        #region Get Booking List
        public async Task<BookingResList> GetBookingList(ReqIdBooking request)
        {
            BookingResList BookingRes = new();
            List<DataBooking> listBooking = [];

            try
            {
                var getData = context.Booking.ToList();
                if (getData != null && getData.Any())
                {
                    foreach (var result in getData)
                    {
                        DataBooking Booking = new()
                        {
                            Id = result.Id,
                            UserId = result.UserId,
                            MeetingRoomId = result.MeetingRoomId,
                            StartBooking = result.StartBooking,
                            EndBooking = result.EndBooking,
                            Description = result.Description,
                            Status = result.Status,
                            CreatedAt = result.CreatedAt
                        };
                        listBooking.Add(Booking);
                    };

                    BookingRes.STATUS = Constants.ResponseConstant.Success;
                    BookingRes.MESSAGE = Constants.ResponseConstant.DataFound;
                    BookingRes.RESULT = listBooking;
                }
                else
                {
                    BookingRes.STATUS = Constants.ResponseConstant.NotFound;
                    BookingRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                BookingRes.STATUS = Constants.ResponseConstant.Failed;
                BookingRes.MESSAGE = errmsg;
            }
            return BookingRes;
        }
        #endregion

    }
}
