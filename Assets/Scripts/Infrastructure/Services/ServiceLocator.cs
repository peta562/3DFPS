namespace Infrastructure.Services {
    public sealed class ServiceLocator {
        class Implementation<T> where T : IService {
            public static T ServiceInstance;
        }
        
        static ServiceLocator _instance;

        public static ServiceLocator Services => _instance ??= new ServiceLocator();

        public void RegisterSingle<T>(T implementation) where T : IService {
            Implementation<T>.ServiceInstance = implementation;
        }

        public T Single<T>() where T : IService {
            return Implementation<T>.ServiceInstance;
        }

    }
}