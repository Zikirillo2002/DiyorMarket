using DiyorMarket.Domain.Common;

namespace DiyorMarket.Domain.Entities
{
    public  class User : EntityBase
    {
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
