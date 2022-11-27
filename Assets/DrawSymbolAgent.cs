/*
 * Author: Grant Waldow
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DrawSymbolAgent : Agent
{
    private List<LineRenderer> _linesRenderer = new List<LineRenderer>();
    private LineRenderer _currentLineRenderer;
    private int _vertexCount = 0;
    [SerializeField] Transform _linePrefab;
    [SerializeField] Transform _reader;
    [SerializeField] RuntimeData _runtimeData;

    public override void OnEpisodeBegin()
    {
        // destroy lines
        _linesRenderer.Clear();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        _runtimeData.object_id = Random.Range(0, 10); // 10 possible objects to describe
        _runtimeData.hasDrawn = false;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // observation for scribe is only the object id
        sensor.AddObservation(_runtimeData.object_id);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (!_runtimeData.hasDrawn)
        {
            // draw symbol
            for (int i = 0; i < actions.ContinuousActions.Length; i = i + 3)
            {
                if (i == 0) 
                {
                    Transform tmpGesture = Instantiate(_linePrefab, transform.position, transform.rotation, transform) as Transform;
                    _currentLineRenderer = tmpGesture.GetComponent<LineRenderer>();
                    _linesRenderer.Add(_currentLineRenderer);
                    _vertexCount = 0;

                    _currentLineRenderer.SetVertexCount(++_vertexCount);
                    _currentLineRenderer.SetPosition(_vertexCount - 1, transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(actions.ContinuousActions[i] * (Screen.width/2) + (Screen.width/2), actions.ContinuousActions[i + 1] * (Screen.height/2) + (Screen.height / 2), Vector3.Distance(transform.position, transform.parent.Find("FocalPoint").position))));

                }
                else if (actions.ContinuousActions[i + 2] > 0)
                {
                    if (actions.ContinuousActions[i - 1] <= 0)
                    {
                        Transform tmpGesture = Instantiate(_linePrefab, transform.position, transform.rotation, transform) as Transform;
                        _currentLineRenderer = tmpGesture.GetComponent<LineRenderer>();
                        _linesRenderer.Add(_currentLineRenderer);
                        _vertexCount = 0;
                    }
                    _currentLineRenderer.SetVertexCount(++_vertexCount);
                    _currentLineRenderer.SetPosition(_vertexCount - 1, transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(actions.ContinuousActions[i] * (Screen.width/2) + (Screen.width / 2), actions.ContinuousActions[i + 1] * (Screen.height/2) + (Screen.height / 2), Vector3.Distance(transform.position, transform.parent.Find("FocalPoint").position))));
                }
            }
            _runtimeData.hasDrawn = true;
        }

    }
    /*
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        if (Input.GetMouseButtonDown(0)) {
            Transform tmpGesture = Instantiate(_linePrefab, transform.position, transform.rotation, transform) as Transform;
            _currentLineRenderer = tmpGesture.GetComponent<LineRenderer>();
            _linesRenderer.Add(_currentLineRenderer);
            _vertexCount = 0;
        }
        if (Input.GetMouseButton(0))
        {
            _currentLineRenderer.SetVertexCount(++_vertexCount);
            _currentLineRenderer.SetPosition(_vertexCount - 1, transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, transform.parent.Find("FocalPoint").position))));
        }
    }
    */

    public void Act()
    {
        RequestDecision();
    }

    // - (_linesRenderer.Count / 26) // add to decrease by linecount
    public void updoot()
    {
        int totalVerticies = 0;
        foreach(LineRenderer l in _linesRenderer)
        {
            totalVerticies += l.positionCount;
        }
        //AddReward(+1f);
        AddReward(+2f - (_linesRenderer.Count / 13) - (totalVerticies / 25)); // subracts from reward (now +2) for both # of lines and # of verticies used!
        //Debug.Log("Scribe REWARDED!!!");
    }

    public void downdoot()
    {
        AddReward(-1f);
    }
}