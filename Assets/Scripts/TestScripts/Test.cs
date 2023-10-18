using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICallbackable
{
    void Complete();
}

public static class CallBack
{
    public static Test test;

    public static void CallbackProgress(this ICallbackable callbackable)
    {
        test.callbackable = callbackable;
        test.Progress();
    }
}

public class Test : MonoBehaviour
{
    public ICallbackable callbackable;

    private void Awake()
    {
        CallBack.test = this;
    }

    public void Progress()
    {
        Debug.Log("ˆ—");
        StartCoroutine(A());
    }
    private IEnumerator A()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("ˆ—I—¹");
        callbackable.Complete();
    }
}
