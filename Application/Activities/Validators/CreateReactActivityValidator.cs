using System;
using System.Data;
using Application.Activities.Command;
using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class CreateReactActivityValidator :BaseReactActivityValidator<CreateReactActivity.Command,CreateReactActivityDto>
{
    public CreateReactActivityValidator():base(x=> x.ReactActivityDto)
    {
       
    }
}
