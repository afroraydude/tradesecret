using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using TradeSecret.Data;
using TradeSecret.Enemy;
using UnityEditor;
using UnityEngine;

namespace TradeSecret.GameControl
{
    public class TestGenerator : MonoBehaviour
    {
        List<ObjectPosition> _walls = new List<ObjectPosition>();
        List<Data.Enemy> _enemies = new List<Data.Enemy>();
        List<InteractableObject> _interactableObjects = new List<InteractableObject>();
        List<MissionTrigger> _missionTriggers = new List<MissionTrigger>();
        ObjectPosition playerPosition;

        public void CreateLevelFile(LevelInformation levelInformation)
        {
            GameObject singular = GameObject.CreatePrimitive(PrimitiveType.Cube);

            singular.transform.position = new Vector3(100000, 100000, 100000);

            singular.name = "SceneLooker";

            GameObject[] objects = singular.scene.GetRootGameObjects();

            foreach (GameObject o in objects)
            {
                if (o.name == "Level")
                {
                    GameObject levelData = o.transform.Find("LevelData").gameObject;
                    CreateLevelData(levelData);
                }

                if (o.CompareTag("Enemy"))
                {
                    CreateEnemyData(o);
                }

                if (o.CompareTag("InteractableObject"))
                {
                    CreateInteractableObjects(o);
                }
                
                if (o.CompareTag("MissionTrigger"))
                {
                    CreateMissionTriggers(o);
                }

                if (o.CompareTag("Player"))
                {
                    playerPosition = new ObjectPosition(o.transform.position);
                }
            }
            
            ObjectData objectData = new ObjectData(_walls.ToArray(), _enemies.ToArray(), _interactableObjects.ToArray(), _missionTriggers.ToArray(), playerPosition);
            
            var oj = JsonConvert.SerializeObject(objectData);
            SceneData sceneData = new SceneData(objectData);
            LevelFile levelFileStruct = new LevelFile(levelInformation, sceneData);
            var json = JsonConvert.SerializeObject(levelFileStruct);
            WriteLevelDataToFile(json);
            
            DestroyImmediate(singular);
        }

        private void CreateLevelData(GameObject levelData)
        {
            Transform[] wallTransforms = levelData.GetComponentsInChildren<Transform>();

            foreach (Transform w in wallTransforms)
            {
                
                if (w.gameObject.name != "LevelData")
                    _walls.Add(new ObjectPosition(w.position, w.rotation, w.localScale));
            }
        }

        private void CreateEnemyData(GameObject enemy)
        {
            Transform[] patrolPointsTransform = GameObject.Find($"{enemy.name} Patrol Points").GetComponentsInChildren<Transform>();
            List<ObjectPosition> patrolPoints = new List<ObjectPosition>();
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            ObjectPosition position = new ObjectPosition(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            int state = (int) enemyController.startState;
            string name = enemy.name;
            
            foreach (Transform t in patrolPointsTransform)
            {
                if (t.gameObject.name != $"{enemy.name} Patrol Points")
                {
                    //Debug.Log(new ObjectPosition(t.position.x, t.position.y, t.position.z).ToString());
                    patrolPoints.Add(new ObjectPosition(t.position.x, t.position.y, t.position.z, t.rotation));
                }
            }
            _enemies.Add(new Data.Enemy(name, position, state, patrolPoints.ToArray()));
        }

        private void CreateMissionTriggers(GameObject localGameObject)
        {
            int prefab = 0;
            int type = 0;
            
            if (localGameObject.name.ToLower().Contains("table"))
            {
                prefab = 1;
            }
            if (localGameObject.name.ToLower().Contains("desk"))
            {
                prefab = 2;
            }
            if (localGameObject.name.ToLower().Contains("refreshment"))
            {
                prefab = 3;
            }
            
            _missionTriggers.Add(new MissionTrigger(prefab, type, new ObjectPosition(localGameObject.transform.position, localGameObject.transform.rotation), null));
        }
        
        private void CreateInteractableObjects(GameObject localGameObject)
        {
            int prefab = 0;
            
            if (localGameObject.name.ToLower().Contains("table"))
            {
                prefab = 1;
            }
            if (localGameObject.name.ToLower().Contains("desk"))
            {
                prefab = 2;
            }
            if (localGameObject.name.ToLower().Contains("refreshment"))
            {
                prefab = 3;
            }
            
            _interactableObjects.Add(new InteractableObject(prefab, new ObjectPosition(localGameObject.transform.position, localGameObject.transform.rotation)));
        }

        private void WriteLevelDataToFile(string json)
        {
            string dataPath = Application.dataPath;
            string levelPath = dataPath + "/Scenes/Levels";
            Debug.Log(levelPath);
            string filename = GUID.Generate().ToString() + ".level";
            string filePath = Path.Combine(levelPath, filename);
            Debug.Log(filePath);
            File.WriteAllText(filePath, json);
        }
    }
}