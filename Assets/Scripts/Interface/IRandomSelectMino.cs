using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandomSelectMino
{
    List<GameObject> MinoList { get; set; }
    List<GameObject> GhostList { get; set; }
    void RandomSelectMino();
}
