using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestA : MonoBehaviour
{
    private Text text;
    private Test2 test2;
    private void Awake()
    {
        text = this.GetComponent<Text>();
        test2 = GameObject.FindObjectOfType<Test2>();

        test2.testProperty.Subscribe((x) => { text.text = x.ToString(); });
    }
}
