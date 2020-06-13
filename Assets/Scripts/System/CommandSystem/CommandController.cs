using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public enum eButton {   BUTTON_FIRE, BUTTON_JUMP, BUTTON_RELOAD, BUTTON_EVADE, BUTTON_NONE}

    eButton pressed_button = eButton.BUTTON_NONE;

    CommandObject fire, jump, reload, evade, skill1, skill2, skill3, skill4;
    CommandActor actor;
    // Start is called before the first frame update
    void Start()
    {
        actor = new CommandActor();
        SetCommand();
    }

    void SetCommand()
    {
        fire = new CommandFire();
        jump = new CommandJump();
        reload = new CommandReload();
        evade = new CommandEvade();
    }

    // Update is called once per frame
    void Update()
    {
        CommandObject command = GetCommand();
        if (command != null)
        {
            command.Execute(actor);
        }
    }

    CommandObject GetCommand()
    {
        if (IsPressed(eButton.BUTTON_EVADE))
        {
            return evade;
        }
        else if (IsPressed(eButton.BUTTON_FIRE))
        {
            return fire;
        }
        else if (IsPressed(eButton.BUTTON_JUMP))
        {
            return jump;
        }
        else if (IsPressed(eButton.BUTTON_RELOAD))
        {
            return reload;
        }
        return null;
    }

    bool IsPressed(eButton btn)
    {
        pressed_button = eButton.BUTTON_NONE;

        if (Input.GetKey("fire"))
            pressed_button = eButton.BUTTON_FIRE;
        else if (Input.GetKey("jump"))
            pressed_button = eButton.BUTTON_JUMP;
        else if (Input.GetKey("reload"))
            pressed_button = eButton.BUTTON_RELOAD;
        else if (Input.GetKey("evade"))
            pressed_button = eButton.BUTTON_EVADE;

        return (btn == pressed_button);
    }
}
