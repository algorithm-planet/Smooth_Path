using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier_Curve
{
    public Bezier bezier;
    public float t_s;
    float d_s;

    public Bezier_Curve(List<Vector3> p_)
    {
        bezier = new Bezier(p_);
    }

    public void Generate_steps(float D, int N = 10)
    {
        float d = Distance_(1f);
        int num_of_segments = Mathf.RoundToInt((d / D) * N);
        //
        if (num_of_segments == 0)
            num_of_segments = 1;

        t_s = 1f / num_of_segments;
        d_s = d / num_of_segments;
    }

    #region D(t)
    public float Distance_(float t)
    {
        int resolution = 100;
        float t_h = 1f / resolution;

        float sum = 0;
        for(float t_i = t_h; t_i <= t; t_i += t_h)
        {
            Vector3 current_v = bezier.position(t_i);
            Vector3 prev_v = bezier.position(t_i - t_h);

            sum += Vector3.Magnitude(current_v - prev_v);
        }
        return sum;
    }
    #endregion

    #region new_t(t)
    public float Ping_(float t)
    {
        float d = Mathf.RoundToInt(t / t_s) * d_s;
        float t_current = t;

        int i = 0;
        while (true)
        {
            //
            float new_D = Distance_(t_current) - d;
            float new_v = bezier.velocity(t_current).magnitude;

            float t_next = t_current - new_D / new_v;

            float accuracy = 0.1f;
            i += 1;
            if (i > 10 || Mathf.Abs(t_current - t_next) < accuracy * t_s)
            {
                //Debug.Log(i);
                break;
            }

            t_current = t_next;
        }
        return t_current;
    }
    #endregion

}
