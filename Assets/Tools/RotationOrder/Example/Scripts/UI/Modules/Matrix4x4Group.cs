using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.RotationOrder.Example.UI
{
    public class Matrix4x4Group : MonoBehaviour
    {
        [SerializeField] private Button _normalizeButton = null;
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
            _normalizeButton.onClick.AddListener(() =>
            {
                SetValueWithoutNotify(CreateNormalizedQuaternion());
            });

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

        private Quaternion CreateNormalizedQuaternion ()
        {
            var matrix4x4 = CreateMatrix4x4();
            var quaternion = ToQuaternion(matrix4x4);
            quaternion.Normalize();
            return quaternion;
        }

        private static Quaternion ToQuaternion(Matrix4x4 m)
        {
            // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
            Quaternion q = new Quaternion();
            q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
            q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
            q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
            q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
            q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
            q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
            q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
            return q;
        }

        private void CallOnValueChanged()
        {
            OnValueChanged?.Invoke(CreateNormalizedQuaternion());
        }
    }
}