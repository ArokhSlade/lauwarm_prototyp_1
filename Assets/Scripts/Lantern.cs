using UnityEngine;


public class Lantern : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] CamouflageType detectedTypes;

    void Update()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, light.range);
        foreach (var thing in overlaps)
        {
            var camouflaged = thing.GetComponent<Camouflaged>();

            if (camouflaged == null) continue;

            if (camouflaged.Type.HasFlag(detectedTypes))
            {
                camouflaged.Reveal();
            } else
            {
                camouflaged.Hide();
            }
        }
    }
}
