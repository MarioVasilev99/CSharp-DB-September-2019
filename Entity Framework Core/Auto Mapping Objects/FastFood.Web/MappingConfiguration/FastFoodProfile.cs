namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using Models;

    using ViewModels.Positions;
    using ViewModels.Employees;
    using ViewModels.Categories;
    using FastFood.Web.ViewModels.Items;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Employees
            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(s => s.Id))
                .ForMember(x => x.PositionName, y => y.MapFrom(s => s.Name));

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(m => m.Name, y => y.MapFrom(s => s.CategoryName));

            //Items
            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(m => m.CategoryId, m => m.MapFrom(c => c.Id))
                .ForMember(m => m.CategoryName, m => m.MapFrom(c => c.Name));

            this.CreateMap<CreateItemInputModel, Item>();
            this.CreateMap<Item, ItemsAllViewModels>();
        }
    }
}
