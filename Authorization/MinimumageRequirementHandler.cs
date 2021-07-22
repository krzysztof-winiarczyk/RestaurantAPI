using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumageRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirement> _logger;

        public MinimumageRequirementHandler(ILogger<MinimumAgeRequirement> logger)
        {
            _logger = logger;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (context.User.FindFirst(c => c.Type == "DateOfBirth") is null)
            {
                throw new ForbidException("Unauthorized access");
            }

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User {userEmail} with date of birth: [{dateOfBirth}] ");

            if(dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                _logger.LogInformation("Authorization succeded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }

            return Task.CompletedTask;
        }
    }
}
