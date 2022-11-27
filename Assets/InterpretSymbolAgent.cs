/*
 * Author: Grant Waldow
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class InterpretSymbolAgent : Agent
{
    [SerializeField] Transform _scribe;
    [SerializeField] Transform _focalPoint;
    [SerializeField] RuntimeData _runtimeData;
    // private Vector3 initialPos = new Vector3(0, 3, -0.5f); // FIRST TRIAL INITIAL POS
    private Vector3 initialPos = new Vector3(0, 3, -2);
    private int correct = 0;
    public override void OnEpisodeBegin()
    {
        // transform.localPosition = initialPos + new Vector3(Random.Range(-1f,1f) / 2, Random.Range(-1f,1f) / 2, 0); // FIRST TRIALS RANDOMIZATION USED
        transform.localPosition = initialPos + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-1f, 1f));
        transform.LookAt(_focalPoint);
        if (CompletedEpisodes % 1000 == 0)
        {
            Debug.Log("Percent Correct this 100,000 episodes: " + (correct / 1000f));
            correct = 0;
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if(_runtimeData.hasDrawn)
        {
            int guess = actions.DiscreteActions[0];
            if (guess == _runtimeData.object_id)
            {
                // reward both
                //Debug.Log("------REWARD!!!------");
                AddReward(1f);
                _scribe.GetComponent<DrawSymbolAgent>().updoot();
                correct++;
            }
            else
            {
                // punish both
                //Debug.Log("PUNISH");
                AddReward(-1f);
                _scribe.GetComponent<DrawSymbolAgent>().downdoot();
            }

            // end episode for both
            _runtimeData.stepsTaken = 0;
            _scribe.GetComponent<DrawSymbolAgent>().EndEpisode();
            EndEpisode();
        }
    }

    public void Act()
    {
        RequestDecision();
    }
}