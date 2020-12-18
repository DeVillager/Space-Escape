using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
[Serializable]
public enum DoorColor
{
    Green = 0,
    Red = 1,
    Blue = 2,
}

namespace InteractableTypes
{
    public enum State
    {
        inactive = 0,
        active = 1,
        selected = 2,
        activated = 3,
        grabbed = 4,
    }
}


// public enum MoveState
// {
//     Idle,
//     Run,
//     Jump,
//     Stunned,
//     Dash,
//     Crouch,
//     Fall,
// }
//
// public enum ActionState
// {
//     Idle,
//     Attack,
// }