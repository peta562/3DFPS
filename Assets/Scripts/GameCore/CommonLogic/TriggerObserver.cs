using System;
using UnityEngine;

namespace GameCore.CommonLogic {
    [RequireComponent(typeof(Collider))]
    public sealed class TriggerObserver : MonoBehaviour {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        void OnTriggerEnter(Collider other) {
            TriggerEnter?.Invoke(other);
        }

        void OnTriggerExit(Collider other) {
            TriggerExit?.Invoke(other);
        }
    }
}