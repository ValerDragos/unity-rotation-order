using System;
using UnityEngine;

namespace Tools.RotationOrder.Example
{
    // This could be a Scriptable Object but since it is a part of a example, there is no need to clutter the unity Assets menu.
    public class Settings : MonoBehaviour
    {
        public enum InputFieldType
        {
            AxisX,
            AxisY,
            AxisZ,
            AxisW,
            Angle
        }

        [SerializeField] private InputFieldData _xAxis = new InputFieldData();
        [SerializeField] private InputFieldData _yAxis = new InputFieldData();
        [SerializeField] private InputFieldData _zAxis = new InputFieldData();
        [SerializeField] private InputFieldData _wAxis = new InputFieldData();
        [SerializeField] private InputFieldData _angle = new InputFieldData();

        public InputFieldData GetInputFieldDataFromType (InputFieldType inputFieldType)
        {
            switch (inputFieldType)
            {
                case InputFieldType.AxisX:
                    return _xAxis;
                case InputFieldType.AxisY:
                    return _yAxis;
                case InputFieldType.AxisZ:
                    return _zAxis;
                case InputFieldType.AxisW:
                    return _wAxis;
                default: // Angle
                    return _angle;
            }
        }

        [Serializable]
        public struct InputFieldData
        {
            public string name;
            public Color color;
        }
    }
}