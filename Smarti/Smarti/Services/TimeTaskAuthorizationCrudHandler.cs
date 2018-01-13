using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public class TimeTaskAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, TimeTask>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TimeTaskAuthorizationCrudHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       TimeTask resource)
        {
            string userId = _userManager.GetUserId(context.User);

            if (userId.Equals(resource.Socket.Room.UserId))
            {
                if (requirement.Name == Operations.Create.Name ||
                    requirement.Name == Operations.Read.Name ||
                    requirement.Name == Operations.Update.Name ||
                    requirement.Name == Operations.Delete.Name)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
