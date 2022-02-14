using UnityEngine;
using UnityEngine.UI;

namespace Tools.RotationOrder.Example.UI
{
    public class EulerGroup : MonoBehaviour
    {
        public delegate void ValuesChangedCallback (EulerGroup eulerGroup, Euler previousEuler);

        [SerializeField] private EulerRotationOrder _eulerRotationOrder = null;
        [SerializeField] private Axis _xAxis = null;
        [SerializeField] private Axis _yAxis = null;
        [SerializeField] private Axis _zAxis = null;
        [SerializeField] private Toggle _keepEulerToggle = null;

        private Euler _euler = new Euler();
        public Euler euler => _euler;

        public event ValuesChangedCallback OnValuesChanged;
        public bool keepEuler => _keepEulerToggle.isOn;

        private void Awake()
        {
            _euler = new Euler(_eulerRotationOrder.value);
            _eulerRotationOrder.OnValueChanged += (rotationOrder) =>
            {
                var previousEuler = _euler;
                _euler.rotationOrder = rotationOrder;
                OnValuesChanged?.Invoke(this, previousEuler);
            };

            _xAxis.OnValueChanged += Axis_OnValueChanged;
            _yAxis.OnValueChanged += Axis_OnValueChanged;
            _zAxis.OnValueChanged += Axis_OnValueChanged;
        }

        public void SetEulerWithoutNotify(Euler euler)
        {
            _euler = euler;
            _xAxis.SetValueWithoutNotify(_euler.x);
            _yAxis.SetValueWithoutNotify(_euler.y);
            _zAxis.SetValueWithoutNotify(_euler.z);

            _eulerRotationOrder.SetValueWithoutNotify(_euler.rotationOrder);
        }
        
        private void Axis_OnValueChanged(Axis axis)
        {
            var previousEuler = _euler;
            if (axis == _xAxis) _euler.x = axis.value;
            else if (axis == _yAxis) _euler.y = axis.value;
            else _euler.z = axis.value;
            OnValuesChanged?.Invoke(this, previousEuler);
        }
    }
}