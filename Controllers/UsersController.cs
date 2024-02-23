﻿using AutoMapper;
using CitasApp.Data;
using CitasApp.DTOs;
using CitasApp.Entities;
using CitasApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CitasApp.Controllers;
[Authorize]
public class UsersController : BaseCont {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UsersController(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() {
        var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(await _userRepository.GetUsersAsync());
        return Ok(usersToReturn);
    }
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username){
        var user = await _userRepository.GetUserByUsernameAsync(username);

        return _mapper.Map<MemberDto>(user);
    }
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null) return NotFound();
        _mapper.Map(memberUpdateDto, user);
        if (await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("No se pudo realizar la operación");
    }
}