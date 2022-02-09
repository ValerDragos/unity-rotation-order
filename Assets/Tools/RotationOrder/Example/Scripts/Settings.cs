using System;
using UnityEngine;

namespace Tools.RotationOrder.Example
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Axis _xAxis = new Axis();
        [SerializeField] private Axis _yAxis = new Axis();
        [SerializeField] private Axis _zAxis = new Axis();
        [SerializeField] private Axis _wAxis = new Axis();

        public Axis xAxis => _xAxis;
        public Axis yAxis => _yAxis;
        public Axis zAxis => _zAxis;
        public Axis wAxis => _wAxis;

        [Serializable]
        public struct Axis
        {
            public char name;
            public Color color;
        }
    }
}