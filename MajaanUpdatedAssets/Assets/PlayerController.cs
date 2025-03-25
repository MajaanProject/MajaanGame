using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    //getting components on start
    void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    //checking for input
    void Update()
    {
        //no vector3 needed because navmeshagent automatically snaps the attached object to the navmesh within the game environment.
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //if no input detected, set animator to idle.
        if(input.magnitude <= 0)
        {
            animator.SetBool("Walking", false);
            return;
        }

        //if any input detected on the Vector2's vertical axis, run movement and/or rotation methods.
        if(Mathf.Abs(input.y) > 0.01f)
        {
            Move(input);
        }
        else
        {
            Rotate(input);
        }
    }

    //A and D rotate the character.
    private void Rotate(Vector2 input)
    {
            //simply transforming the rotation based on horizontal axial input
            navMeshAgent.destination = transform.position;
            animator.SetBool("Walking", false);
            transform.Rotate(0, input.x * navMeshAgent.angularSpeed * Time.deltaTime, 0);
    }

    //W and S move forward and back
    private void Move(Vector2 input)
    {
            animator.SetBool("Walking", true);
            //setting the destination of the navmesh agent to be offset from the player through vertical axial input
            //speed and such are determined by the navmesh agent component itself, attached to the player.
            Vector3 destination = transform.position + transform.right * input.x + transform.forward * input.y;
            navMeshAgent.destination = destination;
    }
}
