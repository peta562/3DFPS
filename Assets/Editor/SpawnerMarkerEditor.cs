using GameCore.CommonLogic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(SpawnMarker))]
    public sealed class SpawnerMarkerEditor : UnityEditor.Editor {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo) {
            Gizmos.color = new Color(0.38f, 0.33f, 1f);
            Gizmos.DrawSphere(spawner.transform.position, 2f);
        }
    }
}