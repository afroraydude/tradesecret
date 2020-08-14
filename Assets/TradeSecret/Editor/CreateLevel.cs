using TradeSecret.Data;
using TradeSecret.GameControl;
using UnityEditor;
using UnityEngine;

namespace TradeSecret.Editor
{
    public class CreateLevel : EditorWindow
    {
        private string name;
        private string description;
        private string sizeX;
        private string sizeY;

            [MenuItem("Window/Jetpack Game Studios/Level Creator")]
        private static void ShowWindow()
        {
            var window = GetWindow<CreateLevel>();
            window.titleContent = new GUIContent("Level Creator");
            
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label ("Level Data", EditorStyles.boldLabel);
            name = EditorGUILayout.TextField("Level Name", name);
            
            sizeX = EditorGUILayout.TextField("Size X", sizeX);
            sizeY = EditorGUILayout.TextField("Size Y", sizeY);
            
            EditorGUILayout.PrefixLabel("Level Description");
            description = EditorGUILayout.TextArea(description, GUILayout.Height(position.height - 200));
            
            if (GUI.Button(new Rect(5, position.height - 35, 250, 30), "Generate Level"))
            {
                GameObject singular = GameObject.CreatePrimitive(PrimitiveType.Cube);

                singular.transform.position = new Vector3(100000, 100000, 100000);

                singular.name = "TestGenerator";

                TestGenerator testGenerator = singular.AddComponent<TestGenerator>();
                
                LevelInformation information = new LevelInformation(name, description, sizeX, sizeY);
                
                testGenerator.CreateLevelFile(information);
                
                DestroyImmediate(singular);
            }
        }
    }
}