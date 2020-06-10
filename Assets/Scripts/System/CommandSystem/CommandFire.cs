using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;


public class CommandFire : CommandObject
{
    
    void CommandObject.Execute(CommandActor actor)
    {
        actor.Attack();
    }
}
