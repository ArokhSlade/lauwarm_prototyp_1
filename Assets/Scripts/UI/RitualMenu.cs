#nullable disable

using UnityEngine;

public class RitualMenu : MonoBehaviour
{
    [SerializeField] Ritual_Upgrade ritual;
    public void PerformRitual()
    {
        ritual.Perform();
    }
}
