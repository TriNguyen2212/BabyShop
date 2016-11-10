using AutoMapper;
using BabyShop.Common;
using BabyShop.Model.Models;
using BabyShop.Service;
using BabyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;
        private IProductService _productService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
        }

        public ActionResult Index()

        {
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;

            var lastestProducts = _productService.GetLastest(4);
            var hotProducts = _productService.GetLastest(8);

            var lastestProductsView = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProducts);
            var hotProductsView = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProducts);

            homeViewModel.LastestProducts = lastestProductsView;
            homeViewModel.TopSaleProduct = hotProductsView;

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            return PartialView(cart);
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var productCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(productCategoryViewModel);
        }
    }
}