using UnityEngine;

public class Ritual_Upgrade : MonoBehaviour
{
    [SerializeField] Renderer upgradableCrop;
    [SerializeField] Material upgradedMaterial;

    public void Perform()
    {
        UpgradeCrops();
    }

    public void UpgradeCrops()
    {
        upgradableCrop.material = upgradedMaterial;
    }

}
