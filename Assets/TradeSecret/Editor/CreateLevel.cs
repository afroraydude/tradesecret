using TradeSecret.GameControl;
using UnityEditor;
using UnityEngine;

namespace TradeSecret.Editor
{
    public class CreateLevel : EditorWindow
    {
        
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
            GUI.TextField(new Rect(5, 20, 250, 20), "Level Name");
            GUI.TextArea(new Rect(5, 50, 250, 100), "Level Description");
            if (GUI.Button(new Rect(10, 200, 250, 30), "Generate Level"))
            {
                GameObject singular = GameObject.CreatePrimitive(PrimitiveType.Cube);

                singular.transform.position = new Vector3(100000, 100000, 100000);

                singular.name = "TestGenerator";

                TestGenerator testGenerator = singular.AddComponent<TestGenerator>();
                
                testGenerator.CreateLevelFile();
                
                DestroyImmediate(singular);
            }
        }
    }
}