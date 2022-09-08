using Infrastructure.Services;

namespace Infrastructure.StateMachine {
    public interface IGameStateMachine : IService {
        void Enter<T>() where T : class, IState;
        void Enter<T, TP>(TP payload) where T : class, IPayloadedState<TP>;
    }
}