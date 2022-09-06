namespace Infrastructure.Services.WindowService {
    public interface IWindowManager : IService {
        public void Show(WindowType windowType);
    }
}