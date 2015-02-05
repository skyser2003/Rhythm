using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Bezier
{
    private NoteParser.Vec3[] coords;
    private double[] coefficients;

    private int Binomial(int n, int i)
    {
        if (n == i || i == 0)
        {
            return 1;
        }

        int dividend = 1;
        int divisor = 1;

        for (int j = i; j <= n; ++j)
        {
            dividend *= j;
        }

        for (int j = 1; j < i; ++j)
        {
            divisor *= j;
        }

        return dividend / divisor;
    }

    public void Init(NoteParser.Vec3[] coords)
    {
        this.coords = coords;

        int n = coords.Length;
        coefficients = new double[n];

        for (int i = 0; i < n; ++i)
        {
            coefficients[i] = Binomial(n - 1, i);
        }
    }

    public NoteParser.Vec3 GetPosition(float ft)
    {
        double t = (double)ft;

        t = Math.Min(1.0f, t);
        t = Math.Max(0.0f, t);

        double rt = 1 - t;

        int n = coords.Length - 1;

        var ret = new NoteParser.Vec3();

        for (int i = 0; i <= n; ++i)
        {
            double coefficient = coefficients[i];
            coefficient = coefficient * Math.Pow(rt, n - i) * Math.Pow(t, i);
            ret += coefficient * coords[i];
        }

        return ret;
    }
}