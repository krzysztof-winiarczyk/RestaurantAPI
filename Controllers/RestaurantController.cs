using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]   //path to the controller
    [ApiController] //this is called attribute
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController (IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
             _restaurantService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")] //authorization by role
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            //checking if data is not valid
            //  this block is not necessarry since adding attribute "[ApiController]"
            //  it is called automathically when some validation exists
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            HttpContext.User.IsInRole("Admin");

            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _restaurantService.Update(id, dto);

            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "HasNationality")]
        public ActionResult <IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult <RestaurantDto> Get([FromRoute] int id)
        {
            var restaurantDto = _restaurantService.GetById(id);

            if (restaurantDto is null)
            {
                return NotFound();
            }

            return Ok(restaurantDto);
        }


    }
}
