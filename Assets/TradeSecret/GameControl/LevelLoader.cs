using System;
using System.IO;
using Newtonsoft.Json;
using TradeSecret.Data;
using TradeSecret.Enemy;
using UnityEditor;
using UnityEngine;

namespace TradeSecret.GameControl
{
    public class LevelLoader : MonoBehaviour
    {
        private LevelFile _level;
        [SerializeField] private GameObject floor;
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject enemy;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject[] InteractableObjects;
        [SerializeField] private GameObject[] MissionTriggers;

        private void OnEnable()
        {
            
        }

        private void Awake()
        {
            var levelFile = LoadLevelDataFromFile("Test_Level");
            _level = JsonConvert.DeserializeObject<LevelFile>(levelFile);
            Debug.Log(_level.LevelInformation.title);
            GameObject Level = new GameObject("Level");
            GameObject levelFloor = floor;
            levelFloor.transform.localScale = new Vector3(_level.LevelInformation.sizeX, 1, _level.LevelInformation.sizeY);
            Instantiate(levelFloor);
            var playerT = _level.sceneData.objectData.playerPosition;
            var playerR = (new Quaternion(playerT.rotation.x, playerT.rotation.y, playerT.rotation.z, playerT.rotation.w));
            var p = Instantiate(player, playerT.position, playerR);
            p.transform.Rotate(0, 90, 0);
            CreateWalls(_level.sceneData.objectData.walls, Level);
            CreateInteractableObjects(_level.sceneData.objectData.interactableObjects);
            
            LocalNavMeshBuilder localNavMeshBuilder = gameObject.AddComponent<LocalNavMeshBuilder>();
            localNavMeshBuilder.UpdateNavMesh();
            CreateEnemies(_level.sceneData.objectData.enemies);
        }
        
        private string LoadLevelDataFromFile(string levelName)
        {
            string dataPath = Application.dataPath;
            string levelPath = dataPath + "/Scenes/Levels";
            Debug.Log(levelPath);
            string filename = levelName + ".level";
            string filePath = Path.Combine(levelPath, filename);
            Debug.Log(filePath);
            return File.ReadAllText(filePath);
        }

        private void CreateNavMesh()
        {
            
        }

        private void CreateEnemies(TradeSecret.Data.Enemy[] enemies)
        {
            int enemyId = 1;
            foreach (var e in enemies)
            {
                string name = "enemy" + enemyId;
                CreatePatrolPoints(name, e.patrolPoints);
                var rot = new Quaternion(e.transform.rotation.x, e.transform.rotation.y, e.transform.rotation.z,
                    e.transform.rotation.w);
                GameObject o = Instantiate(enemy, e.transform.position, rot);
                o.name = name;
                EnemyController c = o.GetComponent<EnemyController>();
                c.startState = (EnemyController.States) ((int) e.startState);
                
            }
        }

        private void CreatePatrolPoints(string enemyName, ObjectPosition[] patrolPoints)
        {
            GameObject PatrolPoints = new GameObject(enemyName + " Patrol Points");
            foreach (var patrolPoint in patrolPoints)
            {
                GameObject o = new GameObject(GUID.Generate().ToString());
                o.transform.position = patrolPoint.position;
                o.transform.parent = PatrolPoints.transform;
            }
        }

        private void CreateWalls(ObjectPosition[] walls, GameObject level)
        {
            foreach (var w in walls)
            {
                GameObject o = Instantiate(wall, w.position,
                    new Quaternion(w.rotation.x, w.rotation.y, w.rotation.z, w.rotation.w), level.transform);
                o.transform.localScale = w.scale;
            }
        }
        
        private void CreateInteractableObjects(InteractableObject[] interactableObjects)
        {
            foreach (var iO in interactableObjects)
            {
                GameObject o = Instantiate(InteractableObjects[(int)iO.objectType], iO.transfrom.position,
                    new Quaternion(iO.transfrom.rotation.x, iO.transfrom.rotation.y, iO.transfrom.rotation.z, iO.transfrom.rotation.w));
                o.transform.localScale = iO.transfrom.scale;
            }
        }
        
        private void CreateMissionTriggers(MissionTrigger[] missionTriggers)
        {
            foreach (var mT in missionTriggers)
            {
                GameObject o = Instantiate(MissionTriggers[(int)mT.prefab], mT.transform.position,
                    new Quaternion(mT.transform.rotation.x, mT.transform.rotation.y, mT.transform.rotation.z, mT.transform.rotation.w));
                o.transform.localScale = mT.transform.scale;
                
            }
        }
    }
}