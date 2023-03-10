using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public bool Istouch = false;
    public bool Isactive = false;
    [SerializeField]private StageManager stagemanager;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float turnSpeed = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            //コントローラーなし
        }
        else
        {
            //
            if(gamepad.buttonSouth.wasReleasedThisFrame)
            {
                stagemanager.shotLazers();
            }
            
            // スティックの入力を取得
            var leftStickValue = gamepad.leftStick.ReadValue();
            var rightStickValue = gamepad.rightStick.ReadValue();
            this.transform.position = new Vector3(this.transform.position.x+(leftStickValue.x/20) * moveSpeed, this.transform.position.y, this.transform.position.z+(leftStickValue.y/20) * moveSpeed);
            if(Istouch != true)
            {
                if(gamepad.rightShoulder.wasReleasedThisFrame)
                {
                    if(Isactive == true)
                    {
                        Isactive = false;
                    }
                    else
                    {
                        Isactive = true;
                    }
                }
                this.transform.Rotate(0, rightStickValue.x * turnSpeed, 0);
            }
        }
           
    }
    void FixedUpdate()
    {
        if(Isactive == true)
        {
            this.gameObject.tag = "Mirror";
        }
        else
        {
            this.gameObject.tag = "Player";
        }
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Mirror" || collision.gameObject.tag == "Untagged")
        {
            Istouch = true;
        }
        else
        {
            Istouch = false;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Mirror"|| collision.gameObject.tag == "Untagged")
        {
            Istouch = false;
        }
    }
}
