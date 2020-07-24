using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVersion : MonoBehaviour
{
    private Text versionText;
    // Start is called before the first frame update
    void Start()
    {
        versionText = GetComponentInParent<Text>();
        versionText.text = $"Pre-Alpha v{Application.version}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
