using DiyorMarket.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/informations")]
    [ApiController]
    [Authorize]
    public class InformationsController : Controller
    {
        private readonly DiyorMarketDbContext _context;

        public InformationsController(DiyorMarketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public DiyorMarketDbContext Index()
        {
            return _context;
        }
    }
}
