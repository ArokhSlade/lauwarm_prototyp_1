using Unity.VisualScripting;
using UnityEngine;


public class Lantern : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] CamouflageType detectedTypes;
    [SerializeField] SphereCollider detector;

    void Update()
    {
        //detector.radius = light.range;
    }

    //void FixedUpdate()
    //{
    //    Collider[] overlaps = Physics.OverlapSphere(transform.position, light.range);
    //    foreach (var thing in overlaps)
    //    {
    //        var camouflaged = thing.GetComponent<Camouflaged>();

    //        if (camouflaged == null) continue;

    //        if (camouflaged.Type.HasFlag(detectedTypes))
    //        {
    //            camouflaged.Reveal();
    //        } else
    //        {
    //            camouflaged.Hide();
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        var camouflaged = other.GetComponent<Camouflaged>();

        if (camouflaged == null) return;

        if (camouflaged.Type.HasFlag(detectedTypes))
        {
            camouflaged.Reveal();
        }
        else
        {
            camouflaged.Hide();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("Trigger Exit");
        var camouflaged = collider.gameObject.GetComponent<Camouflaged>();

        if (camouflaged != null)
        {
            camouflaged.Hide();
        }
       
    }
}
