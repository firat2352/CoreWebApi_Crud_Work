using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService; //dependency injection
        }

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels); //response code:200 ,add data to body
        }

        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel= await _hotelService.GetHotelByName(name);
            if(hotel!=null)
            {
                return Ok(hotel);
            }

            return NotFound();//404
        }

         [HttpGet]
         [Route("[action]/{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel= await _hotelService.GetHotelByID(id);
            if(hotel!=null)
            {
                return Ok(hotel);
            }

            return NotFound();//404
        }

        /// <summary>
        /// Create an Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Hotel hotel)
        {           
                var createdHotel = await _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); //201 +data

        }

        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Hotel hotel)
        {
            if(await _hotelService.GetHotelByID(hotel.Id)!=null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel)); //200+data
            }
            return NotFound();

        }

        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _hotelService.GetHotelByID(id) != null)
            {
                await _hotelService.DeleteHotel(id);
                return Ok(); //200+data
            }

            return NotFound();
        }
    }
}
