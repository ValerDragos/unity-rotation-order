using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    public class Matrix4x4Group : MonoBehaviour
    {
        [SerializeField] private FloatInputField _component0x0 = null;
        [SerializeField] private FloatInputField _component0x1 = null;
        [SerializeField] private FloatInputField _component0x2 = null;
        [SerializeField] private FloatInputField _component0x3 = null;

        [SerializeField] private FloatInputField _component1x0 = null;
        [SerializeField] private FloatInputField _component1x1 = null;
        [SerializeField] private FloatInputField _component1x2 = null;
        [SerializeField] private FloatInputField _component1x3 = null;

        [SerializeField] private FloatInputField _component2x0 = null;
        [SerializeField] private FloatInputField _component2x1 = null;
        [SerializeField] private FloatInputField _component2x2 = null;
        [SerializeField] private FloatInputField _component2x3 = null;

        [SerializeField] private FloatInputField _component3x0 = null;
        [SerializeField] private FloatInputField _component3x1 = null;
        [SerializeField] private FloatInputField _component3x2 = null;
        [SerializeField] private FloatInputField _component3x3 = null;

        public Matrix4x4 matrix4x4
        {
            get => new Matrix4x4(
                new Vector4(_component0x0.value, _component0x1.value, _component0x2.value, _component0x3.value),
                new Vector4(_component1x0.value, _component1x1.value, _component1x2.value, _component1x3.value),
                new Vector4(_component2x0.value, _component2x1.value, _component2x2.value, _component2x3.value),
                new Vector4(_component3x0.value, _component3x1.value, _component3x2.value, _component3x3.value));
            set
            {
                _component0x0.value = value.m00;
                _component0x1.value = value.m01;
                _component0x2.value = value.m02;
                _component0x3.value = value.m03;

                _component1x0.value = value.m10;
                _component1x1.value = value.m11;
                _component1x2.value = value.m12;
                _component1x3.value = value.m13;

                _component2x0.value = value.m20;
                _component2x1.value = value.m21;
                _component2x2.value = value.m22;
                _component2x3.value = value.m23;

                _component3x0.value = value.m30;
                _component3x1.value = value.m31;
                _component3x2.value = value.m32;
                _component3x3.value = value.m33;
            }
        }
    }
}