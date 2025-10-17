using System;
using Application.Activities.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
public MappingProfiles()
{
        CreateMap<ReactActivity, ReactActivity>();
        CreateMap<CreateReactActivityDto, ReactActivity>();
        CreateMap<EditReactActivityDto, ReactActivity>();
}
}
