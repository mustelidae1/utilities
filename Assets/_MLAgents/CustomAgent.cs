using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CustomAgent : Agent
{
    Rigidbody rb;
    public Transform target;
    public float forceMult = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void AgentReset()
    {
        // If we fell off the plane, zero momentum and return to start position 
        if (this.transform.position.y < 0)
        {
            this.rb.angularVelocity = Vector3.zero;
            this.rb.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

        target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    // All the things we care about knowing
    public override void CollectObservations()
    {
        // must set space size to match number of observations in Behavior Parameters 
        AddVectorObs(transform.position);
        AddVectorObs(target.position);

        AddVectorObs(rb.velocity.x);
        AddVectorObs(rb.velocity.z);
    }

    // vectorAction indicates possible choices that you could take 
    // Can be discrete or continuous 
    // Must set parameters for vectorAction in Behavior Parameters 
    // Continuous is always between -1 and 1
    // For discrete we can set possible values 
    // Space size is the size of the vectorAction array 
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];       // the neural net will set the values of vectorAction for us 
        controlSignal.z = vectorAction[1];
        rb.AddForce(controlSignal * forceMult);

        // Rewards 
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // -- reached target 
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            Done();
        }

        // -- fell off platform 
        if (transform.position.y < 0)
        {
            // No reward for you 
            // If we want to deincentivize falling off, we could set a negative reward here 
            Done();
        }

        // -- loitering penalty 
        SetReward(-0.1f);
    }

    // Can let a human control it for testing purposes 
    // Lets you put in your own values for vectorAction
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

}

