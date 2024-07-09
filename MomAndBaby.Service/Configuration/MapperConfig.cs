using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.ProductDto;

namespace MomAndBaby.Service.Configuration
{
    public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Message, MessageDTO>().ReverseMap();
			CreateMap<Product, ProductDto>();
			CreateMap<ProductDto, Product>()
				.ForPath(dest => dest.CategoryNavigation, opt => opt.Ignore())
				.ForPath(dest => dest.Statistic, opt => opt.Ignore());
			CreateMap<Order, OrderResponseModel>()
				.ForMember(x => x.StatusName, opt => opt.MapFrom(src => src.Status.Name))
			.ReverseMap();
		}
	}
}
