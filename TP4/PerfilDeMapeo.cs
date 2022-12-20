using AutoMapper;
using TP4.Models;
using TP4.ViewModels;
public class PerfilDeMapeo : Profile {
    public PerfilDeMapeo(){
        CreateMap<Cadete, CadeteViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        CreateMap<Pedido, PedidosCadetesClientesViewModel>().ReverseMap(); 
        CreateMap<Pedido, PedidosCadetesViewModel>().ReverseMap(); 
    }
}