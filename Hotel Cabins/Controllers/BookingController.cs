using AutoMapper;
using Hotel_Cabins.DTOs.BookingDTOs;
using Hotel_Cabins.DTOs.GuestDOTs;
using Hotel_Cabins.Models;
using Hotel_Cabins.Repository.IRepository;
using Hotel_Cabins.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Hotel_Cabins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        internal APIResponse res;
        private readonly IBookingRepository _dbBooking;

        public BookingController(IMapper mapper , IBookingRepository dbBooking)
        {
            _mapper = mapper;
            _dbBooking = dbBooking;
            res = new();
        }



        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllBookings(int page = 1, int pageSize = 10)
        {
            var bookings =await _dbBooking.GetAllAsync(page: page, pageSize: pageSize, include: "Guest,Cabin");

            var pagination = new Pagination()
            {
                Page = page,
                PageSize = pageSize,
            };

            Response.Headers.Add("x-Pagination", JsonSerializer.Serialize(pagination));

            res.Result = _mapper.Map<List<BookingDTO>>(bookings);
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }

        [HttpGet("{id:int}", Name = "GetBooking")]
        public async Task<ActionResult<APIResponse>> GetBooking(int id)
        {
            var bookingModel = await _dbBooking.GetOneAsync(g => g.Id == id , include:"Guest,Cabin");

            if (bookingModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);


            res.Result = _mapper.Map<BookingDTO>(bookingModel);
            res.StatusCode = HttpStatusCode.OK;

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateGuest(BookingCreateDTO bookingCreateDTO)
        {
            var bookingModel = _mapper.Map<Booking>(bookingCreateDTO);

            await _dbBooking.CreateOneAsync(bookingModel);

            var booking = _mapper.Map<BookingDTO>(bookingModel);

            res.Result = booking;
            res.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetGuest", new { id = bookingModel.Id }, res);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteBooking(int id)
        {
            var bookingModel = await _dbBooking.GetOneAsync(g => g.Id == id);

            if (bookingModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);

            await _dbBooking.DeleteOneAsync(bookingModel);


            res.Result = "Booking is deleted";
            res.StatusCode = HttpStatusCode.Created;

            return Ok(res);
        }


        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateGuest(BookingUpdateDTO bookingUpdateDTO)
        {
            var bookingModel = await _dbBooking.GetOneAsync(b => b.Id == bookingUpdateDTO.Id, tracked: false);

            if (bookingModel is null) throw new AppError("No Guest Found With This Id", HttpStatusCode.NotFound);

            var booking = _mapper.Map<Booking>(bookingUpdateDTO);

            await _dbBooking.UpdateOneAsync(booking);

            res.StatusCode = HttpStatusCode.OK;
            res.Result = _mapper.Map<BookingDTO>(booking);

            return CreatedAtRoute("GetBooking", new { id = bookingModel.Id }, res);
        }
    }
}
