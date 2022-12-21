using AutoMapper;
using Demo.Payments.Api.Infra.DbContext.Models;
using Demo.SharedModel.Models;

namespace Demo.Payments.Api.Infra.DbContext.Mapper
{
    public class DynamoModelProfile : Profile
    {
        public DynamoModelProfile()
        {
            CreateMap<Payment, PaymentDbModel>()
                .ForMember(dest => dest.PartitionKey, exp => exp.MapFrom(src => src.Id))
                .ForMember(dest => dest.SortKey, exp => exp.MapFrom(src => src.Id));
        }
    }
}