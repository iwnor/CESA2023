using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    [SerializeField] private int needLazerNum = 1;  // �j��ɕK�v�ȃ��[�U�[�̖{��
    private int nowLazerNum;    // �������Ă��郌�[�U�[�̖{��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetLazerNum()
    {
        nowLazerNum = 0;
    }

    public void countUpLazer()
    {
        nowLazerNum++;
    }

    public bool checkBreakLazer()
    {
        Debug.Log(nowLazerNum);
        if(needLazerNum < nowLazerNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
