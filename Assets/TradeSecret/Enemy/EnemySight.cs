﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TradeSecret.Enemy
{
    /// <summary>
    /// Handles enemy "sight" and "hearing"
    /// basically enemy passive interaction with other objects
    /// </summary>
    public class EnemySight : MonoBehaviour
    {
        public Vector3 raycastPosition;
        public Quaternion raycastRotation;
        public Transform enemyHead;
        public Animator enemyAnimator;
        public RaycastHit globalRaycast;

        private void Awake()
        {
            
            //enemyHead = enemyAnimator.GetBoneTransform()
        }

        // Start is called before the first frame update
        void Start()
        {
            enemyAnimator = GetComponentInParent<Animator>();
            enemyHead = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1")
                .Find("mixamorig:Spine2").Find("mixamorig:Neck").Find("mixamorig:Head");
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

            RaycastHit hit;
            
            // forward raycast
            if (Physics.SphereCast(raycastPosition, 0.25f, enemyHead.TransformDirection(Vector3.forward), out hit, 100))
            {
                globalRaycast = hit;
                Debug.DrawRay(raycastPosition, enemyHead.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.tag == "Player")
                {

                    gameObject.SendMessage("OnRaycastHit", true);
                } else
                {

                    gameObject.SendMessage("OnRaycastHit", false);
                }
            }
            else
            {
                Debug.DrawRay(raycastPosition, transform.TransformDirection(Vector3.forward) * 100, Color.red);
                //gameObject.SendMessage("OnRaycastHit", false);
            }
            
            //right raycast
            if (Physics.SphereCast(raycastPosition, 0.25f, enemyHead.TransformDirection(new Vector3(1, 0, 1)), out hit, 100))
            {
                globalRaycast = hit;
                Debug.DrawRay(raycastPosition, enemyHead.TransformDirection(new Vector3(1, 0, 1)) * hit.distance, Color.yellow);
                if (hit.collider.tag == "Player")
                {

                    gameObject.SendMessage("OnRaycastHit", true);
                } else
                {

                    gameObject.SendMessage("OnRaycastHit", false);
                }
            }
            else
            {
                Debug.DrawRay(raycastPosition, transform.TransformDirection(new Vector3(1, 0, 1)) * 100, Color.red);
                //gameObject.SendMessage("OnRaycastHit", false);
            }
            
            //left raycast
            if (Physics.SphereCast(raycastPosition, 0.25f, enemyHead.TransformDirection(new Vector3(-1, 0, 1)), out hit, 100))
            {
                globalRaycast = hit;
                Debug.DrawRay(raycastPosition, enemyHead.TransformDirection(new Vector3(-1, 0, 1)) * hit.distance, Color.yellow);
                if (hit.collider.tag == "Player")
                {

                    gameObject.SendMessage("OnRaycastHit", true);
                } else
                {

                    gameObject.SendMessage("OnRaycastHit", false);
                }
            }
            else
            {
                Debug.DrawRay(raycastPosition, transform.TransformDirection(new Vector3(-1, 0, 1)) * 100, Color.red);
                //gameObject.SendMessage("OnRaycastHit", false);
            }
        }
        
        

        public void OnSoundHeard(GameObject soundCreator)
        {
            
        } 
    }
}