using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public enum eButton {   BUTTON_FIRE, BUTTON_JUMP, BUTTON_RELOAD, BUTTON_EVADE, BUTTON_NONE}

    eButton pressed_button = eButton.BUTTON_NONE;

    CommandObject fire, jump, reload, evade;
    CommandActor actor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
