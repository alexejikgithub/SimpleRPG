using SimpleRPG.Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [UnityEditor.CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmo)
        {
            Gizmos.color= Color.red;
            Gizmos.DrawSphere(spawner.transform.position,0.5f);
        }
    }
}