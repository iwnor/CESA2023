using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
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
            // スティックの入力を取得
            var leftStickValue = gamepad.leftStick.ReadValue();
            
            this.transform.position = new Vector3(this.transform.position.x+(leftStickValue.x/20), this.transform.position.y, this.transform.position.z+(leftStickValue.y/20));
            
        }
            
    }
}
