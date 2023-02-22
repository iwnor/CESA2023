using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MirrorSpin : MonoBehaviour
{
    public bool Istouch = false;
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
            this.transform.Rotate(0, rightStickValue.x, 0);
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Istouch = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Istouch= false;
        }
    }
}
