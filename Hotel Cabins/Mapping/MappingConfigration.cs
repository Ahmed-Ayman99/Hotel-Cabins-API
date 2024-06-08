using AutoMapper;
using Hotel_Cabins.DTOs.BookingDTOs;
using Hotel_Cabins.DTOs.CabinsDTOs;
using Hotel_Cabins.DTOs.GuestDOTs;
using Hotel_Cabins.DTOs.SettingsDTOs;
using Hotel_Cabins.DTOs.User;
using Hotel_Cabins.Models;
using Microsoft.AspNetCore.Identity;
using VillaAPI.DTOs.Role;

namespace Hotel_Cabins.Mapping
{
    public class MappingConfigration : Profile
    {
        public MappingConfigration()
        {
            CreateMap<Guest , GuestCreateDTO>().ReverseMap();
            CreateMap<Guest , GuestUpdateDTO>().ReverseMap();
            CreateMap<Guest , GuestDTO>().ReverseMap();

            CreateMap<Cabin, CabinCreateDTO>().ReverseMap();
            CreateMap<Cabin, CabinUpdateDTO>().ReverseMap();
            CreateMap<Cabin, CabinDTO>().ReverseMap();


            CreateMap<Setting, SettingsDTO>().ReverseMap();
            CreateMap<Setting, SettingsCreateDTO>().ReverseMap();
            CreateMap<Setting, SettingsUpdateDTO>().ReverseMap();

            CreateMap<Booking, BookingDTO>().ReverseMap();
            CreateMap<Booking, BookingCreateDTO>().ReverseMap();
            CreateMap<Booking, BookingUpdateDTO>().ReverseMap();

            CreateMap<IdentityRole, CreateRoleDTO>().ReverseMap();
            CreateMap<IdentityRole, RoleDTO>().ReverseMap();


            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();
        }
    }
}
