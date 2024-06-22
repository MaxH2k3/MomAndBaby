using AutoMapper;
using MomAndBaby.Entity;
using MomAndBaby.Models.MessageModel;

namespace MomAndBaby.Configuration.ServiceConfig
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Message, MessageDTO>().ReverseMap();
		}
	}
}
