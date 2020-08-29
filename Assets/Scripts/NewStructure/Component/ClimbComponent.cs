using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ClimbComponent : Component
{
    public override void PreformComponent()
    {
        ObjectToPreform.ExcuteClimb();
    }
}
