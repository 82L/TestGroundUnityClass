using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
 [CustomEditor(typeof(TerrainGeneratorUsingScriptableObjects))]
    public class TerrainGeneratorUsingScriptableObjects_Inspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();
            var script = (TerrainGeneratorUsingScriptableObjects)target;

            if(GUILayout.Button("Change", GUILayout.Height(40)))
            {
                script.BtnGenerateTerrain();
            }
        }
    }
