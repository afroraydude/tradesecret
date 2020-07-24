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

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetBool("Crouched", crouched);

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
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            if (Input.GetKey(KeyCode.C))
            {
                crouched = !crouched;
            }
            
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
    }
}
