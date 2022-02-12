using System;
using TMPro;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    public class FloatInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField = null;

        private float _value = 0f;
        public float value
        {
            get => _value;
            set
            {
                SetValueWithoutNotify(value);
                OnValueChanged?.Invoke(value);
            }
        }

        public event Action<float> OnValueChanged;
        public event Action<float> OnEndEdit;

        public void SetValueWithoutNotify(float value)
        {
            _value = value;
            _inputField.SetTextWithoutNotify(ValueToString(value));
        }

        private void Awake()
        {
            SetValueWithoutNotify(_value);
            _inputField.onValueChanged.AddListener(InputField_OnValueChanged);
            _inputField.onEndEdit.AddListener(InputField_OnEndEdit);
        }

        private void InputField_OnValueChanged(string text)
        {
            if (float.TryParse(text, out float floatValue))
            {
                _value = floatValue;
                OnValueChanged?.Invoke(floatValue);
            }
        }

        private void InputField_OnEndEdit(string text)
        {
            if (!float.TryParse(text, out float floatValue))
            {
                SetValueWithoutNotify(_value);
            }

            OnEndEdit?.Invoke(_value);
        }

        private string ValueToString(float value)
        {
            return value.ToString("G8");
        }
    }
}