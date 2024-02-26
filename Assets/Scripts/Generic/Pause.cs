using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Pause");
        } 
    }
}
