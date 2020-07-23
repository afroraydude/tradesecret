using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeSecret.Player
{
    public class PlayerControl : MonoBehaviour
    {
        public Animator anim;
        public float defaultSpeed = 10.0f;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

            float speed = defaultSpeed;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            // Translation of stuff aka acceleration and speed
            float translationX = Input.GetAxis("Horizontal") * speed;
            anim.SetFloat("VelocityX", translationX);
            float translationZ = Input.GetAxis("Vertical") * speed;
            anim.SetFloat("VelocityZ", translationZ);
            translationX *= Time.deltaTime;
            translationZ *= Time.deltaTime;

            // Move player
            transform.Translate(translationX, 0, translationZ);
        }
    }
}
