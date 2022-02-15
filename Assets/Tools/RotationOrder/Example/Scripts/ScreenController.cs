using UnityEngine;
using Tools.RotationOrder.Example.UI;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Tools.RotationOrder.Example
{
    public class ScreenController : MonoBehaviour
    {
        [SerializeField] private Euler _startEuler = new Euler();

        [SerializeField] private List<RotationVisualizationGroup> _rotationVisualizationGroups = null;
        [SerializeField] private QuaternionGroup _quaternionGroup = null;
        [SerializeField] private Matrix4x4Group _matrix4x4Group = null;
        [SerializeField] private AxisAngleGroup _axisAngleGroup = null;

        [SerializeField] private Button _showRotationOrderButton = null;

        private void Awake()
        {
            Quaternion quaternion = _startEuler.ToQuaternion();

            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _quaternionGroup.OnValueChanged += QuaternionGroup_OnValueChanged;

            _matrix4x4Group.SetValueWithoutNotify(quaternion);
            _matrix4x4Group.OnValueChanged += Matrix4x4Group_OnValueChanged;

            _axisAngleGroup.SetValueWithoutNotify(quaternion);
            _axisAngleGroup.OnValueChanged += AngleAxisGroup_OnValueChanged;

            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(_startEuler);
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(_startEuler);
                rotationVisualizationGroup.eulerGroup.OnValuesChanged += EulerGroup_OnValuesChanged;
            }

            _showRotationOrderButton.onClick.AddListener(() =>
            {
                foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
                {
                    rotationVisualizationGroup.modelRotationVisualizer.AnimateShowOrder();
                }
            });
        }

        private void AngleAxisGroup_OnValueChanged(Quaternion quaternion)
        {
            SetRotationVisualizationGroups(quaternion);

            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _matrix4x4Group.SetValueWithoutNotify(quaternion);
        }

        private void QuaternionGroup_OnValueChanged(Quaternion quaternion)
        {
            SetRotationVisualizationGroups(quaternion);

            _matrix4x4Group.SetValueWithoutNotify(quaternion);
            _axisAngleGroup.SetValueWithoutNotify(quaternion);
        }

        private void Matrix4x4Group_OnValueChanged(Quaternion quaternion)
        {
            SetRotationVisualizationGroups(quaternion);

            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _axisAngleGroup.SetValueWithoutNotify(quaternion);
        }

        private void SetRotationVisualizationGroups (Quaternion quaternion)
        {
            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                var euler = rotationVisualizationGroup.eulerGroup.euler;
                euler = quaternion.ToEuler(euler.rotationOrder);
                rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(euler);
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(euler);
            }
        }

        private void EulerGroup_OnValuesChanged(EulerGroup eulerGroup, Euler previousEuler)
        {
            var euler = eulerGroup.euler;
            Matrix4x4 matrix4x4;
            
            if (!eulerGroup.keepEuler && previousEuler.rotationOrder != euler.rotationOrder)
            {
                matrix4x4 = previousEuler.ToRotationMatrix();

                euler = Euler.FromRotationMatrix(matrix4x4, euler.rotationOrder);
                eulerGroup.SetEulerWithoutNotify(euler);
                foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
                {
                    if (rotationVisualizationGroup.eulerGroup == eulerGroup)
                    {
                        rotationVisualizationGroup.modelRotationVisualizer.SetEuler(euler);
                    }
                    else
                    {
                        rotationVisualizationGroup.modelRotationVisualizer.ResetView();
                    }
                }
                return;
            }

            matrix4x4 = euler.ToRotationMatrix();

            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                var otherEuler = rotationVisualizationGroup.eulerGroup.euler;

                if (rotationVisualizationGroup.eulerGroup != eulerGroup)
                {
                    otherEuler = otherEuler.rotationOrder == euler.rotationOrder?
                        euler : Euler.FromRotationMatrix(matrix4x4, otherEuler.rotationOrder);
                    rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(otherEuler);
                }
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(otherEuler);
            }

            Quaternion quaternion = euler.ToQuaternion();

            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _matrix4x4Group.SetValueWithoutNotify(matrix4x4);
            _axisAngleGroup.SetValueWithoutNotify(quaternion);
        }

        [Serializable]
        private class RotationVisualizationGroup
        {
            public EulerGroup eulerGroup = null;
            public ModelRotationVisualizer modelRotationVisualizer = null;
        }
    }
}
