using UnityEngine;

namespace _01_Scripts.Control.WayPoint
{
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private Vector3[] points;
        private Vector3 curPos;
        private bool isChange;
        public Vector3[] Points => points;
        public Vector3 CurPos => curPos;
        public bool IsChange => isChange;
        private void Start()
        {
            curPos = transform.position;
            isChange = true;
        }
        public int GetLengthPoint() =>  points.Length;
        public Vector3 GetWaypointPosition(int index) => curPos + points[index];

        private void OnDrawGizmos()
        {
            if (transform.hasChanged && !isChange)
            {
                curPos = transform.position;
            }

            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(points[i]+curPos,0.5f);

                if (i < points.Length - 1)
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawLine(points[i]+ curPos,points[i+1]+curPos);
                }
            }
        }
    }
}
