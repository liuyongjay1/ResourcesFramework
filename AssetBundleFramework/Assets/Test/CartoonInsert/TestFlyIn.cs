using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestFlyIn : MonoBehaviour
{
    public Transform trans1;
    public Transform trans2;
    public Transform trans3;
    // Start is called before the first frame update
    void Start()
    {
        trans1.DOLocalMoveX(-1390, 0.5f).SetDelay(0f);
        trans2.DOLocalMoveX(0, 0.5f).SetDelay(1);
        trans3.DOLocalMoveX(1390, 0.5f).SetDelay(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
