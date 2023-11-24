﻿using CitasApp.DTOs;
using CitasApp.Entities;

namespace CitasApp.Interfaces;
public interface IUserRepository
{
    Task<AppUser> GetUserByIdAsync (int id);
    Task<AppUser> GetUserByUsernameAsync(string username);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<bool> SaveAllAsync();
    void Update(AppUser user);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberAsync(string username);
}
