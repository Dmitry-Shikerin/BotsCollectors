using Sources.Presentations.Views.CommandCenters;
using UnityEditor;
using UnityEngine;

namespace Sources.Editor
{
    [CustomEditor(typeof(CommandCenterView))]
    public class CommandCenterRadarGizmosEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(CommandCenterView commandCenterView, GizmoType gizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(commandCenterView.transform.position, commandCenterView.Radius);
        }
    }
}