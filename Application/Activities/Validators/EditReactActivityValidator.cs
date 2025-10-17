using System;
using Application.Activities.Command;
using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class EditReactActivityValidator : BaseReactActivityValidator<EditReactActivity.Command, EditReactActivityDto>
{
    public EditReactActivityValidator() : base(x=>x.ReactActivityDto)
    {
        RuleFor(x => x.ReactActivityDto.Id).NotEmpty().WithMessage("Id is required");
    }
}
