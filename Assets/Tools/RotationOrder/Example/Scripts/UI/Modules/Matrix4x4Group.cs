using System;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    public class Matrix4x4Group : MonoBehaviour
    {
        [Header("Column 1")]
        [SerializeField] private FloatInputField _component0x0 = null;
        [SerializeField] private FloatInputField _component1x0 = null;
        [SerializeField] private FloatInputField _component2x0 = null;
        [SerializeField] private FloatInputField _component3x0 = null;
        [Header("Column 2")]
        [SerializeField] private FloatInputField _component0x1 = null;
        [SerializeField] private FloatInputField _component1x1 = null;
        [SerializeField] private FloatInputField _component2x1 = null;
        [SerializeField] private FloatInputField _component3x1 = null;
        [Header("Column 3")]
        [SerializeField] private FloatInputField _component0x2 = null;
        [SerializeField] private FloatInputField _component1x2 = null;
        [SerializeField] private FloatInputField _component2x2 = null;
        [SerializeField] private FloatInputField _component3x2 = null;
        [Header("Column 4")]
        [SerializeField] private FloatInputField _component0x3 = null;
        [SerializeField] private FloatInputField _component1x3 = null;
        [SerializeField] private FloatInputField _component2x3 = null;
        [SerializeField] private FloatInputField _component3x3 = null;

        public Matrix4x4 matrix4x4
        {
            get => CreateMatrix4x4();
            set
            {
                SetValueWithoutNotify(value);
                CallOnValueChanged();
            }
        }

        public event Action<Quaternion> OnValueChanged;

        public void SetValueWithoutNotify (Quaternion quaternion)
        {
            SetValueWithoutNotify(Matrix4x4.Rotate(quaternion));
        }

        public void SetValueWithoutNotify(Matrix4x4 matrix4x4)
        {
            _component0x0.SetValueWithoutNotify(matrix4x4.m00);
            _component1x0.SetValueWithoutNotify(matrix4x4.m10);
            _component2x0.SetValueWithoutNotify(matrix4x4.m20);
            _component3x0.SetValueWithoutNotify(matrix4x4.m30);

            _component0x1.SetValueWithoutNotify(matrix4x4.m01);
            _component1x1.SetValueWithoutNotify(matrix4x4.m11);
            _component2x1.SetValueWithoutNotify(matrix4x4.m21);
            _component3x1.SetValueWithoutNotify(matrix4x4.m31);

            _component0x2.SetValueWithoutNotify(matrix4x4.m02);
            _component1x2.SetValueWithoutNotify(matrix4x4.m12);
            _component2x2.SetValueWithoutNotify(matrix4x4.m22);
            _component3x2.SetValueWithoutNotify(matrix4x4.m32);

            _component0x3.SetValueWithoutNotify(matrix4x4.m03);
            _component1x3.SetValueWithoutNotify(matrix4x4.m13);
            _component2x3.SetValueWithoutNotify(matrix4x4.m23);
            _component3x3.SetValueWithoutNotify(matrix4x4.m33);
        }

        private void Awake()
        {
            _component0x0.OnValueChanged += InputField_OnValueChanged;
            _component1x0.OnValueChanged += InputField_OnValueChanged;
            _component2x0.OnValueChanged += InputField_OnValueChanged;
            _component3x0.OnValueChanged += InputField_OnValueChanged;

            _component0x1.OnValueChanged += InputField_OnValueChanged;
            _component1x1.OnValueChanged += InputField_OnValueChanged;
            _component2x1.OnValueChanged += InputField_OnValueChanged;
            _component3x1.OnValueChanged += InputField_OnValueChanged;

            _component0x2.OnValueChanged += InputField_OnValueChanged;
            _component1x2.OnValueChanged += InputField_OnValueChanged;
            _component2x2.OnValueChanged += InputField_OnValueChanged;
            _component3x2.OnValueChanged += InputField_OnValueChanged;

            _component0x3.OnValueChanged += InputField_OnValueChanged;
            _component1x3.OnValueChanged += InputField_OnValueChanged;
            _component2x3.OnValueChanged += InputField_OnValueChanged;
            _component3x3.OnValueChanged += InputField_OnValueChanged;
        }

        private void InputField_OnValueChanged(float value)
        {
            CallOnValueChanged();
        }

        private Matrix4x4 CreateMatrix4x4()
        {
            return new Matrix4x4(
                new Vector4(_component0x0.value, _component1x0.value, _component2x0.value, _component3x0.value),
                new Vector4(_component0x1.value, _component1x1.value, _component2x1.value, _component3x1.value),
                new Vector4(_component0x2.value, _component1x2.value, _component2x2.value, _component3x2.value),
                new Vector4(_component0x3.value, _component1x3.value, _component2x3.value, _component3x3.value));
        }

        private Quaternion CreateQuaternion ()
        {
            var matrix4x4 = CreateMatrix4x4();
            var quaternion = matrix4x4.ToQuaternion();
            quaternion.Normalize();
            return quaternion;
        }

        private void CallOnValueChanged()
        {
            OnValueChanged?.Invoke(CreateQuaternion());
        }
    }
}