using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;

    void Update()
    {
        float newZPosition = transform.position.z + speed * Time.deltaTime;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZPosition);
        transform.position = newPosition;
    }
}
