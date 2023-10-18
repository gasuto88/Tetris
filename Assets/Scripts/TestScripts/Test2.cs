using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void CompleteMethod();

public class Test2 : MonoBehaviour
{
    public TestD<int> testProperty = new TestD<int>();
   
    private void Update()
    {
        if (Input.anyKey)
        {
            testProperty.Value++;
        }
    }
}
