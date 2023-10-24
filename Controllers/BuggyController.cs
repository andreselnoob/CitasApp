﻿using CitasApp.Data;
using CitasApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers;
    public class BuggyController : BaseCont
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context) { 
            _context = context;
        }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult <string> GetSecret() {
        return "secreto de la api";
    }
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = _context.Users.Find(-1);
        if (thing == null) return NotFound();
        return thing;
    }
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.Users.Find(-1);
        var thingToReturn = thing.ToString();
        return thingToReturn;
    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("Usted ha solicitado algo de forma incorrecta");
    }
}
