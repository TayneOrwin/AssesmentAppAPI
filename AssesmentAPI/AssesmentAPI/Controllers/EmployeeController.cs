using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssesmentAPI.Models;
using AssesmentAPI.Models.Employee;
using AssesmentAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AssesmentAPI.ViewModel;
using AssesmentAPI.Models.Manager;
using AssesmentAPI.Models.AccessRole;

namespace AssesmentAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IAccessRoleRepository _accessRoleRepository;
        private readonly AppDbContext _appDbContext = new AppDbContext();

        public EmployeeController(IEmployeeRepository employeeRepository, IAccessRoleRepository accessRoleRepository, IManagerRepository managerRepository)
        {
            _employeeRepository = employeeRepository;
            _managerRepository = managerRepository;
            _accessRoleRepository = accessRoleRepository;
        }






        [HttpGet]
        [Route("GetAllEmployees")]

        public async Task<ActionResult> GetAllEmployeesAsync()
        {


            try
            {
                var results = await _employeeRepository.getAllEmployeesAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }




        [HttpPost]
        [Route("AddEmployee")]

        public async Task<ActionResult> AddEmployee(EmployeeViewModel evm)
        {

            var employee = new Models.Entities.Employee { name = evm.name, surname = evm.surname, salary = evm.salary,dob = evm.dob, ProfileImage= evm.ProfileImage,ManagerID=evm.ManagerID,AccessRoleID=evm.AccessRoleID };

            try
            {
                _employeeRepository.Add(employee);
                await _employeeRepository.SaveChangesAsync();

            }
            catch (Exception err)
            {
                return Ok(err); ;
            }

            return Ok();
        }


        [HttpPut]
        [Route("UpdateEmployee")]

        public async Task<ActionResult> UpdateEmployee(int id, EmployeeViewModel evm)
        {

            try
            {
                var existingEmployee = await _employeeRepository.getEmployeeAsync(id);


                if (existingEmployee == null) return NotFound("Could not find employee");

                existingEmployee.name = evm.name;
                existingEmployee.surname = evm.surname;
                existingEmployee.salary = evm.salary;
                existingEmployee.AccessRoleID = evm.AccessRoleID;
                existingEmployee.ProfileImage = evm.ProfileImage;
                existingEmployee.ManagerID = evm.ManagerID;
                existingEmployee.dob = evm.dob;

                  

                 


                if (await _employeeRepository.SaveChangesAsync())
                {
                    return Ok("event updated successfully");
                }

            }

            catch (Exception)
            {
                return BadRequest("Error");
            }

            return Ok("Success");

        }


        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var existingEmployee = await _employeeRepository.getEmployeeAsync(id);
                if (existingEmployee == null) return NotFound();


                _employeeRepository.Delete(existingEmployee);

                if (await _employeeRepository.SaveChangesAsync())
                {
                    return Ok("event deleted successfully");
                }


            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

            return BadRequest();
        }



        [HttpGet]
        [Route("SearchEmployee")]
        public async Task<ActionResult<IEnumerable<Models.Entities.Employee>>> Search(string name) // IEnumerable used for iterating through collection of a type??
        {
            try
            {
                var result = await _employeeRepository.Search(name);

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

        [HttpGet]
        [Route("GetIdByFullname")]

        public async Task<IActionResult> GetIdByFullname(string name, string surname)
        {

            var results = await _employeeRepository.getIdByFullname(name, surname);

            try
            {
                return Ok(results);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }


        }


        [HttpGet]
        [Route("GetManagerEmployees")]
        public async Task<ActionResult> GetManagerEmployeesAsync(int id)
        {
            try
            {
                var results = await _employeeRepository.getAllEmployeesForManager(id);
                return Ok(results);
            }




            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Service Error");
            }

        }



        [HttpGet]
        [Route("GetEmployeeById")]

        public async Task<IActionResult> GetEmployeeById(int id)
        {

            var results = await _employeeRepository.getEmployeeAsync(id);

            try
            {
                return Ok(results);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }


        }




        [HttpGet]
        [Route("GetIdByAccessRole")]

        public async Task<IActionResult> GetIdByAccessRole(string role)
        {

            var results = await _accessRoleRepository.getIdByAccessRole(role);

            try
            {
                return Ok(results);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }


        }




        [HttpGet]
        [Route("GetIdByManager")]

        public async Task<IActionResult> GetIdByManager(string name)
        {

            var results = await _managerRepository.getIdByName(name);

            try
            {
                return Ok(results);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }


        }









    }
}

