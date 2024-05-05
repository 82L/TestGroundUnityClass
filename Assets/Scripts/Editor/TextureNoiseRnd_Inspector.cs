using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextureNoiseRnd))]
public class TextureNoiseRnd_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (TextureNoiseRnd)target;

        if(GUILayout.Button("Change", GUILayout.Height(40)))
        {
            script.TextureGenerator();
        }
        
    }

}
