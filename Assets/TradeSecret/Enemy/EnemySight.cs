using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TradeSecret.Enemy
{
    public class EnemySight : MonoBehaviour
    {
        public Vector3 raycastPosition;
        public Quaternion raycastRotation;
        // Start is called before the first frame update
        void Start()
        {
            raycastPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            raycastRotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            

            raycastPosition = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            raycastRotation = transform.rotation;


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
            if (Physics.SphereCast(raycastPosition, 0.25f, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
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
        }
    }
}