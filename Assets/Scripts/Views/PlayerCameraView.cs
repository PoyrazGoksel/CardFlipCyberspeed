using Events;
using Extensions.Unity.MonoHelper;
using UnityEngine;

namespace Views
{
    public class PlayerCameraView : EventListenerMono
    {
        [SerializeField] private float _padding = 0.25f;
        [SerializeField] private Camera _cam;

        private void FitBounds(Bounds targetBounds)
        {
            float verticalFOV = _cam.fieldOfView * Mathf.Deg2Rad;
            float aspectRatio = _cam.aspect;

            float halfHeight = (targetBounds.size.z * 0.5f) + _padding;
            float halfWidth = (targetBounds.size.x * 0.5f) + _padding;
            
            float requiredDistance;
            if (targetBounds.size.x > targetBounds.size.z)
            {
                float horizontalFOV = 2 * Mathf.Atan(Mathf.Tan(verticalFOV / 2) * aspectRatio);
                requiredDistance = halfWidth / Mathf.Tan(horizontalFOV / 2);
            }
            else
            {
                requiredDistance = halfHeight / Mathf.Tan(verticalFOV / 2);
            }

            Vector3 center = targetBounds.center;
            Vector3 cameraDirection = _cam.transform.forward;
            _cam.transform.position = center - cameraDirection * requiredDistance;

            _cam.farClipPlane = requiredDistance + targetBounds.extents.magnitude * _padding;
        }

        protected override void RegisterEvents()
        {
            GridEvents.GridStart += OnGridStart;
        }

        private void OnGridStart(Bounds arg0)
        {
            FitBounds(arg0);
        }
        
        protected override void UnRegisterEvents()
        {
            GridEvents.GridStart -= OnGridStart;
        }
    }
}