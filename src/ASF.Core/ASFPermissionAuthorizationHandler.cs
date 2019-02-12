﻿using ASF.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ASF
{
    public class ASFPermissionAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        public ASFPermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ILogger<ASFPermissionAuthorizationHandler> logger,
            IServiceProvider serviceProvider)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._logger = logger;
            this._serviceProvider = serviceProvider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var roles = httpContext.User.FindFirst("roles")?.Value;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                //验证登陆用户是否有权限
                var result = this._serviceProvider.GetRequiredService<AccountAuthorizationService>().Authentication(roles, httpContext.Request);
                var requestPath = httpContext.Request.PathBase + httpContext.Request.Path;
                if (result.Success)
                {
                    this._logger.LogInformation($"{requestPath} Permission authorization success");
                    httpContext.Items.Add("asf_parmission", result.Data);
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                else
                {
                    this._logger.LogWarning($"{requestPath} Permission authorization failed");
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}