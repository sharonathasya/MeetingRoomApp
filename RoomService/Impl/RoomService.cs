using RoomService.Interfaces;
using RoomService.ViewModels;
using Microsoft.EntityFrameworkCore;
using MeetingRoomApp.Data.Models;
using Microsoft.Extensions.Options;
using RoomService.Helpers;

namespace RoomService.Impl
{
    public class RoomServices(dbContext context, ITokenManager tokenManager, IOptions<CredentialAttr> appSettings) : IRoomService
    {
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly dbContext _dbContext = context;
        private readonly CredentialAttr _appSettings = appSettings.Value;
        #region Add Room
        public async Task<RoomRes> AddRoom(ReqRoom request)
        {
            DateTime aDate = DateTime.Now;
            RoomRes RoomRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var checkdata = context.MeetingRoom.Where(x => x.RoomName == request.RoomName).FirstOrDefault();
                if (checkdata == null)
                {
                    MeetingRoom insertRoom = new MeetingRoom
                    {
                        RoomName = request.RoomName,
                        Description = request.Description,
                        CreatedAt = aDate
                    };
                    context.MeetingRoom.Add(insertRoom);
                    context.SaveChanges();


                    RoomRes.STATUS = Constants.ResponseConstant.Success;
                    RoomRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    RoomRes.STATUS = Constants.ResponseConstant.Failed;
                    RoomRes.MESSAGE = Constants.ResponseConstant.RoomNameExist;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                RoomRes.STATUS = Constants.ResponseConstant.Failed;
                RoomRes.MESSAGE = errmsg;
            }

            return RoomRes;
        }
        #endregion

        #region Edit Room
        public async Task<RoomRes> EditRoom(ReqRoom request)
        {
            DateTime aDate = DateTime.Now;
            RoomRes RoomRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var dataRoom = context.MeetingRoom.Where(x => x.Id == request.Id).FirstOrDefault();
                if (dataRoom != null)
                {
                    dataRoom.RoomName = request.RoomName;
                    dataRoom.Description = request.Description;
                    context.MeetingRoom.Update(dataRoom);
                    context.SaveChanges();

                    RoomRes.STATUS = Constants.ResponseConstant.Success;
                    RoomRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    RoomRes.STATUS = Constants.ResponseConstant.Failed;
                    RoomRes.MESSAGE = Constants.ResponseConstant.SubmitFailed;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                RoomRes.STATUS = Constants.ResponseConstant.Failed;
                RoomRes.MESSAGE = errmsg;
            }

            return RoomRes;
        }
        #endregion

        #region Delete
        public async Task<RoomRes> DeleteRoom(ReqIdRoom request)
        {
            RoomRes RoomRes = new();
            DataRoom dataRoom = new();
            int RoomId = 0;
            if (request.Id != null)
            {
                RoomId = int.Parse(request.Id);
            }

            try
            {
                var getData = context.MeetingRoom.Where(x => x.Id == RoomId).FirstOrDefault();
                if (getData != null)
                {
                    context.MeetingRoom.Remove(getData);
                    context.SaveChanges();

                    RoomRes.STATUS = Constants.ResponseConstant.Success;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DeleteSuccess;
                    RoomRes.RESULT = dataRoom;
                }
                else
                {
                    RoomRes.STATUS = Constants.ResponseConstant.NotFound;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                RoomRes.STATUS = Constants.ResponseConstant.Failed;
                RoomRes.MESSAGE = errmsg;
            }
            return RoomRes;
        }
        #endregion

        #region Get Room By Id
        public async Task<RoomRes> GetRoomById(ReqIdRoom request)
        {
            RoomRes RoomRes = new();
            DataRoom dataRoom = new();
            int RoomId = 0;
            if (request.Id != null) 
            { 
                RoomId = int.Parse(request.Id);
            }

            try
            {
                var getData = context.MeetingRoom.Where(x => x.Id == RoomId).FirstOrDefault();
                if (getData != null)
                {
                    dataRoom = new()
                    {
                        Id = getData.Id,
                        RoomName = getData.RoomName,
                        Description = getData.Description,
                        CreatedAt = getData.CreatedAt
                    };
                    RoomRes.STATUS= Constants.ResponseConstant.Success;
                    RoomRes.MESSAGE= Constants.ResponseConstant.DataFound;
                    RoomRes.RESULT = dataRoom;
                }
                else
                {
                    RoomRes.STATUS = Constants.ResponseConstant.NotFound;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex) 
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                RoomRes.STATUS = Constants.ResponseConstant.Failed;
                RoomRes.MESSAGE = errmsg;
            }
            return RoomRes;
        }
        #endregion

        
        #region Get Room By Name 
        public async Task<RoomResList> GetRoomByName(ReqIdRoom request)
        {
            RoomResList RoomRes = new();
            List<DataRoom> listRoom = [];

            try
            {
                var getData = context.MeetingRoom.Where(x => x.RoomName == request.RoomName);
                if (getData != null && getData.Any())
                {
                    foreach(var result in getData)
                    {
                        DataRoom Room = new()
                        {
                            Id = result.Id,
                            RoomName = result.RoomName,
                            Description = result.Description,
                            CreatedAt = result.CreatedAt
                        };
                        listRoom.Add(Room);
                    };

                    RoomRes.STATUS = Constants.ResponseConstant.Success;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DataFound;
                    RoomRes.RESULT = listRoom;
                }
                else
                {
                    RoomRes.STATUS = Constants.ResponseConstant.NotFound;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                RoomRes.STATUS = Constants.ResponseConstant.Failed;
                RoomRes.MESSAGE = errmsg;
            }
            return RoomRes;
        }
        #endregion

        #region Get Room List
        public async Task<RoomResList> GetRoomList(ReqIdRoom request)
        {
            RoomResList RoomRes = new();
            List<DataRoom> listRoom = [];

            try
            {
                var getData = context.MeetingRoom.ToList();
                if (getData != null && getData.Any())
                {
                    foreach (var result in getData)
                    {
                        DataRoom Room = new()
                        {
                            Id = result.Id,
                            RoomName = result.RoomName,
                            Description = result.Description,
                            CreatedAt = result.CreatedAt
                        };
                        listRoom.Add(Room);
                    };

                    RoomRes.STATUS = Constants.ResponseConstant.Success;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DataFound;
                    RoomRes.RESULT = listRoom;
                }
                else
                {
                    RoomRes.STATUS = Constants.ResponseConstant.NotFound;
                    RoomRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                RoomRes.STATUS = Constants.ResponseConstant.Failed;
                RoomRes.MESSAGE = errmsg;
            }
            return RoomRes;
        }
        #endregion

    }
}
