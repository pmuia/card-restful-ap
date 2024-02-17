using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.MessagingModule.Aggregates;
using Cards.API.Models.DTOs.Responses.Auth;
using Cards.API.Models.DTOs.Responses.MessagingModule;

namespace Cards.API.Models.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MappingProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<Client, ClientDto>()
               .ForMember(destinationMember => destinationMember.ApiKey, options => options.MapFrom(src => src.ClientId));
            CreateMap<Client, MinifiedClientDto>()
               .ForMember(destinationMember => destinationMember.ApiKey, options => options.MapFrom(src => src.ClientId));

          

            CreateMap<TextAlert, TextAlertDto>();
            CreateMap<TextAlert, MinifiedTextAlertDto>();
            CreateMap<EmailAlert, EmailAlertDto>();
            CreateMap<EmailAlert, MinifiedEmailAlertDto>();
        }
    }
}
