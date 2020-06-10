using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
public class CommandEvade : CommandObject
{
    void CommandObject.Execute(CommandActor actor)
    {
        actor.Evade();
    }
}
