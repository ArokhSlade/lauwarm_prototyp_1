using UnityEngine;

[RequireComponent(typeof(EnemyBrain))]
public class RetreatActionState : ActionState
{
    public override void Enter()
    {
        EnemyBrain enemyBrain = GetComponent<EnemyBrain>();

        Vector3 basePosition = enemyBrain.GetBasePosition();
        navMeshAgent.destination = basePosition;
    }

    public override void Exit()
    { }

    public override void UpdateState()
    { }
}
