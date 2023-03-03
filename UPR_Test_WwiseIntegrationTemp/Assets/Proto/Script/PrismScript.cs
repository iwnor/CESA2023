using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismScript : MonoBehaviour
{
    [SerializeField] private GameObject beamPrefab;    // ���[�U�[�I�u�W�F�N�g
    [SerializeField] private float angleInterval;   // �v���Y������o����̊Ԋu
    [SerializeField] private Color[] lightColor;    // ���̐F�̔z��i���������Ŏ擾�j

    private List<GameObject> beamObjList;
    // Start is called before the first frame update
    void Start()
    {
        beamObjList = new List<GameObject>();
        for (int i = 0; i < lightColor.Length; i++)
        {
            beamObjList.Add(Instantiate(beamPrefab));
            beamObjList[i].GetComponent<LineRenderer>().SetColors(lightColor[i], lightColor[i]);    // �J���[�ݒ�
            beamObjList[i].SetActive(false);    // �ʏ��Ԃł͔�\��
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void�@TurnOnPrismLight(Vector3 inVec)
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
