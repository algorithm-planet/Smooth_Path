using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_ : MonoBehaviour
{ 
    public Transform Transform_;
    [Range(0.1f, 0.5f)]
    public float t = 0.3f;
    [Range(1, 50)]
    public int N = 20;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Generate_p();
            //Generate_v();
            //Generate_Length();
            //Generate_Ping();
            //Generate_Ping_List();
            Generate_path();
        }
        Generate_path();
    }

    List<Vector3> p_points;
    void Generate_path()
    {
        List<Vector3> p_way = new List<Vector3>();
        foreach(Transform transform_ in Transform_)
        {
            p_way.Add(transform_.position);
        }
        //
        p_points = Smooth_path.p_Path(p_way, N, t);

    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if(p_points != null)
        {
            for(int i = 1; i <= p_points.Count - 1; i += 1)
            {
                Vector3 current_v = p_points[i];
                Vector3 prev_v = p_points[i - 1];

                Gizmos.color = Color.white;
                Gizmos.DrawSphere(current_v, 0.1f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(current_v, prev_v);
            }
        }
    }
    #endregion

}
