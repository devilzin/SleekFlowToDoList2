using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SleekFlowToDoList2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SleekFlowToDoList2.Controllers
{
    [Route("api/HomeController")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ITODORepository todoRepository;
        public HomeController(ITODORepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        //Search Task by Name, Desc and sorting
        [Route("SearchByNameDesc")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TODO>>> Search(string FilterString, string sortOrder)
        {
            try
            {
                var result = await todoRepository.Search(FilterString, sortOrder);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data from the database: " + ex.Message);
            }
        }
        //Get all task list
        [Route("GetTODOList")]
        [HttpGet]
        public async Task<ActionResult> GetTODOs()
        {
            try
            {
                return Ok(await todoRepository.GetTODOs());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data from the database" + ex.Message);
            }
        }
        //Get Task by ID
        [Route("GetTODOListByID")]
        [HttpGet]
        public async Task<ActionResult<TODO>> GetTODO(int ID)
        {
            try
            {
                var result = await todoRepository.GetTODO(ID);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data from the database" + ex.Message);
            }
        }
        //Add new Task
        [Route("AddTODOList")]
        [HttpPost]
        public async Task<ActionResult<TODO>> AddTODO(TODO todo)
        {
            try
            {
                if (todo == null)

                    return BadRequest();

                var tdo = await todoRepository.GetTODO(todo.ID);

                if (tdo != null)
                {
                    ModelState.AddModelError("ID", "ID already in use");
                    return BadRequest(ModelState);
                }

                var createdTODO = await todoRepository.AddTODO(todo);

                return CreatedAtAction(nameof(GetTODO),
                    new { id = createdTODO.ID }, createdTODO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating data to database" + ex.Message);
            }
        }
        //Update Task
        [Route("UpdateTODOList")]
        [HttpPut]
        public async Task<ActionResult<TODO>> UpdateTODO(TODO todo)
        {
            try
            {
                var TODOToUpdate = await todoRepository.GetTODO(todo.ID);

                if (TODOToUpdate == null)
                {
                    return NotFound($"Task Name with ID = {todo.ID} not found");
                }
                return await todoRepository.UpdateTODO((todo));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating data to database" + ex.Message);
            }
        }
        //Delete task
        [Route("DeleteTODOList")]
        [HttpDelete]
        public async Task<ActionResult<TODO>> DeleteTODO(int ID)
        {
            try
            {
                var TODOToDelete = await todoRepository.GetTODO(ID);

                if (TODOToDelete == null)
                {
                    return NotFound($"Task Name with ID = {ID} not found");
                }

                await todoRepository.DeleteTODO((ID));

                return Ok($"Name with ID = {ID} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting data from database" + ex.Message);
            }
        }
    }
}
