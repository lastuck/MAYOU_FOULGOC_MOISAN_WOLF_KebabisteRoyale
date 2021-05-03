﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KebabisteIntent
{
    public Kebabiste.Action action;
    public Ingredient ingredient;
}

public class PlayerKebabiste : Kebabiste
{ 
    public PlayerInputs playerInputs;
    public bool isAI;

    public override KebabisteIntent GetIntent()
    {
        KebabisteIntent toReturn = playerInputs.intent;
        playerInputs.intent = null;
        return toReturn;
    }
}
