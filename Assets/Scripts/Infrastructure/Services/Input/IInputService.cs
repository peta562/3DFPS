using UnityEngine;

namespace Infrastructure.Services.Input {
    public interface IInputService : IService {
        Vector3 InputAxis { get; }
        Vector2 MouseAxis { get; }

        bool IsAttackButtonUp();
    }
}