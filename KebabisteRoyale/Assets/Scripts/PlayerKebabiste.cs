using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKebabiste : Kebabiste
{ 
    private PlayerInputs playerInputs;

    public override KebabisteIntent GetIntent()
    {
        return playerInputs.intent;
    }
}
