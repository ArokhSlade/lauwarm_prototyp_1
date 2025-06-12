using System;
using UnityEngine;

public class ChaseActionState : ActionState
{
    GameObject player;

    public override void Enter()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent.speed = speed;
    }

    public override void Exit()
    { }

    public override void UpdateState()
    {
        if (player == null) return;

        navMeshAgent.destination = player.transform.position;
    }
}
