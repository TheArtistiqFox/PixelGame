using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorModePowerup : Powerup
{
    public MirrorModeSystem mirrorModeSystem;
    
    protected override void PowerupCollected()
    {
        mirrorModeSystem.ActivateMirrorMode();
    }
}
