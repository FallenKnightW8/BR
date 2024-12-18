using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GhostSprites))]
public class GhostSpritesCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GhostSprites script = (GhostSprites)target;

        // Убираем вызов RestoreDefaults, если он не используется
        if (GUILayout.Button("Restore Defaults"))
        {
            Debug.LogWarning("The RestoreDefaults method is not implemented in the current version.");
        }
    }
}
