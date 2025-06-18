#nullable disable

using System.Collections;
using UnityEngine;

namespace TicTacToe
{
    public class Markers : MonoBehaviour
    {
        private static Markers instance;
        public static Markers Instance => instance;

        [SerializeField] GameObject XMarkerPrefab;
        [SerializeField] GameObject OMarkerPrefab;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        public GameObject Create(FieldState markerType, Transform parent)
        {
            if (markerType == FieldState.Empty)
            {
                return null;
            }
            GameObject markerPrefab = null;
            switch (markerType)
            {
                case FieldState.X:
                    markerPrefab = XMarkerPrefab;
                    break;
                case FieldState.O:
                    markerPrefab = OMarkerPrefab;
                    break;
                default:
                    Debug.LogError($"Unexpected Marker Type: {markerType}");
                    return null;
            }

            var position = new Vector3(0, 0, 0);
            var offset = new Vector3(0, 1, 0);
            var rotation = Quaternion.identity;

            if (parent)
            {
                position = parent.transform.position;
            }

            position += offset;

            GameObject marker = Instantiate(markerPrefab, position, rotation, parent);

            return marker;
        }

    }
}