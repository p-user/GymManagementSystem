﻿global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Shared.Data.Interceptors;
global using Shared.DDD;
global using Attendance.Models;
global using Shared.Constants;
global using Attendance.Attendance.Dtos;
global using Attendance.Data;
global using MediatR;
global using Shared.Results;
global using Membership.Contracts.Membership.Features.Member;
global using MassTransit;
global using Microsoft.Extensions.Logging;
global using Shared.Messaging.IntegrationEvents;
global using Attendance.Attendance.Events.EventTypes;
global using Attendance.Contracts.Attendance.ModuleErrors;