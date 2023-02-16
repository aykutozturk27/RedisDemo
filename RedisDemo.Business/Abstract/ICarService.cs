using RedisDemo.Entities.Concrete;

namespace RedisDemo.Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car GetById(int id);
        Car Add(Car car);
        void Update(Car car);
        void Delete(string key);
    }
}
