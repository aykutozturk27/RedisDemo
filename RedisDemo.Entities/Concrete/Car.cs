using RedisDemo.Core.Entities;

namespace RedisDemo.Entities.Concrete
{
    public class Car : BaseEntity
    {
        public string? Description { get; set; }
        public decimal DailyPrice { get; set; }
        public DateTime ModelYear { get; set; }
    }
}
