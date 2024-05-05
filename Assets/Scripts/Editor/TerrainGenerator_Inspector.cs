using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGenerator_Inspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (TerrainGenerator)target;

        if(GUILayout.Button("Change", GUILayout.Height(40)))
        {
            script.BtnGenerateTerrain();
        }
        
    }

}
