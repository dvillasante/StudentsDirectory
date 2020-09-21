using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace StudentsDirectory.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class StudentDirectoryController : ControllerBase
    {
        private IStudentsManager _studentManager;
        public StudentDirectoryController(IStudentsManager studentManager)
        {
            _studentManager = studentManager;
            _studentManager.LoadTestData();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost] //("create")
        public async Task<IActionResult> Create([FromBody]Student student)
        {
            if (student != null)
            {
                var result = await _studentManager.Create(student);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id }, result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut] //("update")
        public async Task<IActionResult> Update([FromBody]Student student)
        {
            if (student != null)
            {
                var result = await _studentManager.Update(student);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = Role.All)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id > 0)
            {
                var result = _studentManager.Get(id);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}