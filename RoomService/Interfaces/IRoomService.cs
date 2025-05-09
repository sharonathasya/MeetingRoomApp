using RoomService.ViewModels;

namespace RoomService.Interfaces
{
    public interface IRoomService
    {
        Task<RoomRes> AddRoom(ReqRoom request);
        Task<RoomRes> EditRoom(ReqRoom request);
        Task<RoomRes> DeleteRoom(ReqIdRoom request);
        Task<RoomRes> GetRoomById(ReqIdRoom request);
        Task<RoomResList> GetRoomByName(ReqIdRoom request);
        Task<RoomResList> GetRoomList(ReqIdRoom request);
    }
}
