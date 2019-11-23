namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;

    using Data;
    using ViewModels.Categories;
    using FastFood.Models;
    using AutoMapper.QueryableExtensions;
    using System.Linq;

    public class CategoriesController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public CategoriesController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                this.RedirectToAction("Error", "Home");
            }

            var newCategory = this.mapper.Map<Category>(model);
            this.context.Add(newCategory);
            this.context.SaveChanges();

            return RedirectToAction("All", "Categories");
        }

        public IActionResult All()
        {
            var categories = this.context
                                 .Categories
                                 .OrderBy(c => c.Id)
                                 .ProjectTo<CategoryAllViewModel>()
                                 .ToList();

            return this.View(categories);
        }
    }
}
