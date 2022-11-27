/*
 * Author: Grant Waldow
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;
using Unity.MLAgents.Editor;
using Unity.MLAgents.Integrations;
public class AcademyManager : MonoBehaviour
{
    [SerializeField] RuntimeData _runtimeData;
    [SerializeField] Transform _scribeAgent;
    [SerializeField] Transform _readerAgent;
    void OnEnable()
    {
        Academy.Instance.AutomaticSteppingEnabled = false;
    }

    private void Update()
    {
        Academy.Instance.EnvironmentStep();
        if (_runtimeData.stepsTaken == 1) _scribeAgent.GetComponent<DrawSymbolAgent>().Act();
        if (_runtimeData.stepsTaken == 3) _readerAgent.GetComponent<InterpretSymbolAgent>().Act();
        _runtimeData.stepsTaken++;
    }
    
}
