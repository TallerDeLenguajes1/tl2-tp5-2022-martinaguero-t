using AutoMapper;
using TP4.Models;
using TP4.ViewModels;
public class PerfilDeMapeo : Profile {
    public PerfilDeMapeo(){
        CreateMap<Cadete, CadeteViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        // CreateMap<List<Cadete>,List<CadeteViewModel>>().ReverseMap();
        /* (???)
        CreateMap<List<Cadete>, ListaCadeteViewModel>().ForMember(
            dest => dest.listaCadetesViewModel,
            opt => opt.MapFrom(src => )
        ); 
        */
    }
}