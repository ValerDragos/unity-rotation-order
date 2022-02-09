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
            get => new Quaternion(_xFloatInputField.value, _yFloatInputField.value, _zFloatInputField.value, _wFloatInputField.value);
            set
            {
                _xFloatInputField.value = value.x;
                _yFloatInputField.value = value.y;
                _zFloatInputField.value = value.z;
                _wFloatInputField.value = value.w;
            }
        }
    }
}