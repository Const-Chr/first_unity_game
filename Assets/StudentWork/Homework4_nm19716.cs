//create a nav mesh agent with an animator controller that changes animations depending on its status
//(moving, idle, running etc)
//use the camera prefab (in 3D Models/Misc folder) to locate the player
//when the camera sees the player, play a different animation (https://www.mixamo.com/#/ for animations)
//also stop nav mesh agent component

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Homework4_nm19716 : ManagedClass
{


    public NavMeshAgent agent;
    public Animator animator;


    // use event system from NavMeshAgentVision_nm19716.cs to get the visibility of the player
    private void OnEnable()
    {
        // subscribe to the event
        NavMeshAgentVision_nm19716.ObjectVisibilityChanged += OnObjectVisibilityChanged;
    }

    private void OnObjectVisibilityChanged(string objectName, bool isVisible)
    {
        if (objectName == gameObject.name)
        {
            if (isVisible)
            {
                // change crouch animation parameter
                animator.SetBool("Crouch" ,true);
                // stop nav mesh agent component
                agent.isStopped = true;
            }
            else
            {
                // change crouch animation parameter
                animator.SetBool("Crouch" ,false);
                // start nav mesh agent component
                agent.isStopped = false;
            }
        }
    }

    void Update()
    {
        // Calculate the speed of the agent and change animator parameter
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Agent_Speed", speed);    
    }

    private void OnDisable()
    {
        // unsubscribe from the event
        NavMeshAgentVision_nm19716.ObjectVisibilityChanged -= OnObjectVisibilityChanged;
    }
}