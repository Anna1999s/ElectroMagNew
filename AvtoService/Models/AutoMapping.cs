using AutoMapper;
using DomainModel.Content;
using DomainModel.Localization;
using Microsoft.AspNetCore.Identity;
using WebSite.Models.Account;
using WebSite.Models.Content;
using WebSite.Models.Navigation;

namespace WebSite.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<LocalizedMenu, MenuViewModel>();
            CreateMap<LocalizedMenuItem, MenuItemViewModel>();
            CreateMap<LocalizedPage, PageViewModel>();
            CreateMap<UserViewModel, IdentityUser>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<PhotoViewModel, Photo>();
            CreateMap<Photo, PhotoViewModel>();

            CreateMap<ProductCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();

            CreateMap<New, NewViewModel>();
            CreateMap<NewViewModel, New>();

            CreateMap<Brand, BrandViewModel>();
            CreateMap<BrandViewModel, Brand>();
        }
    }
}