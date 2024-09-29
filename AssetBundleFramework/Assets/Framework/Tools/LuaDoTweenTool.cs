using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using XLua;

[LuaCallCSharp]
public class LuaDoTweenTool : MonoBehaviour
{
    public static void DoMove(GameObject obj, Vector3 pos, float moveTime)
    {
        obj.transform.DOKill();
        obj.transform.DOMove(pos, moveTime);
    }
    public static void DoLocalMove(GameObject obj, Vector3 pos, float moveTime)
    {
        obj.transform.DOKill();
        obj.transform.DOLocalMove(pos, moveTime);
    }
    public static void DoLocalMoveX(GameObject obj, float xValue, float moveTime)
    {
        obj.transform.DOKill();
        obj.transform.DOLocalMoveX(xValue, moveTime);
    }

    public static void DolocalMoveRight(GameObject obj,float rightValue, float moveTime)
    {
        obj.transform.DOKill();
        Vector3 targetPos = obj.transform.position + new Vector3(rightValue, 0);
        obj.transform.DOMove(targetPos, moveTime);
    }
}
