namespace MySpot.Application.Abstractions;

public interface IQueryHandler<in IQuery, TResult> where IQuery : class, IQuery<TResult>
{
    Task<TResult> HandleAsync(IQuery query);
}