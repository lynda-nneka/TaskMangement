using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;
using TaskManagement.Services.Infrastructure;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskItemService;

        public TaskController(ITaskService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpPost("create-new-task", Name = "Create-New-task")]
        [SwaggerOperation(Summary = "Creates task")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Id of created task", Type = typeof(ItemResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create task", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateTask([FromBody] ItemRequest request)
        {
            var response = await _taskItemService.CreateTask(request);
            return Ok(response);
        }


        [HttpPut("update-task/{Id}", Name = "Update task")]
        [SwaggerOperation(Summary = "Updates the task details")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "task updated successfully", Type = typeof(ItemResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "Update Unsuccessful", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateTask(string Id, [FromBody] UpdatetaskRequest request)
        {
            var response = await _taskItemService.UpdateTask(Id, request);
            return Ok(response);
        }


        [HttpGet("get-a-task/{Id}")]
        [SwaggerOperation(Summary = "Gets task with id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets task with id", Type = typeof(ItemResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "item does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ItemResponse>> GetTask(string Id)
        {
            var response = await _taskItemService.GetTask(Id);
            return Ok(response);
        }


       


        [HttpDelete("deletes-a-task/{Id}", Name = "deletes-a-candidate")]
        [SwaggerOperation(Summary = "deletes a task ")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "deleted succesfully", Type = typeof(ItemResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "task does not exist", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ItemResponse>> DeletesTask(string Id)
        {
            var response = await _taskItemService.DeleteTask(Id);
            return Ok(response);
        }
    }
}

