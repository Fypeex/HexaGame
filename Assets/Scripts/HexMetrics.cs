using UnityEngine;

public static class HexMetrics
{
    public static Vector3 Center(int x, int y, float hexSize)
    {
        return new Vector3
        {
            x = x * OuterRadius(hexSize) * 1.5f,
            y = (y + x * 0.5f - x / 2) * InnerRadius(hexSize) * 2f,
            z = 0
        };
    }

    public static Vector3[] Corners(float hexSize)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, i);
        }

        return corners;
    }

    public static Vector3 Corner(float hexSize, int i)
    {
        float angle_deg = 60 * i;

        return new Vector3
        {
            x = hexSize * Mathf.Cos(angle_deg * Mathf.Deg2Rad),
            y = hexSize * Mathf.Sin(angle_deg * Mathf.Deg2Rad),
            z = 0
        };
    }

    public static float OuterRadius(float hexSize) => hexSize;
    public static float InnerRadius(float hexSize) => hexSize * Mathf.Sqrt(3) / 2;
}