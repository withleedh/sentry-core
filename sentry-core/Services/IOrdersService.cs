using sentry_core.DTOs;

namespace sentry_core.Services;

public interface IOrdersService
{
    Task<object> CreateAsync(CreateOrderDto createOrderDto);
}