using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Bezier
{
    private NoteParser.Vec3[] coords;

    private int Binomial(int n, int i)
    {
        if (n == i || i == 0)
        {
            return 1;
        }

        int dividend = 1;
        int divisor = 1;

        for (int j = 1; j <= n; ++j)
        {
            dividend *= j;
        }
        for (int j = 1; j < i; ++j)
        {
            divisor *= j;
        }
        for (int j = 1; j < n - i; ++j)
        {
            divisor *= j;
        }

        return dividend / divisor;
    }

    public void Init(NoteParser.Vec3[] coords)
    {
        this.coords = coords;
    }

    public NoteParser.Vec3 GetPosition(float t)
    {
        t = Math.Min(1.0f, t);
        t = Math.Max(0.0f, t);

        float rt = 1 - t;

        int n = coords.Length;

        var ret = new NoteParser.Vec3();

        for (int i = 0; i < n; ++i)
        {
            double coefficient = Binomial(n, i);
            for (int j = 0; j < n - i; ++j)
            {
                coefficient *= rt;
            }
            for (int j = 0; j < i; ++j)
            {
                coefficient *= t;
            }

            ret += coefficient * coords[i];
        }

        return ret;
    }
}