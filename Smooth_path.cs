using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth_path
{
    public static List<Vector3> p_Path(List<Vector3> way_points , int N = 20 , float t = 0.3f)
    {
        List<Vector3> p_Path = new List<Vector3>();
        #region Controls
        List<Vector3> p_Controls = Smoooth_controls.p_Controls(way_points, t);
        #endregion

        #region path
        float Sum = 0f;

        int i_L = (p_Controls.Count - 1) / 3;

        Bezier_Curve[] bezier_Curves = new Bezier_Curve[i_L];

        for (int i = 0; i < i_L; i += 1)
        {
            List<Vector3> p_points = new List<Vector3>();
            int index = i * 3;
            for (int v = index; v <= index + 3; v += 1)
            {
                p_points.Add(p_Controls[v]);
            }

            bezier_Curves[i] = new Bezier_Curve(p_points);
            Sum += bezier_Curves[i].Distance_(1f);
        }

        //
        p_Path = new List<Vector3>();
        for (int i = 0; i < i_L; i += 1)
        {
            Bezier_Curve bezier_Curve = bezier_Curves[i];
            bezier_Curve.Generate_steps(Sum, N);

            for (float t_i = 0f; t_i < 1f; t_i += bezier_Curve.t_s)
            {
                float new_t = bezier_Curve.Ping_(t_i);
                Vector3 v = bezier_Curve.bezier.position(new_t);
                p_Path.Add(v);
            }
        }
        //Last
        p_Path.Add(p_Controls[p_Controls.Count - 1]);
        #endregion

        return p_Path;
    }
}
//
#region Smooth_Controls
public static class Smoooth_controls
{
    public static List<Vector3> p_Controls(List<Vector3> way_points, float t = 0.3f)
    {
        List<Vector3> p_Anchors = way_points;
        float Debug_time = Time.deltaTime;

        List<Vector3> p_Controls = new List<Vector3>();
        for (int i = 1; i < p_Anchors.Count - 1; i += 1)
        {
            Vector3 dir_1 = p_Anchors[i - 1] - p_Anchors[i];
            Vector3 dir_2 = p_Anchors[i + 1] - p_Anchors[i];

            Vector3 n_ = (dir_1.normalized - dir_2.normalized).normalized;

            Vector3 p_control_1 = p_Anchors[i] + n_ * dir_1.magnitude * t;
            Vector3 p_control_2 = p_Anchors[i] - n_ * dir_2.magnitude * t;

            p_Controls.Add(p_control_1);
            p_Controls.Add(p_Anchors[i]);
            p_Controls.Add(p_control_2);

            Debug.DrawLine(p_Anchors[i], p_control_1, Color.green, Debug_time);
            Debug.DrawLine(p_Anchors[i], p_control_2, Color.green, Debug_time);
        }


        #region Start_End
        Vector3 start = p_Anchors[0];
        Vector3 end = p_Anchors[p_Anchors.Count - 1];

        Vector3 dir_start = p_Controls[0] - start;
        Vector3 p_control_start = start + dir_start * 0.5f;

        Vector3 dir_end = p_Controls[p_Controls.Count - 1] - end;
        Vector3 p_control_end = end + dir_end * 0.5f;

        p_Controls.Insert(0, p_control_start);
        p_Controls.Insert(0, start);

        p_Controls.Add(p_control_end);
        p_Controls.Add(end);

        Debug.DrawLine(start, p_control_start, Color.green, Debug_time);
        Debug.DrawLine(end, p_control_end, Color.green, Debug_time);
        #endregion

        return p_Controls;
    }
}
#endregion