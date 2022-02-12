using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tools.RotationOrder.Example
{
    public class EulerRotationOrder : MonoBehaviour
    {
        private const string AXIS_PATTERN = "<color=#{0}>{1}</color>"; 
        private const string OPTION_PATTERN = "{0} {1} {2}";

        [SerializeField] private Settings _settings = null;
        [SerializeField] private TMP_Dropdown _dropDown = null;

        public Action<Euler.RotationOrder> OnValueChanged;

        private Euler.RotationOrder _value = Euler.RotationOrder.XYZ;
        public Euler.RotationOrder value
        {
            get => _value;
            set
            {
                SetValueWithoutNotify(value);
                OnValueChanged?.Invoke(value);
            }
        }

        public void SetValueWithoutNotify(Euler.RotationOrder rotationOrder)
        {
            _value = rotationOrder;
            _dropDown.SetValueWithoutNotify((int)rotationOrder);
        }

        private void Awake()
        {
            var options = new List<string>(6);

            string xString = GetFormattedString(_settings.GetInputFieldDataFromType(Settings.InputFieldType.AxisX));
            string yString = GetFormattedString(_settings.GetInputFieldDataFromType(Settings.InputFieldType.AxisY));
            string zString = GetFormattedString(_settings.GetInputFieldDataFromType(Settings.InputFieldType.AxisZ));

            options.Add(string.Format(OPTION_PATTERN, xString, yString, zString));
            options.Add(string.Format(OPTION_PATTERN, xString, zString, yString));
            options.Add(string.Format(OPTION_PATTERN, yString, xString, zString));
            options.Add(string.Format(OPTION_PATTERN, yString, zString, xString));
            options.Add(string.Format(OPTION_PATTERN, zString, xString, yString));
            options.Add(string.Format(OPTION_PATTERN, zString, yString, xString));

            _dropDown.AddOptions(options);
            _dropDown.value = 0;
            _dropDown.onValueChanged.AddListener(TMP_Dropdown_onValueChanged);
        }

        private void TMP_Dropdown_onValueChanged(int intValue)
        {
            var newValue = (Euler.RotationOrder)intValue;
            if (newValue == value) return;
            _value = newValue;
            OnValueChanged?.Invoke(newValue);
        }

        private string GetFormattedString (Settings.InputFieldData axisData)
        {
            return string.Format(AXIS_PATTERN, ColorUtility.ToHtmlStringRGBA(axisData.color), axisData.name);
        }
    }
}