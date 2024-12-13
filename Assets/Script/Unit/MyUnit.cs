using UnityEngine;

public class MyUnit
{
    public static readonly float xMul = 0.1f;
    public static readonly float yMul = 0.4f;
    public static Vector3 GetMyVecUnit(Vector3 vector3)
    {
        return new Vector3(vector3.x * xMul, vector3.y, vector3.z * yMul);
    }
}
