using Microsoft.AspNetCore.Mvc;

using Stage.Models;

namespace Stage.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart cart;
        private readonly Market marketService;

        public CartSummaryViewComponent(Cart cartService, Market marketService)
        {
            this.cart = cartService;
            this.marketService = marketService;
        }


        public IViewComponentResult Invoke()
        {
            this.cart.IsInternational = marketService.MarketId != marketService.CountryId;
            return View(this.cart);
        }
    }
}