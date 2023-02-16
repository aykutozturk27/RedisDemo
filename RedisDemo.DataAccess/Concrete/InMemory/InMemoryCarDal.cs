using RedisDemo.DataAccess.Abstract;
using RedisDemo.Entities.Concrete;

namespace RedisDemo.DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1,Description="Otomobil",DailyPrice=50000,ModelYear=DateTime.Now.AddYears(-5)},
                new Car{Id=2,Description="Yarış Otomobili",DailyPrice=55000,ModelYear=DateTime.Now.AddYears(-7)},
                new Car{Id=3,Description="Tır",DailyPrice=60000,ModelYear=DateTime.Now.AddYears(-6)},
                new Car{Id=4,Description="Otobüs",DailyPrice=80000,ModelYear=DateTime.Now.AddYears(-8)},
                new Car{Id=5,Description="Kamyon",DailyPrice=100000,ModelYear=DateTime.Now.AddYears(-9)}
            };
        }

        public List<Car> GetList()
        {
            return _cars;
        }

        public Car GetById(int id)
        {
            var car = _cars[id];
            return car;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car car)
        {
            _cars.Remove(car);
        }
    }
}
