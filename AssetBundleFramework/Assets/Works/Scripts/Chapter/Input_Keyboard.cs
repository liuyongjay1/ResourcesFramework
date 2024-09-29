using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Keyboard:Singleton<Input_Keyboard>
{
    public event Action<Vector2> MouseScrollEvent;
    public void Init()
    {
      
    }

    public void Tick(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            InputManager.Instance.ExcuteAction("selectOption1", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            InputManager.Instance.ExcuteAction("selectOption2", 0);
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            InputManager.Instance.ExcuteAction("selectOption3", 0);
        }
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            MouseScrollEvent?.Invoke(Input.mouseScrollDelta);
        }
    }
}
