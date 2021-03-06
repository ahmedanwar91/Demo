﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            this._context = context;

        }
        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetValues()
        {
            var values=_context.Employees.ToList();
            return Ok(values);
            
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"value{id}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
