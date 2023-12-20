using API.Data;
using API.DIOs;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[AllowAnonymous]

public class UsersController : BaseApiController
{
    private readonly UserRepository _userRepository;
    private readonly IUserRepository userRepository;

    private readonly IMapper _mapper;

     public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;

        _mapper = mapper;

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        // var users = await userRepository.GetUsersAsync();
        // return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
        return Ok(await userRepository.GetMembersAsync());
    }

    [HttpGet("{id}")]
     public async Task<ActionResult<MemberDto?>> GetUser(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return _mapper.Map<MemberDto>(user);
    }


    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto?>> GetUserByUserName(string username)
    {
        // var user = await userRepository.GetUserByUserNameAsync(username);
        // return _mapper.Map<MemberDto>(user);

        return await userRepository.GetMemberByUserNameAsync(username);
    }
}