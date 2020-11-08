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
            /* UI Objects */
            GUILayout.Label ("Level Data", EditorStyles.boldLabel);
            name = EditorGUILayout.TextField("Level Name", name);
            sizeX = EditorGUILayout.TextField("Size X", sizeX);
            sizeY = EditorGUILayout.TextField("Size Y", sizeY);
            
            EditorGUILayout.PrefixLabel("Level Description");
            description = EditorGUILayout.TextArea(description, GUILayout.Height(position.height - 200));
            
            /* On Generate Level button pushed */
            if (GUI.Button(new Rect(5, position.height - 35, 250, 30), 
                "Generate Level"))
            {
                // Create a new object in the scene, this object will be able to link the editor script to the scene.
                GameObject singular = GameObject.CreatePrimitive(PrimitiveType.Cube);
                singular.transform.position = new Vector3(100000, 100000, 100000);
                singular.name = "TestGenerator";

                // Attach the generation script to the object
                TestGenerator testGenerator = singular.AddComponent<TestGenerator>();
                
                // Create a level information struct for use in the generator
                LevelInformation information = new LevelInformation(name, description, sizeX, sizeY);
                
                // Create a level JSON file
                testGenerator.CreateLevelFile(information);
                
                // cleanup 
                DestroyImmediate(singular);
            }
        }
    }
}