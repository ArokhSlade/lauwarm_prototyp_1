using UnityEngine;

public class RitualUpgrade : MonoBehaviour
{
    Renderer upgradableCrop;
    Material upgradedMaterial;


    void Start()
    {
        
    }

    void UpgradeCrops()
    {
        upgradableCrop.material = upgradedMaterial;
    }

    void Update()
    {
        
    }
}
