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
        // �N���X�^���̃��Z�b�g����
        foreach (GameObject cristal in Cristals)
        {
            cristal.GetComponent<CrystalScript>().resetLazerNum();
        }

        // ���[�U�[�̓����蔻�菈��
        foreach (GameObject lazer in Lazers)
        {
            lazer.GetComponent<LightStartPoint>().shotLazer();
        }

        // �j��m�F����
        foreach(GameObject cristal in Cristals)
        {
            if(cristal.GetComponent<CrystalScript>().checkBreakLazer())
            {
                cristal.SetActive(false);
            }
        }

        // ���ׂĔj��ł������̃`�F�b�N
        bool allBreakFlag = true;
        foreach(GameObject cristal in Cristals)
        {
            if(cristal.activeSelf == true)
            {
                allBreakFlag = false;
                break;
            }
        }

        // �S�����Ă����ꍇ�I������
        if(allBreakFlag)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().ActiveReloadButton();
        }
        else
        {
            // 1�ł��c���Ă��ꍇ�N���X�^������
            foreach (GameObject cristal in Cristals)
            {
                cristal.SetActive(true);
            }
        }
    }
}
