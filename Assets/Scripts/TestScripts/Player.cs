using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ICallbackable
{
    private void Start()
    {
        this.CallbackProgress();
    }

    public void Complete()
    {
        Debug.LogError("callbackが呼ばれた");
    }
}
