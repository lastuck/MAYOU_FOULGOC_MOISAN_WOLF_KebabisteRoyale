using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerKebabiste : FPSKebabiste
{
    public FPSPlayerInputs playerInputs;
    
    public override Action GetIntent()
    {
        Action toReturn = playerInputs.intent;
        playerInputs.intent = Action.None;
        return toReturn;
    }

    public override void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        float rotationSpeed = 2.0f;
            
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;

        kebabistePrefab.transform.Rotate(0, horizontal, 0);    
        kebabisteCam.transform.Rotate(-vertical, 0, 0, Space.Self);

        Quaternion q = kebabisteCam.transform.localRotation;

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, -90, 90);
        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        kebabisteCam.transform.localRotation = q;
    }
}
