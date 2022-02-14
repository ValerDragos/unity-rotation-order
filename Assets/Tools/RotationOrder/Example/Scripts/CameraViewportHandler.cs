using UnityEngine;

namespace Tools.RotationOrder.Example
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class CameraViewportHandler : MonoBehaviour
    {
        [SerializeField] private Camera _uiCamera = null;
        [SerializeField] private Camera _camera = null;

        private RectTransform _cacheRectTransform = null;
        public RectTransform cacheRectTransform
        {
            get
            {
                if (_cacheRectTransform == null) _cacheRectTransform = GetComponent<RectTransform>();
                return _cacheRectTransform;
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (_uiCamera == null || _camera == null) return;

            Vector3[] corners = new Vector3[4];

            cacheRectTransform.GetWorldCorners(corners);
            Vector3 viewportPoint = _uiCamera.WorldToViewportPoint(corners[0]);
            Vector2 min = viewportPoint;
            Vector2 max = viewportPoint;

            for (int i = 1; i < corners.Length; ++i)
            {
                viewportPoint = _uiCamera.WorldToViewportPoint(corners[i]);
                if (viewportPoint.x < min.x) min.x = viewportPoint.x;
                else if (viewportPoint.x > max.x) max.x = viewportPoint.x;
                if (viewportPoint.y < min.y) min.y = viewportPoint.y;
                else if (viewportPoint.y > max.y) max.y = viewportPoint.y;

                _camera.rect = new Rect(min, max - min);
            }
        }
    }
}
