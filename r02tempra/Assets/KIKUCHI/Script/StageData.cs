using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
  fileName = "StageData",
  menuName = "ScriptableObject/StageData",
  order = 0)
]
public class StageData : ScriptableObject
{
    public List<GameObject> StageObjList = new List<GameObject>();
    public int Number_S;
    public int Xmax;
    public int Ymax;
}


