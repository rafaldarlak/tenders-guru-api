using MediatR;
using Tenders.Guru.API.Models;

namespace Tenders.Guru.API.Pagination;

public interface IPagedQuery<TResponse> : IRequest<IPagedResponse<TResponse>>
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
}