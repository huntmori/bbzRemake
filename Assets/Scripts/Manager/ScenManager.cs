using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeLoginScene()
    {
        SceneManager.LoadScene("00000.index");

    }

    public void changeLobyScene()
    {
        SceneManager.LoadScene("00001.Loby");

    }
}
