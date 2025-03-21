﻿global using Authentication.Data;
global using Duende.IdentityModel.Client;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Authentication.Data.Seed;
global using Authentication.Models;
global using Authentication.Services;
global using Duende.IdentityServer.Services;
global using Duende.IdentityServer.Validation;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.Extensions.DependencyInjection;
global using Shared.Data.Interceptors;
global using Shared.Data.Seed;
global using Authentication.Authentication.Dtos;
global using MediatR;
global using Authentication.Authentication.Features.ActivationLink;
global using Authentication.Contracts.Authentication.Dtos;
global using Authentication.Contracts.Authentication.Features;
global using Authentication.Authentication.Features.RegisterUser;
global using Membership.Contracts.Membership.Dtos;
global using StaffManagement.Contracts.StaffManagement.Dtos;