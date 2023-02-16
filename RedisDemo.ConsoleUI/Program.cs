using RedisDemo.Business.Abstract;
using RedisDemo.Business.DependencyResolvers.Ninject;
using RedisDemo.Entities.Concrete;

Console.WriteLine("Saving car data in cache");
//var car = new Car
//{
//    Id = 6,
//    Description = "Yeni Tip",
//    DailyPrice = 5000,
//    ModelYear = DateTime.Now,
//};

var car = new Car
{
	Id = 1,
	Description= "Otomobil",
	DailyPrice = 5000,
	ModelYear = DateTime.Now.AddDays(-5)
};

try
{
    var carService = InstanceFactory.GetInstance<ICarService>();
	//var checkCar = carService.GetById(6);
	//var carList = carService.GetAll();
	//var addedCar = carService.Add(car);
	//Console.WriteLine(addedCar);
	carService.Delete("car");
}
catch (Exception ex)
{
	throw new Exception(ex.Message);
}

Console.WriteLine("Finished");
Console.ReadLine();
