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
    public class RoomAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, Room>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RoomAuthorizationCrudHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       Room resource)
        {
            string userId =_userManager.GetUserId(context.User);

            if (userId.Equals(resource.UserId))
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
        }
    }
}
