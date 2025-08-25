using AutoMapper;
using msAlertaMongoDB.DTO;
using msAlertaMongoDB.Entity;

namespace msAlertaMongoDB.Mapper
{
    public class AlertaProfile : Profile
    {
        public AlertaProfile() 
        {
            CreateMap<AlertaRequestDto, Alerta>();
            CreateMap<Alerta, AlertaResponseDto>();
        }
    }
}
