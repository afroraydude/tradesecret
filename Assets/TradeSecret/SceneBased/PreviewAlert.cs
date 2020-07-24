using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreviewAlert : MonoBehaviour
{
    [SerializeField] private Text play;
    public float timer;

    public float blinkLength = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKeyDown) {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) {
                return;
            }
            else {
                SceneManager.LoadScene("Menu");
            }
        }
        
        timer = timer + Time.deltaTime;
        if(timer >= (blinkLength / 2))
        {
            play.enabled = true;
        }
        if(timer >= blinkLength)
        {
            play.enabled = false;
            timer = 0;
        }
        
    }

    public static void GoToLink()
    {
        Application.OpenURL("https://blogs.unity3d.com/2018/11/06/performance-reporting-is-now-cloud-diagnostics/");
    }
}
