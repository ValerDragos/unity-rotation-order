using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    public class AngleAxisGroup : MonoBehaviour
    {
        [SerializeField] private FloatInputField _xFloatInputField = null;
        [SerializeField] private FloatInputField _yFloatInputField = null;
        [SerializeField] private FloatInputField _zFloatInputField = null;
        [SerializeField] private FloatInputField _angleFloatInputField = null;

        public Quaternion quaternion
        {
            set
            {
                value.ToAngleAxis(out float angle, out Vector3 axis);

                _xFloatInputField.value = axis.x;
                _yFloatInputField.value = axis.y;
                _zFloatInputField.value = axis.z;
                _angleFloatInputField.value = angle;
            }
        }
    }
}