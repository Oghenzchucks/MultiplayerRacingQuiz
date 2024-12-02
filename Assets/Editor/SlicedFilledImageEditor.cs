using UI;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor (typeof(SlicedFilledImage)), CanEditMultipleObjects]
    public class SlicedFilledImageEditor : UnityEditor.Editor
    {
        private SerializedProperty spriteProp, colorProp;
        private GUIContent spriteLabel;

        private void OnEnable()
        {
            spriteProp = serializedObject.FindProperty ("m_Sprite");
            colorProp = serializedObject.FindProperty ("m_Color");
            spriteLabel = new GUIContent ("Source Image");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField (spriteProp, spriteLabel);
            EditorGUILayout.PropertyField (colorProp);
            DrawPropertiesExcluding (serializedObject, "m_Script", "m_Sprite", "m_Color", "m_OnCullStateChanged");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
