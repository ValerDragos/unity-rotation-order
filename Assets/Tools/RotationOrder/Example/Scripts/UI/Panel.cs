using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.RotationOrder.Example.UI
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private EulerGroup _eulerGroup = null;
        [SerializeField] private QuaternionGroup _quaternionGroup = null;
        [SerializeField] private Matrix4x4Group _matrix4x4Group = null;

        [SerializeField] private ModelRotationVisualizer _modelRotationVisualizer = null;
        [SerializeField] private Button _showRotationOrderButton = null;

        public event Action<RotationData> OnValuesChanged;

        private RotationData _rotationData = new RotationData(new Euler(Euler.RotationOrder.XYZ));
        public RotationData rotationData 
        {
            get => _rotationData;
            set
            {
                _rotationData = value;

                _eulerGroup.SetEulerWithoutNotify(value.euler);
                _quaternionGroup.quaternion = value.quaternion;
                _matrix4x4Group.matrix4x4 = value.matrix4x4;
                _modelRotationVisualizer.Set(value);
            }
        }

        private void Awake()
        {
            _quaternionGroup.quaternion = _rotationData.quaternion;
            _matrix4x4Group.matrix4x4 = _rotationData.matrix4x4;
            _modelRotationVisualizer.Set(_rotationData);

            // Init components from the rotation data
            _eulerGroup.OnValuesChanged += (euler) =>
            {
                _rotationData = new RotationData(euler);

                _quaternionGroup.quaternion = _rotationData.quaternion;
                _matrix4x4Group.matrix4x4 = _rotationData.matrix4x4;
                _modelRotationVisualizer.Set(_rotationData);
                OnValuesChanged?.Invoke(_rotationData);
            };

            _showRotationOrderButton.onClick.AddListener(()=>
            {
                _modelRotationVisualizer.AnimateShowOrder();
            });
        }
    }
}