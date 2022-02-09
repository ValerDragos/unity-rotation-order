using TMPro;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AxisTitle : MonoBehaviour
    {
        private const string NAME_PATTERN = "<color=#{0}>{1}</color>:";

        public enum Type
        {
            X,
            Y,
            Z,
            W
        }

        [SerializeField] private Type _type = Type.X;

        [Header("Resources")]
        [SerializeField] private Settings _settings = null;

        private void Awake()
        {
            var textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            Settings.Axis axisData;

            switch (_type)
            {
                case Type.Y:
                    axisData = _settings.yAxis;
                    break;
                case Type.Z:
                    axisData = _settings.zAxis;
                    break;
                case Type.W:
                    axisData = _settings.wAxis;
                    break;
                default: // Type.X
                    axisData = _settings.xAxis;
                    break;
            }

            textMeshProUGUI.text = string.Format(NAME_PATTERN, ColorUtility.ToHtmlStringRGBA(axisData.color), axisData.name);
        }
    }
}