using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFuncTool
{

    /// <summary>
    /// 围绕某点旋转指定角度
    /// </summary>
    /// <param name="position">自身坐标</param>
    /// <param name="center">旋转中心</param>
    /// <param name="axis">围绕旋转轴</param>
    /// <param name="angle">旋转角度</param>
    /// <returns></returns>
    public static Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * (position - center) + center;
    }

}
