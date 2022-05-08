using bookShop.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace bookShop.Web.ViewComponents
{
    public class PublisherViewComponent : ViewComponent
    {
        private readonly IPublisherService _publisherService;
        public PublisherViewComponent(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        public IViewComponentResult Invoke() 
        {
            var publishers = _publisherService.GetAllEntities();
            return View(publishers);
        }
    }
}
