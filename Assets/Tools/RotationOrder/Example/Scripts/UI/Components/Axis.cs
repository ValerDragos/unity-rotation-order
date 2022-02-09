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
                OnValueChanged?.Invoke(this);
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

                OnValueChanged?.Invoke(this);
            });
        }

        private void FloatInputField_OnValueChanged(float value)
        {
            value = FormatValue(value);
            _slider.SetValueWithoutNotify(value);

            OnValueChanged?.Invoke(this);
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
    }
}