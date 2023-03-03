using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Lazers;
    [SerializeField] private GameObject[] Cristals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shotLazers();
    }

    public void shotLazers()
    {
        // クリスタルのリセット処理
        foreach (GameObject cristal in Cristals)
        {
            cristal.GetComponent<CrystalScript>().resetLazerNum();
        }

        // レーザーの当たり判定処理
        foreach (GameObject lazer in Lazers)
        {
            lazer.GetComponent<LightStartPoint>().shotLazer();
        }

        // 破壊確認処理
        foreach(GameObject cristal in Cristals)
        {
            if(cristal.GetComponent<CrystalScript>().checkBreakLazer())
            {
                cristal.SetActive(false);
            }
        }

        // すべて破壊できたかのチェック
        bool allBreakFlag = true;
        foreach(GameObject cristal in Cristals)
        {
            if(cristal.activeSelf == true)
            {
                allBreakFlag = false;
                break;
            }
        }

        // 全部壊れていた場合終了処理
        if(allBreakFlag)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().ActiveReloadButton();
        }
        else
        {
            // 1個でも残ってた場合クリスタル復活
            foreach (GameObject cristal in Cristals)
            {
                cristal.SetActive(true);
            }
        }
    }
}
