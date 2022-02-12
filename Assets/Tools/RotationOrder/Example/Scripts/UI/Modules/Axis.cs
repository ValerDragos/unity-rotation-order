using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.RotationOrder.Example.UI
{
    public class Axis : MonoBehaviour
    {
        const int FULL_ROTATION_EULER = 360;

        [SerializeField] private FloatInputField _floatInputField = null;
        [SerializeField] private Slider _slider = null;

        public float value
        {
            get => _floatInputField.value;
            set
            {
                SetValueWithoutNotify(value);
                CallOnValueChanged();
            }
        }

        public event Action<Axis> OnValueChanged;

        private void Awake()
        {
            _slider.minValue = -FULL_ROTATION_EULER;
            _slider.maxValue = FULL_ROTATION_EULER;

            _floatInputField.OnValueChanged += FloatInputField_OnValueChanged;

            _floatInputField.OnEndEdit += (value) =>
            {
                _floatInputField.value = FormatValue(_floatInputField.value);
            };

            _slider.onValueChanged.AddListener((value) =>
            {
                _floatInputField.SetValueWithoutNotify(value);
                CallOnValueChanged();
            });
        }

        private void FloatInputField_OnValueChanged(float value)
        {
            value = FormatValue(value);
            _slider.SetValueWithoutNotify(value);

            CallOnValueChanged();
        }

        public void SetValueWithoutNotify (float value)
        {
            value = FormatValue(value);
            _floatInputField.SetValueWithoutNotify(value);
            _slider.SetValueWithoutNotify(value);
        }

        private float FormatValue (float value)
        {
            return value % FULL_ROTATION_EULER;
        }

        private void CallOnValueChanged()
        {
            OnValueChanged?.Invoke(this);
        }
    }
}