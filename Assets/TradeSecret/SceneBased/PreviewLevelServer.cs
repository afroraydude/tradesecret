﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TradeSecret.SceneBased
{
    public class PreviewLevelServer : MonoBehaviour
    {
        private bool _playerIsHere = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.E))
                if (_playerIsHere)
                {
                    SceneManager.LoadScene("PreviewGameOver");
                }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsHere = true;
                Debug.Log("Player enter");
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsHere = false;
                Debug.Log("Player exit");
            }
        }
    }
}