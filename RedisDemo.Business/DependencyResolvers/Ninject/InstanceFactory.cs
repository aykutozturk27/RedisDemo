using Ninject;

namespace RedisDemo.Business.DependencyResolvers.Ninject
{
    public static class InstanceFactory
    {
        public static T GetInstance<T>()
        {
            var standardKernel = new StandardKernel(new BusinessModule());
            return standardKernel.Get<T>();
        }
    }
}
