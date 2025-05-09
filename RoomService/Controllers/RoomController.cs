using RoomService.Impl;
using RoomService.Interfaces;
using RoomService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoomService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController(IRoomService RoomService) : ControllerBase
    {
        public readonly IRoomService RoomService = RoomService;

        [Authorize(Roles = "admin")]
        [HttpPost("AddRoom")]
        public async Task<RoomRes> AddRoom([FromBody] ReqRoom request)
        {
            return await RoomService.AddRoom(request);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("EditRoom")]
        public async Task<RoomRes> EditRoom([FromBody] ReqRoom request)
        {
            return await RoomService.EditRoom(request);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("DeleteRoom")]
        public async Task<RoomRes> DeleteRoom([FromBody] ReqIdRoom request)
        {
            return await RoomService.DeleteRoom(request);
        }

        [Authorize]
        [HttpPost("GetRoomById")]
        public async Task<RoomRes> GetRoomById([FromBody] ReqIdRoom request)
        {
            return await RoomService.GetRoomById(request);
        }

        [Authorize]
        [HttpPost("GetRoomByName")]
        public async Task<RoomResList> GetRoomByName([FromBody] ReqIdRoom request)
        {
            return await RoomService.GetRoomByName(request);
        }

        [Authorize]
        [HttpPost("GetRoomList")]
        public async Task<RoomResList> GetRoomList([FromBody] ReqIdRoom request)
        {
            return await RoomService.GetRoomList(request);
        }

    }
}
