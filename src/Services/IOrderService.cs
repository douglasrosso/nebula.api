using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrders(Guid userId);
        Task<OrderDto?> GetOrderById(Guid orderId, Guid userId);
        Task<OrderDto> Checkout(Guid userId);
    }
}
