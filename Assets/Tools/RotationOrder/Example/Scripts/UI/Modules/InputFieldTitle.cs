using TMPro;
using UnityEngine;

namespace Tools.RotationOrder.Example.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InputFieldTitle : MonoBehaviour
    {
        private const string NAME_PATTERN = "<color=#{0}>{1}</color>:";

        [SerializeField] private Settings _settings = null;
        [SerializeField] private Settings.InputFieldType _type = Settings.InputFieldType.AxisX;

        private void Awake()
        {
            var textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            Settings.InputFieldData axisData = _settings.GetInputFieldDataFromType(_type);
            textMeshProUGUI.text = string.Format(NAME_PATTERN, ColorUtility.ToHtmlStringRGBA(axisData.color), axisData.name);
        }
    }
}