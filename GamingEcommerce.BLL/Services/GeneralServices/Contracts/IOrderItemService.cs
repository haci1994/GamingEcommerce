using GamingEcommerce.BLL.ViewModels;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.Contracts
{
    public interface IOrderItemService : IGenericService<OrderItem, CreateOrderItemViewModel, UpdateOrderItemViewModel, OrderItemViewModel> { }
}
