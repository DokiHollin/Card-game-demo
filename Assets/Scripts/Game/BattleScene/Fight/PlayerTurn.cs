using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : FightUnit
{
    public override void Init()
    {
        UIMgr.Instance.ShowTip("Player Turn", Color.green, delegate ()
        {

            //����
            Debug.Log("����");
        });
    }

    public override void OnUpdate()
    {
       
    }
}
