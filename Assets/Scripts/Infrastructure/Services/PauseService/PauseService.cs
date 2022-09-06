using System.Collections.Generic;

namespace Infrastructure.Services.PauseService {
    public sealed class PauseService : IPauseService {
        private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
        
        public void Register(IPauseHandler pauseHandler) {
            _handlers.Add(pauseHandler);
        }

        public void Unregister(IPauseHandler pauseHandler) {
            _handlers.Remove(pauseHandler);
        }

        public void SetPause(bool isPaused) {
            foreach (var handler in _handlers) {
                handler.OnPauseChanged(isPaused);
            }
        }
    }
}