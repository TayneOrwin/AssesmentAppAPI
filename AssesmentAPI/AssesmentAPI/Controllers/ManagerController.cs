using System;
using AssesmentAPI.Models;
using AssesmentAPI.Models.Employee;
using AssesmentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssesmentAPI.Models.Manager;
namespace AssesmentAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly AppDbContext _appDbContext = new AppDbContext();

        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }






        [HttpGet]
        [Route("GetAllManagers")]

        public async Task<ActionResult> GetAllManagersAsync()
        {


            try
            {
                var results = await _managerRepository.getAllManagersAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }




        [HttpPost]
        [Route("AddManager")]

        public async Task<ActionResult> AddManager(ManagerViewModel evm)
        {

            var employee = new Models.Entities.Manager { name = evm.name, surname = evm.surname, salary = evm.salary, dob = evm.dob, ProfileImage = evm.ProfileImage };

            try
            {
                _managerRepository.Add(employee);
                await _managerRepository.SaveChangesAsync();

            }
            catch (Exception err)
            {
                return Ok(err); ;
            }

            return Ok();
        }


        [HttpPut]
        [Route("UpdateManager")]

        public async Task<ActionResult> UpdateManager(int id, ManagerViewModel evm)
        {

            try
            {
                var existingEmployee = await _managerRepository.getManagerAsync(id);


                if (existingEmployee == null) return NotFound("Could not find employee");

                existingEmployee.name = evm.name;
                existingEmployee.surname = evm.surname;
                existingEmployee.salary = evm.salary;

                existingEmployee.ProfileImage = evm.ProfileImage;

                existingEmployee.dob = evm.dob;






                if (await _managerRepository.SaveChangesAsync())
                {
                    return Ok("manager updated successfully");
                }

            }

            catch (Exception)
            {
                return BadRequest("Error");
            }

            return Ok("Success");

        }


        [HttpDelete]
        [Route("DeleteManager")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            try
            {
                var existingEmployee = await _managerRepository.getManagerAsync(id);
                if (existingEmployee == null) return NotFound();


                _managerRepository.Delete(existingEmployee);

                if (await _managerRepository.SaveChangesAsync())
                {
                    return Ok("manager deleted successfully");
                }


            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

            return BadRequest();
        }




        


        [HttpGet]
        [Route("SearchManager")]
        public async Task<ActionResult<IEnumerable<Models.Entities.Manager>>> Search(string name) // IEnumerable used for iterating through collection of a type??
        {
            try
            {
                var result = await _managerRepository.Search(name);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Could not find the requested employee");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from the database");
            }

        }







    }






}

