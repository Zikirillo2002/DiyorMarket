using Lesson11.Response;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public static class ControllerExtensions
    {
        public static void SetViewBagProperties<T>(this Controller controller, GetBaseResponse<T> result)
        {
            controller.ViewBag.CategoriesCount = result.Data?.Count();
            controller.ViewBag.CurrentPage = result.PageNumber;
            controller.ViewBag.PageSize = result.PageSize;
            controller.ViewBag.HasNext = result.HasNextPage;
            controller.ViewBag.HasPrevious = result.HasPreviousPage;
            controller.ViewBag.TotalPages = result.TotalPages;
        }
    }
}
