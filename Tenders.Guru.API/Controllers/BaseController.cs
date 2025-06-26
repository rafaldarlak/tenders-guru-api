using MediatR;

namespace Tenders.Guru.API.Controllers;

public abstract class BaseController
{
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}