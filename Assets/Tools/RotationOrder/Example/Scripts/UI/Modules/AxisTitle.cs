using TMPro;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AxisTitle : MonoBehaviour
    {
        private const string NAME_PATTERN = "<color=#{0}>{1}</color>:";

        [SerializeField] private Settings.InputFieldType _type = Settings.InputFieldType.AxisX;

        [Header("Resources")]
        [SerializeField] private Settings _settings = null;

        private void Awake()
        {
            var textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            Settings.InputFieldData axisData = _settings.GetInputFieldDataFromType(_type);
            textMeshProUGUI.text = string.Format(NAME_PATTERN, ColorUtility.ToHtmlStringRGBA(axisData.color), axisData.name);
        }
    }
}