﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProjectAPI.Data;
using AngularProjectAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace AngularProjectAPI.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly TwoHaxxContext _context;

        public RoleController(TwoHaxxContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpGet("role-of-type/{type}")]
        public async Task<ActionResult<Role>> RoleOfType(String type)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Name == type);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

    }*/
}
