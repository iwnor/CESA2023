using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject Button;
    //�S�[���������̈ʒu
    private Vector3 Goal = new Vector3(0,-120,0);
    //����ȊO�̈ʒu
    private Vector2 Normal = new Vector2(1000, -120);
    // Start is called before the first frame update
    void Start()
    {
        Button.GetComponent<RectTransform>().anchoredPosition = Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Button.GetComponent<RectTransform>().anchoredPosition == Normal)
            {
                Button.GetComponent<RectTransform>().anchoredPosition = Goal;
            }
            else
            {
                Button.GetComponent<RectTransform>().anchoredPosition = Normal;
            }
        }

    }
    private void FixedUpdate()
    {
        //�S�[���������ǂ����ŕ���
    }

    public void ReLoadScene()
    {
        //�V�[���Ăяo��
        SceneManager.LoadScene("ProtoScene");
    }
    public void PushButton()
    {
        //�{�^���������Ƃ�
        ReLoadScene();
    }
}
