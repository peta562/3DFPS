using Infrastructure.Services.PauseService;
using UnityEngine;
using UnityEngine.AI;

namespace GameCore.Enemy {
    public sealed class EnemyMove : MonoBehaviour, IPauseHandler {
        [SerializeField] NavMeshAgent Agent;

        Transform _destinationTransform;
        float _stoppingDistance;

        public void Init(Transform destinationTransform, float speed, float stoppingDistance) {
            _destinationTransform = destinationTransform;

            Agent.speed = speed;
            Agent.stoppingDistance = stoppingDistance;
            _stoppingDistance = stoppingDistance;
        }

        public void Move() {
            if ( DestinationNotReached() ) {
                Agent.destination = _destinationTransform.position;
            }
        }

        bool DestinationNotReached() => 
            Vector3.Distance(Agent.transform.position, _destinationTransform.position) >= _stoppingDistance;

        public void OnPauseChanged(bool isPaused) {
            if ( Agent != null ) {
                Agent.isStopped = isPaused;
            }
        }
    }
}
