using UnityEditor;
using UnityEngine;

using ZombieVsMatch3.Gameplay.Match3.Stones;

namespace ZombieVsMatch3.Project.Scripts.Editor
{
    public class StoneEditor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(Stone distributorStones, GizmoType gizmo)
        {
            RectTransform rect = distributorStones.GetComponent<RectTransform>();
            
            Vector3[] vertices = new Vector3[4];
            rect.GetWorldCorners(vertices);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(vertices[0], vertices[1]);
            Gizmos.DrawLine(vertices[1], vertices[2]);
            Gizmos.DrawLine(vertices[2], vertices[3]);
            Gizmos.DrawLine(vertices[3], vertices[0]);
        }
    }
}