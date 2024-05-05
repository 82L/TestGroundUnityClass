using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureNoise))]
public class TextureNoise_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (TextureNoise)target;

        if(GUILayout.Button("Change", GUILayout.Height(40)))
        {
            script.TextureGenerator();
        }
        
    }

}
