using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoInputTool : MonoBehaviour
{
    public event  Action<Vector3> onMouseDrag;
    public event  Action onMouseUp;
  
    private void OnMouseDrag()
    {
       
        onMouseDrag?.Invoke(Input.mousePosition);
    }

    private void OnMouseOver()
    {
    }

    private void OnMouseUp()
    {
        onMouseUp?.Invoke();
    }
}
