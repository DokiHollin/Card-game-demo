using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//win
public class FightWin : FightUnit
{
    public override void Init()
    {
        //FightManager.Instance.StopAllCoroutines();
        Debug.Log("Win");
        //�������
        UIMgr.Instance.ShowUI<SelectCardUI>("SelectCardUI");
    }
}
