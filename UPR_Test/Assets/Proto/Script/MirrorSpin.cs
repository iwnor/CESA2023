using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MirrorSpin : MonoBehaviour
{
    public bool Istouch = false;
    public bool Isactive = false;
    [SerializeField] private float turnSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var gamepad = Gamepad.current;
        var rightStickValue = gamepad.rightStick.ReadValue();
        
        if (Istouch == true)
        {
            if (gamepad.rightShoulder.wasReleasedThisFrame)
            {
                if (Isactive == false)
                {
                    Isactive = true;
                }
                else
                {
                    Isactive = false;
                }
            }
            this.transform.Rotate(0, rightStickValue.x * turnSpeed, 0);
            
        }
    }
    void FixedUpdate()
    {
        if(Isactive == true)
        {
            transform.GetChild(0).gameObject.tag = "Mirror";
            this.gameObject.tag = "Mirror";
        }
        else
        {
            transform.GetChild(0).gameObject.tag = "Untagged";
            this.gameObject.tag = "Untagged";
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Istouch = true;
        }
        
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Istouch= false;
        }
    }
}
