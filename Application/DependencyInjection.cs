﻿using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IFollowingService, FollowingService>();
            services.AddScoped<IInterestsService, InterestsService>();
            return services;
        }
    }
}
