using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace TicTacToe
{
    
    public class HumanInput : MonoBehaviour
    {
        [SerializeField] FieldState markType;
        Camera cam;

        void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            LayerMask layerMask = 0;
            int fieldLayer = LayerMask.NameToLayer("TicTacToeField");
            layerMask |= 1 << fieldLayer;
            
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
                field.Mark(markType);
            }
        }
    }
}