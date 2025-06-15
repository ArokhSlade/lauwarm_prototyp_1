using System.Collections;
using UnityEngine;

namespace TicTacToe
{
    public class Markers : MonoBehaviour
    {
        private static Markers instance = new();
        private bool isInitialized = false;
        public static Markers Instance => instance;
        [SerializeField] GameObject XMarkerPrefab;
        [SerializeField] GameObject OMarkerPrefab;
        private Markers()
        { 
            Initialize();
        }

        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }
            //TODO(Gerald, 2025 06 15): empty for now

            isInitialized = true;
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