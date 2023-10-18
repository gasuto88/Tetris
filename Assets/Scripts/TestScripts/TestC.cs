using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestC : MonoBehaviour
{
    public delegate void DogMethod();
    public DogMethod dogMethod;

    private void Start()
    {
        //dogMethod = new DogMethod(Unti);

        Unti(() => { Debug.Log("よばれたよー"); });
        //dogMethod();
    }

    private void Unti(DogMethod dogMethod)
    {
        Debug.Log("Untiがよばれました");
        dogMethod();
    }
}
