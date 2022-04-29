using HEXWorld.ProceduralLandmass;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator _mapGenerator = (MapGenerator) target;
        if (DrawDefaultInspector())
        {
            if (_mapGenerator.isAutoUpdate)
            {
                _mapGenerator.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            _mapGenerator.GenerateMap();
        }
    }
     
}
