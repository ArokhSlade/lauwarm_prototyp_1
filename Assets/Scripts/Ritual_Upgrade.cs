using UnityEngine;
using System.Collections.Generic;
public class Ritual_Upgrade : MonoBehaviour
{
    [SerializeField] List<Renderer> upgradableCrops;
    [SerializeField] GameObject fragileCrop;
    [SerializeField] Material upgradedMaterial;

    public void Perform()
    {
        UpgradeCrops();
    }

    public void UpgradeCrops()
    {
        foreach (var upgradableCrop in upgradableCrops)
        {
            upgradableCrop.material = upgradedMaterial;

        }

        fragileCrop.SetActive(false);
    }

}
