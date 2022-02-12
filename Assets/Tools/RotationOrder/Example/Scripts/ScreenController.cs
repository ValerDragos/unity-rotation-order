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
        [SerializeField] private AngleAxisGroup _angleAxisGroup = null;

        [SerializeField] private Button _showRotationOrderButton = null;
        [SerializeField] private Toggle _keepEulerToggle = null;

        private void Awake()
        {
            Matrix4x4 matrix4x4 = _startEuler.ToRotationMatrix();
            Quaternion quaternion = _startEuler.ToQuaternion();

            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _quaternionGroup.OnValueChanged += QuaternionGroup_OnValueChanged;
           // _quaternionGroup.OnEndEdit += QuaternionGroup_OnEndEdit;

            _matrix4x4Group.SetValueWithoutNotify(matrix4x4);
            _matrix4x4Group.OnValueChanged += Matrix4x4Group_OnValueChanged;

            _angleAxisGroup.SetValueWithoutNotify(quaternion);
            _angleAxisGroup.OnValueChanged += AngleAxisGroup_OnValueChanged;

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
            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                var euler = rotationVisualizationGroup.eulerGroup.euler;
                euler = quaternion.ToEuler(euler.rotationOrder);
                rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(euler);
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(euler);
            }

            var matrix4x4 = Matrix4x4.Rotate(quaternion);
            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _matrix4x4Group.SetValueWithoutNotify(matrix4x4);
        }

        private void QuaternionGroup_OnValueChanged(Quaternion quaternion)
        {
            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                var euler = rotationVisualizationGroup.eulerGroup.euler;
                euler = quaternion.ToEuler(euler.rotationOrder);
                rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(euler);
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(euler);
            }

            var matrix4x4 = Matrix4x4.Rotate(quaternion);
            _matrix4x4Group.SetValueWithoutNotify(matrix4x4);
            _angleAxisGroup.SetValueWithoutNotify(quaternion);
        }

        private void Matrix4x4Group_OnValueChanged(Matrix4x4 matrix4x4)
        {
            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                var euler = rotationVisualizationGroup.eulerGroup.euler;
                euler = matrix4x4.ToEuler(euler.rotationOrder);
                rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(euler);
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(euler);
            }

            var quaternion = Quaternion.LookRotation(matrix4x4.GetColumn(2), matrix4x4.GetColumn(1));
            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _angleAxisGroup.SetValueWithoutNotify(quaternion);
        }

        //private void QuaternionGroup_OnEndEdit(Quaternion quaternion)
        //{
        //    quaternion.Normalize();
        //    QuaternionGroup_OnValueChanged(quaternion);
        //    _quaternionGroup.SetValueWithoutNotify(quaternion);
        //}

        // Work on this ... its verry messy
        private void EulerGroup_OnValuesChanged(EulerGroup eulerGroup, Euler previousEuler)
        {
            var euler = eulerGroup.euler;
            Matrix4x4 matrix4x4;
            bool updateSelf = false;
            
            if (!_keepEulerToggle.isOn && previousEuler.rotationOrder != euler.rotationOrder)
            {
                matrix4x4 = previousEuler.ToRotationMatrix();

                euler = Euler.FromRotationMatrix(matrix4x4, euler.rotationOrder);
                updateSelf = true;
            }
            else
            {
                matrix4x4 = euler.ToRotationMatrix();
            }

            Quaternion quaternion = euler.ToQuaternion();

            _quaternionGroup.SetValueWithoutNotify(quaternion);
            _matrix4x4Group.SetValueWithoutNotify(matrix4x4);
            _angleAxisGroup.SetValueWithoutNotify(quaternion);

            foreach (var rotationVisualizationGroup in _rotationVisualizationGroups)
            {
                var otherEuler = rotationVisualizationGroup.eulerGroup.euler;

                if (updateSelf || rotationVisualizationGroup.eulerGroup != eulerGroup)
                {
                    otherEuler = otherEuler.rotationOrder == euler.rotationOrder?
                        euler : Euler.FromRotationMatrix(matrix4x4, otherEuler.rotationOrder);
                    rotationVisualizationGroup.eulerGroup.SetEulerWithoutNotify(otherEuler);
                }
                rotationVisualizationGroup.modelRotationVisualizer.SetEuler(otherEuler);
            }
        }

        //private void UpdateGroup(RotationData rotationData, Panel to)
        //{
        //    var toRotationData = to.rotationData;
        //    to.rotationData = ChangeRotationOrder(rotationData, toRotationData.euler.rotationOrder);
        //}

        //private Euler ChangeRotationOrder(Euler euler, Matrix4x4 matrix4x4, Euler.RotationOrder rotationOrder)
        //{
        //    if (euler.rotationOrder == rotationOrder) return euler;
        //    return Euler.FromRotationMatrix(matrix4x4, rotationOrder);
        //}

        [Serializable]
        private class RotationVisualizationGroup
        {
            public EulerGroup eulerGroup = null;
            public ModelRotationVisualizer modelRotationVisualizer = null;
        }
    }
}
