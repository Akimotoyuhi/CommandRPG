using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Charactor
{
    public override void Setup()
    {
        base.Setup();
        SetParametor(GameManager.Instance.PlayerData.Data[0]);
        SetSlider();
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
