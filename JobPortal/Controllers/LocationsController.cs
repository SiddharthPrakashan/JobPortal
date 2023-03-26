using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using JobPortal.WebModels.LocationWebModels;
using JobPortal.WebModels.DepartmentWebModels;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LocationsController : ControllerBase
    {
        private readonly SQLServerDBContext _context;

        public LocationsController(SQLServerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/v1/Locations
        /// Get all Locations
        /// </summary>
        /// <returns>List of Locations</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetLocationResponseWebModel>>> GetLocations()
        {
            try
            {
                var result = await _context.Locations.Select(loc => new GetLocationResponseWebModel
                {
                    Id = loc.LocationId,
                    Title = loc.Title,
                    City = loc.City,   
                    State = loc.State, 
                    Country = loc.Country,
                    Zip = loc.Zip,
                }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving location list");
            }
        }

        /// <summary>
        /// GET: api/v1/Locations/1
        /// Get Locations by ID  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Locations object</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetLocationResponseWebModel>> GetLocation(int id)
        {
            try
            {
                var loc = await _context.Locations.FindAsync(id);

                if (loc == null)
                {
                    return NotFound($"Location with ID = {id} not found");
                }

                return new GetLocationResponseWebModel
                {
                    Id = loc.LocationId,
                    Title = loc.Title,
                    City = loc.City,
                    State = loc.State,
                    Country = loc.Country,
                    Zip = loc.Zip,
                };
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving location");
            }
        }

        /// <summary>
        /// Put: api/v1/Locations/1
        /// Update Locations by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateLoc"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutLocation(int id, UpdateLocRequestWebModel updateLoc)
        {
            try
            {
                var locToUpdate = await _context.Locations.FindAsync(id);

                if (locToUpdate == null)
                    return NotFound($"Location with ID = {id} not found");

                locToUpdate.Title = updateLoc.Title;
                locToUpdate.City = updateLoc.City;
                locToUpdate.State = updateLoc.State;
                locToUpdate.Country = updateLoc.Country;
                locToUpdate.Zip = updateLoc.Zip;
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error updating location");
            }
        }

        // POST: api/Locations
        /// <summary>
        /// POST: api/Locations
        /// Create Locations
        /// </summary>
        /// <param name="createDept"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(CreateLocRequestWebModel createLoc)
        {
            try
            {
                var newLoc = new Location
                {
                    Title = createLoc.Title,
                    City = createLoc.City,
                    State = createLoc.State,
                    Country = createLoc.Country,
                    Zip = createLoc.Zip
                };

                _context.Locations.Add(newLoc);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLocation), new { id = newLoc.LocationId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating location");
            }
        }

        /// <summary>
        /// DELETE: api/Locations/5
        /// Delete Locations by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                var locToDelete = await _context.Locations.FindAsync(id);
                if (locToDelete == null)
                {
                    return NotFound($"Location with ID = {id} not found");
                }

                _context.Locations.Remove(locToDelete);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error deleting location");
            }
        }

    }
}
