using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] RuntimeData _runtimeData;
    private void Awake()
    {
        _runtimeData.object_id = -1;
        _runtimeData.hasDrawn = false;
        _runtimeData.stepsTaken = 0;
    }
}
