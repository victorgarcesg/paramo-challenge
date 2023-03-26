using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Base controller class that inherits from ControllerBase and provides access to IMediator.
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Provides access to the IMediator service by getting it from the current HttpContext request services.
    /// </summary>
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}