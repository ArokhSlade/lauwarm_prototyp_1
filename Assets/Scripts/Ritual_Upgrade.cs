using UnityEngine;

public class Ritual_Upgrade : MonoBehaviour
{
    [SerializeField] Renderer upgradableCrop;
    [SerializeField] GameObject fragileCrop;
    [SerializeField] Material upgradedMaterial;

    public void Perform()
    {
        UpgradeCrops();
    }

    public void UpgradeCrops()
    {
        upgradableCrop.material = upgradedMaterial;
        fragileCrop.SetActive(false);
    }

}
