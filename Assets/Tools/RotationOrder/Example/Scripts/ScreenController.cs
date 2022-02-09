using UnityEngine;
using Tools.RotationOrder.Example.UI;

namespace Tools.RotationOrder.Example
{
    public class ScreenController : MonoBehaviour
    {
        [SerializeField] private Panel _leftPanel = null;
        [SerializeField] private Panel _rightPanel = null;

        private void Awake()
        {
            _leftPanel.OnValuesChanged += (rotationData) => UpdateGroup(rotationData, _rightPanel);
            _rightPanel.OnValuesChanged += (rotationData) => UpdateGroup(rotationData, _leftPanel);
        }

        private void UpdateGroup(RotationData rotationData, Panel to)
        {
            var toRotationData = to.rotationData;
            to.rotationData = ChangeRotationOrder(rotationData, toRotationData.euler.rotationOrder);
        }

        private RotationData ChangeRotationOrder(RotationData rotationData, Euler.RotationOrder rotationOrder)
        {
            if (rotationData.euler.rotationOrder == rotationOrder) return rotationData;
            return new RotationData(Euler.FromRotationMatrix(rotationData.matrix4x4, rotationOrder));
        }
    }
}
