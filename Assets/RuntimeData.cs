using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RuntimeData")]
public class RuntimeData : ScriptableObject
{
    public int object_id;
    public bool hasDrawn;
    public int stepsTaken;
}
