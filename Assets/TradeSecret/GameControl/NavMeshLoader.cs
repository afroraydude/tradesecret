using System;
using TradeSecret.Data;
using UnityEngine;

namespace TradeSecret.GameControl
{
    public class NavMeshLoader : MonoBehaviour
    {
        private LocalNavMeshBuilder _localNavMeshBuilder;
        public void Awake()
        {
            GameObject floor = GameObject.Find("Floor");
            _localNavMeshBuilder = gameObject.AddComponent<LocalNavMeshBuilder>();
            _localNavMeshBuilder.m_Size = new Vector3(floor.transform.localScale.x, 100, floor.transform.localScale.z);
            //Destroy(x);
        }
    }
}