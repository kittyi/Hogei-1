using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DungeonGenerator Generator = (DungeonGenerator)target;


        if (GUILayout.Button("Generate Dungeon"))
        {
            Generator.GenerateRooms();
        }

        if (GUILayout.Button("Check Room Collisions"))
        {

            Generator.CheckRoomCollisions();

        }

        if (GUILayout.Button("Generate Corridors"))
        {

            Generator.GenerateCorridors();

        }

        if (GUILayout.Button("Insert Prefab Rooms"))
        {

            Generator.ReplaceRooms();

        }

    }

}
