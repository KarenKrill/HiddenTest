using UnityEditor;
using UnityEngine;

namespace HiddenTest.Editor
{
    [CustomEditor(typeof(GameSettingsConfig))]
    public class GameConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, _qualityLevelPropertyName);
            var selectedIndex = _qualityLevelProperty.intValue;
            _qualityLevelProperty.intValue = EditorGUILayout.Popup(selectedIndex, _qualityLevelNames);
            serializedObject.ApplyModifiedProperties();
        }

        private static readonly string _qualityLevelPropertyName = "_qualityLevel";
        private SerializedProperty _qualityLevelProperty;
        private string[] _qualityLevelNames;

        private void OnEnable()
        {
            _qualityLevelProperty = serializedObject.FindProperty(_qualityLevelPropertyName);
            _qualityLevelNames = QualitySettings.names;
        }
    }

}
