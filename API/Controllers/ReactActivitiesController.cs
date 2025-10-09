using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace API.Controllers;

public class ReactActivitiesController(AppDbContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ReactActivity>>> GetActivites()
    {
        return await context.ReactActivities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReactActivity>> GetActivity(string id) {
        var activity = await context.ReactActivities.FindAsync(id);
        if (activity == null) return NotFound();
        return activity;
    }

}
