using System;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    public class QuaternionGroup : MonoBehaviour
    {
        [SerializeField] private FloatInputField _xFloatInputField = null;
        [SerializeField] private FloatInputField _yFloatInputField = null;
        [SerializeField] private FloatInputField _zFloatInputField = null;
        [SerializeField] private FloatInputField _wFloatInputField = null;

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
        //public event Action<Quaternion> OnEndEdit;

        public void SetValueWithoutNotify(Quaternion quaternion)
        {
            _xFloatInputField.SetValueWithoutNotify(quaternion.x);
            _yFloatInputField.SetValueWithoutNotify(quaternion.y);
            _zFloatInputField.SetValueWithoutNotify(quaternion.z);
            _wFloatInputField.SetValueWithoutNotify(quaternion.w);
        }

        private void Awake()
        {
            _xFloatInputField.OnValueChanged += InputField_OnValueChanged;
            _yFloatInputField.OnValueChanged += InputField_OnValueChanged;
            _zFloatInputField.OnValueChanged += InputField_OnValueChanged;
            _wFloatInputField.OnValueChanged += InputField_OnValueChanged;

            //_xFloatInputField.OnEndEdit += InputField_OnEndEdit;
            //_yFloatInputField.OnEndEdit += InputField_OnEndEdit;
            //_zFloatInputField.OnEndEdit += InputField_OnEndEdit;
            //_wFloatInputField.OnEndEdit += InputField_OnEndEdit;
        }

        private void InputField_OnValueChanged(float value)
        {
            CallOnValueChanged();
        }

        //private void InputField_OnEndEdit(float value)
        //{
        //    OnEndEdit?.Invoke(CreateQuaternion());
        //}

        private Quaternion CreateQuaternion ()
        {
            return new Quaternion(_xFloatInputField.value, _yFloatInputField.value, _zFloatInputField.value, _wFloatInputField.value);
        }

        private void CallOnValueChanged()
        {
            OnValueChanged?.Invoke(CreateQuaternion());
        }
    }
}