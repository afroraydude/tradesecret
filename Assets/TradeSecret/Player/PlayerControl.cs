using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TradeSecret.Player
{
    public class PlayerControl : MonoBehaviour
    {
        public Animator anim;
        public float defaultSpeed = 10.0f;
        public bool crouched = false;
        public bool isWalking = false;

        [SerializeField] private float walkRadius = 10.0f;

        public float rotationSpeed = 25.0f;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetBool("Crouched", crouched);

            if (crouched) walkRadius = 2.5f;
            else walkRadius = 5f;

            float speed = defaultSpeed;

            if (crouched) speed = defaultSpeed / 2;

            // Translation of stuff aka acceleration and speed
            float translationX = Input.GetAxis("Horizontal") * speed;
            float translationZ = Input.GetAxis("Vertical") * speed;
            
            // Load velocity into move animations
            anim.SetFloat("VelocityZ", translationZ);
            anim.SetFloat("VelocityX", translationX);
            
            translationX *= Time.deltaTime;
            translationZ *= Time.deltaTime;

            
            
            // Begin move animations
            if (translationX != 0.0 || translationZ != 0.0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
                
            }
            anim.SetBool("Walking", isWalking);

            if (Input.GetKey(KeyCode.C))
            {
                crouched = !crouched;
            }

            /*
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0).eulerAngles);
            }
            */
                // Move player
            transform.Translate(translationX, 0, translationZ);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                SceneManager.LoadScene("PreviewGameOver");
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (isWalking) Gizmos.DrawWireSphere (transform.position, walkRadius);
        }

        private void GenerateSound()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, walkRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                hitCollider.gameObject.SendMessage("OnSoundHeard", gameObject);
            }
        }

    }
}
