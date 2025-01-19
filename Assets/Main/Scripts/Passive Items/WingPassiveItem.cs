using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMovespeed *= 1 + passiveItemData.Multipler /100f;
    }
}
