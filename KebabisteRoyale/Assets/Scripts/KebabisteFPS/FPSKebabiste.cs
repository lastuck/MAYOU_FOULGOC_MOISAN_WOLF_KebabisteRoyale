﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSKebabiste
{
    public enum Action
    {
        None,
        GoUp,
        GoLeft,
        GoRight,
        GoDown,
        StopUp,
        StopLeft,
        StopRight,
        StopDown,
        Shoot,
        Hide,
        Reload
    }

    public int life;
    public int ammo;
    public FPSKebabiste opponent;
    public GameObject kebabistePrefab;
    public Camera kebabisteCam;
    public Rigidbody KebabisteRigidbody;
    public FPSKebabisteExposer exposer;
    
    public bool wantToGoUp;
    public bool wantToGoDown;
    public bool wantToGoLeft;
    public bool wantToGoRight;


    public FPSKebabiste()
    {
        life = 100;
        ammo = 10;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetupPlayer()
    {
        exposer = kebabistePrefab.GetComponent<FPSKebabisteExposer>();
        kebabisteCam = exposer.camera;
        KebabisteRigidbody = exposer.rigidbody;
    }

    public virtual Action GetIntent()
    {
        return Action.None;
    }
}
