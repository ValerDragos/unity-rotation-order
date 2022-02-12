using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.RotationOrder.Example.UI
{
    public class AngleAxisGroup : MonoBehaviour
    {
        [SerializeField] private FloatInputField _xFloatInputField = null;
        [SerializeField] private FloatInputField _yFloatInputField = null;
        [SerializeField] private FloatInputField _zFloatInputField = null;
        [SerializeField] private Axis _angle = null;
        [SerializeField] private Button _normalizeAxisButton = null;

        public Quaternion quaternion
        {
            get => CreateQuaternion();
            set
            {
                SetValueWithoutNotify(value);
                CallOnValueChanged();
            }
        }

        public event Action<Quaternion> OnValueChanged;

        public void SetValueWithoutNotify(Quaternion quaternion)
        {
            quaternion.ToAngleAxis(out float angle, out Vector3 axis);

            SetAxisWithoutNotify(axis);
            _angle.SetValueWithoutNotify(angle);
        }

        private void Awake()
        {
            _xFloatInputField.OnValueChanged += InputField_OnValueChanged;
            _yFloatInputField.OnValueChanged += InputField_OnValueChanged;
            _zFloatInputField.OnValueChanged += InputField_OnValueChanged;
            _angle.OnValueChanged += Angle_OnValueChanged;

            _normalizeAxisButton.onClick.AddListener(() =>
            {
                var axis = GetAxis();
                axis.Normalize();
                SetAxisWithoutNotify(axis);
            });
        }

        private void Angle_OnValueChanged(Axis axis)
        {
            InputField_OnValueChanged(axis.value);
        }

        private void InputField_OnValueChanged(float value)
        {
            CallOnValueChanged();
        }

        private Quaternion CreateQuaternion()
        {
            return Quaternion.AngleAxis(_angle.value, GetAxis());
        }

        private Vector3 GetAxis ()
        {
            return new Vector3(_xFloatInputField.value, _yFloatInputField.value, _zFloatInputField.value);
        }

        private void SetAxisWithoutNotify (Vector3 vector3)
        {
            _xFloatInputField.SetValueWithoutNotify(vector3.x);
            _yFloatInputField.SetValueWithoutNotify(vector3.y);
            _zFloatInputField.SetValueWithoutNotify(vector3.z);
        }

        private void CallOnValueChanged ()
        {
            OnValueChanged?.Invoke(CreateQuaternion());
        }
    }
}