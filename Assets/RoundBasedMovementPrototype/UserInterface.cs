using Assets.RoundBasedMovementPrototype;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] PlayerInteraction playerInteraction;

    public void EndPlanningPhase()
    {
        if (playerInteraction == null) return;

        playerInteraction.EndPlanningPhase();
    }
}
