// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Tenders.Guru.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Tenders.Guru.Application.Tenders.Queries;
//
// namespace Tenders.Guru.API.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class TendersController : BaseController
// {
//     public TendersController(IMediator mediator) : base(mediator)
//     {
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> GetTenders()
//     {
//         var result = await _mediator.Send(new GetTendersQuery());
//         return Ok(result);
//     }
//
//     [HttpGet("by-supplier/{supplierId}")]
//     public async Task<IActionResult> GetTendersBySupplierId(Guid supplierId)
//     {
//         var result = await _mediator.Send(new GetTendersBySupplierIdQuery(supplierId));
//         return Ok(result);
//     }
// }