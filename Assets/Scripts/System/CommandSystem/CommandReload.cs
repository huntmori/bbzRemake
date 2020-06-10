using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
public class CommandReload : CommandObject
{

    void CommandObject.Execute(CommandActor actor)
    {
        actor.Reload();
    }
}
