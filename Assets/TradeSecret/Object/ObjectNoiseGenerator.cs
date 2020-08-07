using System;
using UnityEngine;

namespace TradeSecret.Object
{
    public class ObjectNoiseGenerator : MonoBehaviour
    {
        [SerializeField] private BoxCollider useCollider;

        [SerializeField] private float soundDistance; // Must set for each different object type seperately

        private void Awake()
        {
            useCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKey(KeyCode.E) && other.CompareTag("Player"))
            {
                GenerateSound(other.gameObject.transform.position);
            }
        }
        
        private void GenerateSound(Vector3 playerPosition)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, soundDistance);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    hitCollider.gameObject.SendMessage("OnSoundHeard", playerPosition);
                }
            }
        }
        
        /**
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere (transform.position, soundDistance);
        }
        */
    }
}