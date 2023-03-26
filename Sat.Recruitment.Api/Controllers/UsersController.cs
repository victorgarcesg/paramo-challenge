using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.User.Add;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    /// <summary>
    /// Controller for user-related operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : BaseController
    {
        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="request">Request containing user information to be added.</param>
        /// <returns>Result of the operation, with added user information if successful.</returns>
        [HttpPost]
        public async Task<IOperationResult<UserDto>> Add(AddUserRequest request) => await Mediator.Send(request);
    }
}
