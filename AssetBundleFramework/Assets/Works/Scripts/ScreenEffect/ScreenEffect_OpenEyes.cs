using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScreenEffect_OpenEyes : MonoBehaviour
{
    public Transform UpEye;
    public Transform DownEye;
    // Start is called before the first frame update
    void Start()
    {
        UpEye.localPosition = new Vector3(0, -150, 0);
        DownEye.localPosition = new Vector3(0, 150, 0);

        float openHalfTime = 1.5f;
        //������һ��
        DownEye.DOLocalMoveY(-330, openHalfTime);
        UpEye.DOLocalMoveY(330, openHalfTime);

        float closeTime = 1f;
        float closeTimeDelay =  0.5f;

        //��������
        DownEye.DOLocalMoveY(150, closeTime).SetDelay(1.5f);
        UpEye.DOLocalMoveY(-150, closeTime).SetDelay(1.5f);

        float openDelay = openHalfTime + closeTime + closeTimeDelay;

        //��ȫ��
        DownEye.DOLocalMoveY(-780, 1.5f).SetDelay(openDelay);
        UpEye.DOLocalMoveY(780, 1.5f).SetDelay(openDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
