using System;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    public class EulerGroup : MonoBehaviour
    {
        [SerializeField] private EulerRotationOrder _eulerRotationOrder = null;
        [SerializeField] private Axis _xAxis = null;
        [SerializeField] private Axis _yAxis = null;
        [SerializeField] private Axis _zAxis = null;

        private Euler _euler = new Euler();
        public Euler euler => _euler;

        public event Action<Euler> OnValuesChanged;
        //bool _processOnEulerValueChanged = true;

        private void Awake()
        {
            _euler = new Euler(_eulerRotationOrder.value);
            _eulerRotationOrder.OnValueChanged += (rotationOrder, keepEuler) =>
            {
                //if (!_processOnEulerValueChanged) return;

                if (keepEuler)
                {
                    _euler.rotationOrder = rotationOrder;
                }
                else
                {
                    var rotationMatrix = _euler.ToRotationMatrix();
                    _euler = Euler.FromRotationMatrix(rotationMatrix, rotationOrder);

                    _xAxis.SetValueWithoutNotify(_euler.x);
                    _yAxis.SetValueWithoutNotify(_euler.y);
                    _zAxis.SetValueWithoutNotify(_euler.z);
                }
                OnValuesChanged?.Invoke(_euler);
            };

            _xAxis.OnValueChanged += FloatInputField_OnValueChanged;
            _yAxis.OnValueChanged += FloatInputField_OnValueChanged;
            _zAxis.OnValueChanged += FloatInputField_OnValueChanged;
        }

        public void SetEulerWithoutNotify(Euler euler)
        {
            //_processOnEulerValueChanged = false;

            _euler = euler;
            _xAxis.SetValueWithoutNotify(_euler.x);
            _yAxis.SetValueWithoutNotify(_euler.y);
            _zAxis.SetValueWithoutNotify(_euler.z);

            _eulerRotationOrder.SetValueWithoutNotify(euler.rotationOrder);

            //_processOnEulerValueChanged = true;
        }
        
        private void FloatInputField_OnValueChanged(Axis axis)
        {
            //if (!_processOnEulerValueChanged) return;

            if (axis == _xAxis) _euler.x = axis.value;
            else if (axis == _yAxis) _euler.y = axis.value;
            else _euler.z = axis.value;
            OnValuesChanged?.Invoke(_euler);
        }
    }
}