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
        Debug.LogError("callback‚ªŒÄ‚Î‚ê‚½");
    }
}
