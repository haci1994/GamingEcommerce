using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class OrderService : GenericService<Order, CreateOrderViewModel, UpdateOrderViewModel, OrderViewModel>, IOrderService
    {
        public OrderService(IRepository<Order> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
