// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEditor;

namespace Microsoft.MixedReality.Toolkit.ObjectCollection.Editor
{
    [CustomEditor(typeof(BaseObjectCollection), true)]
    public class BaseCollectionInspector : UnityEditor.Editor
    {
        private SerializedProperty ignoreInactiveTransforms;
        private SerializedProperty sortType;

        protected virtual void OnEnable()
        {
            ignoreInactiveTransforms = serializedObject.FindProperty("ignoreInactiveTransforms");
            sortType = serializedObject.FindProperty("sortType");
        }

        sealed public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(ignoreInactiveTransforms);
            EditorGUILayout.PropertyField(sortType);
            OnInspectorGUIInsertion();
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnInspectorGUIInsertion() { }
    }
}