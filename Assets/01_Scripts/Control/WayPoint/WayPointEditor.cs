using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    WayPoint wayPoint => this.target as WayPoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.cyan;
        for (int i = 0; i < wayPoint.GetLengthPoint(); i++)
        {
            EditorGUI.BeginChangeCheck();
        
            // handle
            Vector3 currentWaypointPoint = wayPoint.CurPos + wayPoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);
        
            // create text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(wayPoint.CurPos + wayPoint.Points[i]+textAlligment,$"{i+1}",textStyle);
            EditorGUI.EndChangeCheck();


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                wayPoint.Points[i] = newWaypointPoint - wayPoint.CurPos;
            }
        }
    }
}
