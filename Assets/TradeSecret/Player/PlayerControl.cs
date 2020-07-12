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

            // bad if chain
            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetBool("Walking", true);

                anim.SetBool("Forward", true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Forward", false);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Left", true);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Left", false);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Back", true);

                speed /= 2;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Back", false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Right", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Right", false);
            }

            // Translation of stuff aka acceleration and speed
            float translationX = Input.GetAxis("Horizontal") * speed;
            float translationZ = Input.GetAxis("Vertical") * speed;
            translationX *= Time.deltaTime;
            translationZ *= Time.deltaTime;

            // Move player
            transform.Translate(translationX, 0, translationZ);
        }
    }
}
