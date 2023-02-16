using RedisDemo.Entities.Concrete;

namespace RedisDemo.DataAccess.Abstract
{
    public interface ICarDal
    {
        List<Car> GetList();
        Car GetById(int id);
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
