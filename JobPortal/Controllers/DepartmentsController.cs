using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using System.Net;
using JobPortal.WebModels.DepartmentWebModels;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DepartmentsController : ControllerBase
    {
        private readonly SQLServerDBContext _context;

        public DepartmentsController(SQLServerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/v1/Departments
        /// Get all Departments
        /// </summary>
        /// <returns>List of Departments</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDepartmentResponseWebModel>>> GetDepartments()
        {
            try
            {
                var result = await _context.Departments.Select(dept => new GetDepartmentResponseWebModel
                            {
                                Id = dept.DepartmentId,
                                Title = dept.Title,
                            }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving department list");
            }            
        }

        /// <summary>
        /// GET: api/v1/Departments/1
        /// Get Department by ID  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Department object</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetDepartmentResponseWebModel>> GetDepartment(int id)
        {
            try
            {
                var dept = await _context.Departments.FindAsync(id);

                if (dept == null)
                {
                    return NotFound($"Department with ID = {id} not found");
                }

                return new GetDepartmentResponseWebModel
                {
                    Id = dept.DepartmentId,
                    Title = dept.Title,
                };
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error retrieving department");
            }
        }


        /// <summary>
        /// Put: api/v1/Departments/1
        /// Update Departments by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDept"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDepartment(int id, UpdateDeptRequestWebModel updateDept)
        {
            try
            {
                var deptToUpdate = await _context.Departments.FindAsync(id);

                if (deptToUpdate == null)
                    return NotFound($"Department with ID = {id} not found");

                deptToUpdate.Title = updateDept.Title;
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error updating department");
            }
        }

        // POST: api/Departments
        /// <summary>
        /// POST: api/Departments
        /// Create Departments
        /// </summary>
        /// <param name="createDept"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(CreateDeptRequestWebModel createDept)
        {
            try
            {
                var newDept = new Department
                {
                    Title = createDept.Title,
                };

                _context.Departments.Add(newDept);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDepartment), new { id = newDept.DepartmentId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error creating department");
            }
        }


        /// <summary>
        /// DELETE: api/Departments/5
        /// Delete Departments by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var deptToDelete = await _context.Departments.FindAsync(id);
                if (deptToDelete == null)
                {
                    return NotFound($"Department with ID = {id} not found");
                }

                _context.Departments.Remove(deptToDelete);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error deleting department");
            }
        }
    }
}
