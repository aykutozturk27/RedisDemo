using Ninject.Modules;
using RedisDemo.Business.Abstract;
using RedisDemo.Business.Concrete.Managers;
using RedisDemo.Core.DataAccess.Redis;
using RedisDemo.DataAccess.Abstract;
using RedisDemo.DataAccess.Concrete.InMemory;

namespace RedisDemo.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICarDal>().To<InMemoryCarDal>().InSingletonScope();
            Bind<ICarService>().To<CarManager>().InSingletonScope();
            Bind<IRedisBaseRepository>().To<RedisBaseRepository>().InSingletonScope();
        }
    }
}
