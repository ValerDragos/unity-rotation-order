using System;
using System.Collections;
using UnityEngine;

namespace Tools.RotationOrder.Example
{
    public class ModelRotationVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform _modelTransform = null;
        [SerializeField] private float _axisAnimationTime = 0f;
        [SerializeField] private float _animationPauseTime = 0f;
        [SerializeField] private float _backToIdentityAnimationTime = 0f;

        [SerializeField] private AxisHandler _xAxis = null;
        [SerializeField] private AxisHandler _yAxis = null;
        [SerializeField] private AxisHandler _zAxis = null;

        public RotationData rotationData { get; private set; }

        private Coroutine _animateShowOrderCoroutine = null;

        private void Awake()
        {
            _xAxis.SetActive();
            _yAxis.SetActive();
            _zAxis.SetActive();
        }

        public void Set(RotationData rotationData)
        {
            this.rotationData = rotationData;
            _modelTransform.rotation = rotationData.quaternion;
        }

        public void AnimateShowOrder()
        {
            if (_animateShowOrderCoroutine != null)
            {
                StopCoroutine(_animateShowOrderCoroutine);
            }
            _animateShowOrderCoroutine = StartCoroutine(AnimateShowOrderCoroutine());
        }

        private IEnumerator AnimateShowOrderCoroutine()
        {
            _xAxis.SetActive();
            _yAxis.SetActive();
            _zAxis.SetActive();

            yield return UpdateTransform(rotationData.quaternion, Quaternion.identity, _backToIdentityAnimationTime);

            _xAxis.SetNormal();
            _yAxis.SetNormal();
            _zAxis.SetNormal();

            yield return new WaitForSecondsRealtime(_animationPauseTime);

            var xRotationData = new RotationVisualData(new Vector3(rotationData.euler.x, 0f, 0f), _xAxis);
            var yRotationData = new RotationVisualData(new Vector3(0f, rotationData.euler.y, 0f), _yAxis);
            var zRotationData = new RotationVisualData(new Vector3(0f, 0f, rotationData.euler.z), _zAxis);

            switch (rotationData.euler.rotationOrder)
            {
                case Euler.RotationOrder.XYZ:
                    yield return DoAxisRotations(xRotationData, yRotationData, zRotationData);
                    break;
                case Euler.RotationOrder.XZY:
                    yield return DoAxisRotations(xRotationData, zRotationData, yRotationData);
                    break;
                case Euler.RotationOrder.YXZ:
                    yield return DoAxisRotations(yRotationData, xRotationData, zRotationData);
                    break;
                case Euler.RotationOrder.YZX:
                    yield return DoAxisRotations(yRotationData, zRotationData, xRotationData);
                    break;
                case Euler.RotationOrder.ZXY:
                    yield return DoAxisRotations(zRotationData, xRotationData, yRotationData);
                    break;
                case Euler.RotationOrder.ZYX:
                    yield return DoAxisRotations(zRotationData, yRotationData, xRotationData);
                    break;
            }

            _xAxis.SetActive();
            _yAxis.SetActive();
            _zAxis.SetActive();
        }

        private IEnumerator DoAxisRotations(params RotationVisualData[] rotationDataArray)
        {
            var startRotation = Quaternion.identity;

            foreach (var rotationData in rotationDataArray)
            {
                var endRotation = startRotation * Quaternion.Euler(rotationData.eulerAngles);
                rotationData.axisHandler.SetActive();
                yield return UpdateTransform(startRotation, endRotation, _axisAnimationTime);
                rotationData.axisHandler.SetNormal();
                startRotation = endRotation;
            }
        }

        private IEnumerator UpdateTransform(Quaternion startRotation, Quaternion endRotation, float duration)
        {
            float time = 0f;
            float factor;

            while (time < duration)
            {
                factor = time / duration;
                _modelTransform.rotation = Quaternion.Lerp(startRotation, endRotation, factor);
                yield return null;
                time += Time.unscaledDeltaTime;
            }

            _modelTransform.rotation = endRotation;
        }

        private struct RotationVisualData
        {
            public Vector3 eulerAngles;
            public AxisHandler axisHandler;

            public RotationVisualData(Vector3 eulerAngles, AxisHandler axisHandler) =>
                (this.eulerAngles, this.axisHandler) = (eulerAngles, axisHandler);
        }

        [Serializable]
        private class AxisHandler
        {
            [SerializeField] private Renderer[] _renderers = null;
            [SerializeField] private Material _normalMaterial = null;
            [SerializeField] private Material _activeMaterial = null;

            public void SetActive()
            {
                SetMaterial(_activeMaterial);
            }

            public void SetNormal()
            {
                SetMaterial(_normalMaterial);
            }

            private void SetMaterial(Material material)
            {
                foreach (var renderer in _renderers)
                {
                    renderer.sharedMaterial = material;
                }
            }
        }
    }
}