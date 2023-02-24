using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismScript : MonoBehaviour
{
    [SerializeField] private GameObject beamPrefab;    // レーザーオブジェクト
    [SerializeField] private float angleInterval;   // プリズムから出る光の間隔
    [SerializeField] private Color[] lightColor;    // 光の色の配列（個数もここで取得）

    private List<GameObject> beamObjList;
    // Start is called before the first frame update
    void Start()
    {
        beamObjList = new List<GameObject>();
        for (int i = 0; i < lightColor.Length; i++)
        {
            beamObjList.Add(Instantiate(beamPrefab));
            beamObjList[i].GetComponent<LineRenderer>().SetColors(lightColor[i], lightColor[i]);    // カラー設定
            beamObjList[i].SetActive(false);    // 通常状態では非表示
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void　TurnOnPrismLight(Vector3 inVec)
    {
        for(int i = 0;i < beamObjList.Count; i++)
        {
            beamObjList[i].SetActive(true);
            beamObjList[i].transform.position = this.transform.position;
            float forwardAngle = Vector3.Angle(this.transform.forward, inVec);
            Vector3 newAngle = beamObjList[i].transform.eulerAngles;
            newAngle.y = forwardAngle + (i * angleInterval);
            beamObjList[i].transform.eulerAngles = newAngle;
        }
        Debug.Log("turnOn");
    }

    public void TurnOffPrismLight()
    {
        foreach(GameObject beam in beamObjList)
        {
            beam.SetActive(false);
        }
    }
}
