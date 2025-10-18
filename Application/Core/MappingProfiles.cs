using System;
using Application.Activities.DTOs;
using Application.Profiles.DTOs;
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
                CreateMap<ReactActivity, ReactActivityDto>()
                .ForMember(t => t.HostDisplayName, s => s.MapFrom(x => x.Attendees.FirstOrDefault(y => y.IsHost)!.User.DisplayName))
                .ForMember(t => t.HostId, s => s.MapFrom(x => x.Attendees.FirstOrDefault(y => y.IsHost)!.User.Id));
                CreateMap<ActivityAttendee, UserProfile>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.User.DisplayName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.User.Bio))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.User.ImageUrl))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.User.Id));
}
}
