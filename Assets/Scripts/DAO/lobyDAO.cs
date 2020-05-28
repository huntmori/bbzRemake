using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lobyDAO : MonoBehaviour
{

    public Button btnTestMode;
    public Button btnMatchStart;
    public Button btnNotAllocated;
    public Button btnConfig;

    // Start is called before the first frame update
    void Start()
    {
        btnTestMode.enabled = true;
        btnMatchStart.enabled = false;
        btnNotAllocated.enabled = false;
        btnConfig.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eventTestMode()
    {
        SceneManager.LoadScene("00002.practice");
    }
}
