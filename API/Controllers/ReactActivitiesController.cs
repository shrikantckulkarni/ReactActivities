using System;
using Application.Activities.Command;
using Application.Activities.DTOs;
using Application.Activities.Queries;
using Application.ReactActivities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace API.Controllers;

public class ReactActivitiesController() : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ReactActivity>>> GetReactActivites()
    {
        return await Mediator.Send(new GetReactActivityList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReactActivity>> GetReactActivity(string id)
    {
        return HandleResult(await Mediator.Send(new GetReactActivityDetail.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateReactActivity(CreateReactActivityDto reactActivityDto)
    {
        return HandleResult(await Mediator.Send(new CreateReactActivity.Command { ReactActivityDto = reactActivityDto }));
    }

    [HttpPut]
    public async Task<ActionResult> EditReactActivity(EditReactActivityDto reactActivity)
    {
        return HandleResult(await Mediator.Send(new EditReactActivity.Command { ReactActivityDto = reactActivity }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReactActivity(string Id)
    {
        return HandleResult(await Mediator.Send(new DeleteReactActivity.Command { Id = Id }));
    }

}
