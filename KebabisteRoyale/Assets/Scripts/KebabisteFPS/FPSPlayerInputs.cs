using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerInputs : MonoBehaviour
{
    public FPSKebabiste.Action intent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            intent = FPSKebabiste.Action.GoUp;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            intent = FPSKebabiste.Action.GoLeft;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            intent = FPSKebabiste.Action.GoDown;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            intent = FPSKebabiste.Action.GoRight;
        }
        if (Input.GetMouseButtonDown(0))
        {
            intent = FPSKebabiste.Action.Shoot;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            intent = FPSKebabiste.Action.StopUp;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            intent = FPSKebabiste.Action.StopLeft;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            intent = FPSKebabiste.Action.StopDown;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            intent = FPSKebabiste.Action.StopRight;
        }
        if (Input.GetMouseButtonDown(0))
        {
            intent = FPSKebabiste.Action.Shoot;
        }
        
    }
}
