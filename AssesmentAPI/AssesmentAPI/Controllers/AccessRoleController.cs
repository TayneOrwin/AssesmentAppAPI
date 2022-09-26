using AssesmentAPI.Models.AccessRole;
using AssesmentAPI.Models.Entities;
using AssesmentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPIWalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRoleController : ControllerBase
    {
        private readonly IAccessRoleRepository _roleRepository;
        //return data from the database

        //dependency injection
        public AccessRoleController(IAccessRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<ActionResult> GetAllRolessAsync()
        {
            try
            {
                var results = await _roleRepository.getAllRolesAsync();
                return Ok(results);
            }




            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Service Error");
            }

        }


        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(AccessRoleViewModel avm)
        {

            var Tevent = new AccessRole { RoleDescription = avm.roleDescription };

            try
            {
                _roleRepository.Add(Tevent);
                await _roleRepository.SaveChangesAsync();
            }




            catch (Exception)
            {
                return BadRequest("Error");
            }

            return Ok("Success");



        }



        [HttpPut]
        [Route("UpdateRole")]

        public async Task<ActionResult> UpdateRole(string name, AccessRoleViewModel avm)
        {



            try
            {
                var existingRole = await _roleRepository.getRoleAsync(name);

                if (existingRole == null) return NotFound("Could not find Role: " + name);


                existingRole.RoleDescription = avm.roleDescription;



                if (await _roleRepository.SaveChangesAsync())
                {
                    return Ok("Role updated successfully");
                }


            }




            catch (Exception)
            {
                return BadRequest("Error");
            }

            return Ok("Success");









        }





        [HttpDelete]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string Name)
        {
            try
            {
                var existingRole = await _roleRepository.getRoleAsync(Name);
                if (existingRole == null) return NotFound();


                _roleRepository.Delete(existingRole);

                if (await _roleRepository.SaveChangesAsync())
                {
                    return Ok("Role deleted successfully");
                }


            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

            return BadRequest();
        }



        [HttpGet]
        [Route("SearchRole")]
        public async Task<ActionResult<IEnumerable<AccessRole>>> Search(string name) // IEnumerable used for iterating through collection of a type??
        {
            try
            {
                var result = await _roleRepository.Search(name);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Could not find the requested guest");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data from the database");
            }



        }


    }




}

