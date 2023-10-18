using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestD<T>
{
    Action<T> action;

    private T value = default;
    
   public T Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            action(this.value);
        }
    }
    public void Subscribe(Action<T> action) => this.action += action;
}
