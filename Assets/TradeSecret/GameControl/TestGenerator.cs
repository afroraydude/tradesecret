using System.Collections.Generic;
using System.Runtime.InteropServices;
using TradeSecret.Data;
using TradeSecret.Enemy;
using UnityEngine;

namespace TradeSecret.GameControl
{
    public class TestGenerator : MonoBehaviour
    {
        List<Wall> _walls = new List<Wall>();
        List<Data.Enemy> _enemies = new List<Data.Enemy>();
        List<InteractableObject> _interactableObjects = new List<InteractableObject>();
        List<MissionTrigger> _missionTriggers = new List<MissionTrigger>();
        
        public void CreateLevelFile()
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
            }
            
            DestroyImmediate(singular);
        }

        private void CreateLevelData(GameObject levelData)
        {
            Transform[] wallTransforms = levelData.GetComponentsInChildren<Transform>();

            foreach (Transform w in wallTransforms) 
            {
                if (w.gameObject.name != "LevelData")
                    _walls.Add(new Wall(new ObjectPosition(w.position.x, w.position.y, w.position.z)));
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
                    patrolPoints.Add(new ObjectPosition(t.position.x, t.position.y, t.position.z));
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
            
            _missionTriggers.Add(new MissionTrigger(prefab, type, new ObjectPosition(localGameObject.transform.position), null));
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
            
            _interactableObjects.Add(new InteractableObject(prefab, new ObjectPosition(localGameObject.transform.position)));
        }
    }
}