using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // References
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start() {
        playButton = GameObject.Find("play").GetComponent<Button>();
        quitButton = GameObject.Find("quit").GetComponent<Button>();
		optionsButton = GameObject.Find("options").GetComponent<Button>();

		playButton.onClick.AddListener(Play);
        optionsButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void Play() {
        SceneManager.LoadScene("ObjectTest");
    }

    void Quit() {
        Application.Quit();
    }
}
