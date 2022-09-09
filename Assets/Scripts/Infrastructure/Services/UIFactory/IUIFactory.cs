using System.Threading.Tasks;

namespace Infrastructure.Services.UIFactory {
    public interface IUIFactory : IService {
        void CreateShop();
        Task CreateUIRoot();
    }
}