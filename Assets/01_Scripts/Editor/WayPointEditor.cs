using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    private WayPoint wayPoint;

    private void OnSceneGUI()
    {
        wayPoint = (WayPoint)target;
        if (wayPoint == null || wayPoint.Points == null || wayPoint.GetLengthPoint() == 0)
            return;

        Handles.color = Color.cyan;

        for (int i = 0; i < wayPoint.GetLengthPoint(); i++)
        {
            Vector3 currentWaypointPoint = wayPoint.CurPos + wayPoint.Points[i];
            EditorGUI.BeginChangeCheck();
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(
                currentWaypointPoint, 
                0.7f, 
                new Vector3(0.3f, 0.3f, 0.3f), 
                Handles.SphereHandleCap);

            GUIStyle textStyle = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
                fontSize = 16,
                normal = { textColor = Color.white }
            };

            Vector3 textAlignment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(currentWaypointPoint + textAlignment, $"{i + 1}", textStyle);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wayPoint, "Move WayPoint Handle");
                wayPoint.Points[i] = newWaypointPoint - wayPoint.CurPos;
                EditorUtility.SetDirty(wayPoint);
            }
        }
    }
}