using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    [SerializeField] private int needLazerNum = 1;  // 破壊に必要なレーザーの本数
    private int nowLazerNum;    // 当たっているレーザーの本数

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
