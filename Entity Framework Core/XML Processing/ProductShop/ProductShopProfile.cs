namespace ProductShop
{
    using ProductShop.Dtos.Import;
    using ProductShop.Models;

    using AutoMapper;
    using ProductShop.Dtos.Export;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            // Import Users
            this.CreateMap<ImportUserDto, User>();

            // Import Products
            this.CreateMap<ImportProductDto, Product>();

            // Import Categories
            this.CreateMap<ImportCategoryDto, Category>();

            //Import Categories-Products
            this.CreateMap<ImportCategoryProductsDto, CategoryProduct>();

            //Export ProductsInRange
            this.CreateMap<Product, ExportProductsInRangeDto>()
                .ForMember(m => m.BuyerFullName,
                           m => m.MapFrom(s => $"{s.Buyer.FirstName} {s.Buyer.LastName}"));
        }
    }
}
