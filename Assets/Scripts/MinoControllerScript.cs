using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoControllerScript : MonoBehaviour
{
    IRandomSelectMino _iRandomSelectMino = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();
    }


    private void Update()
    {
        MinoController();
    }
    /// <summary>
    /// ƒ~ƒm‚ÌŒÂ”‚ğŠÇ—‚·‚é
    /// </summary>
    private void MinoController()
    {
        
        if(_iRandomSelectMino.MinoList.Count <= 7)
        {
            _iRandomSelectMino.RandomSelectMino();
        }
    }
}
