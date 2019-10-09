using System.Drawing;
using AutoMapper;
using ei_core.Entities.WireColorAggregate;
using ei_core.Interfaces;

namespace ei_web_api.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Color, string>().ConvertUsing<WebColorTypeConverter>();
            CreateMap<WireColor, WireColorViewModel>()
                .ForMember(dest => dest.BaseWebColor, opt => opt.MapFrom(src => src.BaseColor))
                .ForMember(dest => dest.StripeWebColor, opt => opt.MapFrom(src => src.StripeColor));
        }
    }

    public class WebColorTypeConverter : ITypeConverter<Color, string>
    {
        private readonly IWebStandardsProvider _webStandardsProvider;

        public WebColorTypeConverter(IWebStandardsProvider webStandardsProvider)
        {
            _webStandardsProvider = webStandardsProvider;
        }

        public string Convert(Color source, string destination, ResolutionContext context)
        {
            return _webStandardsProvider.ToWebColor(source);
        }
    }
}