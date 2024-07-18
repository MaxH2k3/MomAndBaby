using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using MomAndBaby.BusinessObject.Models.ProductDto;

namespace MomAndBaby.Service.Configuration
{
    public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Message, MessageDTO>().ReverseMap();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<ProductDto, CartSessionModel>().ReverseMap();

			CreateMap<ProductDto, Product>()
				.ForPath(dest => dest.CategoryNavigation, opt => opt.Ignore())
				.ForPath(dest => dest.Statistic, opt => opt.Ignore());

			CreateMap<Product, ProductPreview>()
				.ForMember(x => x.CategoryId, opt => opt.MapFrom(src => src.CategoryNavigation!.Id))
				.ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.CategoryNavigation!.Name))
				.ForMember(x => x.TotalPurchase, opt => opt.MapFrom(src => src.Statistic.TotalPurchase))
				.ForMember(x => x.TotalReview, opt => opt.MapFrom(src => src.Statistic.TotalReview))
				.ForMember(x => x.AverageStar, opt => opt.MapFrom(src => src.Statistic.AverageStar))
				.ReverseMap();


            CreateMap<Order, OrderResponseModel>()
				.ForMember(x => x.StatusName, opt => opt.MapFrom(src => src.Status.Name))
			.ReverseMap();
			CreateMap<OrderDetail, OrderDetailResponseModel>()
				.ForMember(x => x.OrderId, opt => opt.MapFrom(src => src.Order.Id))
				.ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product.Name))
				.ForMember(x => x.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
				.ReverseMap();


			//CreateMap<IEnumerable< ProductCategoryDto>, IEnumerable<Category>>().ReverseMap();
            CreateMap<Category, ProductCategoryDto>().ReverseMap();

			
        }
	}
}
