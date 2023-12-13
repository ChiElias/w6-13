﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[AllowAnonymous]
//[Authorize]
// [controller] = Users, (UsersController - Controller = User), ~route = /api/users
public class UsersController : BaseApiController
{
    private readonly DataContext _dataContext;
    //snipper: ctor
    //inject DataContext
    public UsersController(DataContext dataContext)
    {
        //putting cursor inside dataContext (ctor parameter) `ctrl+.` then select `create and assign feild`
        this._dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return await _dataContext.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser?>> GetUser(int id)
    {
        return await _dataContext.Users.FindAsync(id);
    }
}