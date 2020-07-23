using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TradeSecret.Enemy
{
    public class EnemySight : MonoBehaviour
    {
        public Vector3 raycastPosition;
        public Quaternion raycastRotation;
        public Transform enemyHead;
        public Animator enemyAnimator;
        public RaycastHit globalRaycast;

        private void Awake()
        {
            enemyAnimator = GetComponentInParent<Animator>();
            enemyHead = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:Neck").Find("mixamorig:Head");
            //enemyHead = enemyAnimator.GetBoneTransform()
        }

        // Start is called before the first frame update
        void Start()
        {
            
            //raycastPosition = enemyHead.transform.position;
            //raycastRotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            

            raycastPosition = enemyHead.transform.position;
            raycastRotation = enemyHead.transform.rotation;

            /*
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(raycastPosition, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(raycastPosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Did Hit");
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("is player");
                    gameObject.SendMessage("OnRaycastHit", true);
                } else
                {
                    Debug.Log("not player");
                    gameObject.SendMessage("OnRaycastHit", false);
                }
            }
            else
            {
                Debug.DrawRay(raycastPosition, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
            */
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.SphereCast(raycastPosition, 0.25f, enemyHead.TransformDirection(Vector3.forward), out hit, 100))
            {
                globalRaycast = hit;
                Debug.DrawRay(raycastPosition, enemyHead.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("is player");
                    gameObject.SendMessage("OnRaycastHit", true);
                } else
                {
                    Debug.Log("not player");
                    gameObject.SendMessage("OnRaycastHit", false);
                }
            }
            else
            {
                Debug.DrawRay(raycastPosition, transform.TransformDirection(Vector3.forward) * 100, Color.red);
                Debug.Log("Did not Hit");
                gameObject.SendMessage("OnRaycastHit", false);
            }
        }
    }
}