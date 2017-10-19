using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomGenerator Generator = (RoomGenerator)target;

        if (GUILayout.Button("Generate Room"))
        {
            Generator.GenerateRoom();
        }
    }

}
