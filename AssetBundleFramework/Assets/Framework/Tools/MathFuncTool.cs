using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFuncTool
{

    /// <summary>
    /// Χ��ĳ����תָ���Ƕ�
    /// </summary>
    /// <param name="position">��������</param>
    /// <param name="center">��ת����</param>
    /// <param name="axis">Χ����ת��</param>
    /// <param name="angle">��ת�Ƕ�</param>
    /// <returns></returns>
    public static Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * (position - center) + center;
    }

}
