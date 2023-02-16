using Newtonsoft.Json;
using RedisDemo.Business.Abstract;
using RedisDemo.Core.DataAccess.Redis;
using RedisDemo.DataAccess.Abstract;
using RedisDemo.Entities.Concrete;

namespace RedisDemo.Business.Concrete.Managers
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        //private readonly IDatabase _database;
        private readonly IRedisBaseRepository _redisBaseRepository;

        public CarManager(ICarDal carDal, IRedisBaseRepository redisBaseRepository)
        {
            _carDal = carDal;
            _redisBaseRepository = redisBaseRepository;
            //_database = RedisConnectorHelper.GetDatabase();
        }

        //public List<Car> GetAll()
        //{
        //    var carList = _carDal.GetList();
        //    foreach(var car in carList) 
        //    {
        //        var description = _database.StringGet($"Description:{car.Description}");
        //        var dailyPrice = _database.StringGet($"DailyPrice:{car.DailyPrice}");
        //        var modelYear = _database.StringGet($"ModelYear:{car.ModelYear}");
        //        Console.WriteLine($"Value={description} - {dailyPrice} - {modelYear}");
        //        Console.WriteLine("-----------------------------------------------");
        //    }
        //    return carList;
        //}

        //public void Add(Car car)
        //{
        //    var newCar = new Car
        //    {
        //        Id = car.Id,
        //        Description = car.Description,
        //        DailyPrice = car.DailyPrice,
        //        ModelYear = car.ModelYear
        //    };

        //    //var jsonString = JsonConvert.SerializeObject(newCar);
        //    //_database.StringSet("car", jsonString);

        //    _database.StringSet(@"car.Id", newCar.Id);
        //    _database.StringSet(@"car.Description", newCar.Description);
        //    _database.StringSet(@"car.DailyPrice", newCar.DailyPrice.ToString());
        //    _database.StringSet(@"car.ModelYear", newCar.ModelYear.ToString());
        //}

        public List<Car> GetAll()
        {
            var carList = _carDal.GetList();
            //var jsonString = JsonConvert.SerializeObject(carList);
            _redisBaseRepository.Set("car", carList);
            return carList;
        }

        public Car GetById(int id)
        {
            var car = _carDal.GetById(id);
            var serialize = JsonConvert.SerializeObject(car);
            var cachedCar = _redisBaseRepository.Get<Car>($"car:{serialize}");
            return cachedCar;
        }

        public Car Add(Car car)
        {
            _redisBaseRepository.Set("car", car, duration: 5);
            var addedCar = _redisBaseRepository.Get<Car>("car");
            return addedCar;
        }

        public void Update(Car car)
        {
            _redisBaseRepository.Remove("car");
            _redisBaseRepository.Set("car", car);
        }
        
        public void Delete(string key)
        {
            _redisBaseRepository.Remove(key);
            Console.WriteLine("Silindi");
        }
    }
}
