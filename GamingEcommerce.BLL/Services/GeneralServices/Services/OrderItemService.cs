using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices.Services
{
    public class OrderItemService : GenericService<OrderItem, CreateOrderItemViewModel, UpdateOrderItemViewModel, OrderItemViewModel>, IOrderItemService
    {
        public OrderItemService(IRepository<OrderItem> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
