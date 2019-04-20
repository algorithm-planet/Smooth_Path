using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    List<Vector3> p_;
    int n;

    public Bezier(List<Vector3> p_controls)
    {
        p_ = p_controls;
        n = p_.Count - 1;
    }

    public Vector3 position(float t)
    {
        Vector3 Sum = Vector3.zero;
        for(int i = 0; i <= n; i += 1)
        {
            Sum += Constant(n, i, t) * p_[i];
        }
        return Sum;
    }

    public Vector3 velocity(float t)
    {
        Vector3 Sum = Vector3.zero;
        for (int i = 0; i <= n - 1; i += 1)
        {
            Sum += Constant(n - 1, i, t) * (p_[i + 1] - p_[i]);
        }
        return n * Sum;
    }

    #region Constant
    float Constant(int n , int i , float t)
    {
        //
        int num_ = factorial(n);
        int deno_ = factorial(n - i) * factorial(i);
        float Combine_ = (float)num_ / deno_;

        return Combine_ * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
    }

    //
    int factorial(int n)
    {
        if (n == 0)
            return 1;
        int product = n;
        for(int i = n - 1; i >= 1; i -= 1)
        {
            product *= i;
        }
        return product;
    }
    #endregion

}
