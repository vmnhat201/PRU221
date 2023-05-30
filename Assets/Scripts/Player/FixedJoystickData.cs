using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FixedJoystickData
{
    public SerializableVector2 joystickPosition;
    public SerializableVector2 joystickInput;

    public FixedJoystickData(FixedJoystick joystick)
    {
        if(joystick == null)
        {
            Debug.Log("Joystick is null");
            return;
        }
        joystickPosition = new SerializableVector2(joystick.transform.position);
        joystickInput = new SerializableVector2(joystick.Direction);
    }
}
