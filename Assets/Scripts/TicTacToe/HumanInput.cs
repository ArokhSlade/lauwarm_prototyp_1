using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace TicTacToe
{
    
    public class HumanInput : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;

        Camera cam;

        void Reset()
        {
             layerMask = LayerMask.GetMask(new string[] { "TicTacToeField" });
        }

        void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            bool wasHit = Physics.Raycast(ray, out hit, 100f, layerMask);
            Debug.DrawRay(ray.origin, ray.direction);
            if (!wasHit)
            {
                return;
            }
            
            Field field = hit.collider.GetComponent<Field>();
            if (field == null)
            {
                return;
            }

            if (Input.GetMouseButtonUp((int)MouseButton.Left))
            {
                field.RequestMark();
            }
        }
    }
}