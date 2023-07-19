using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : FightUnit
{
    public override void Init()
    {
        //ɾ�����п���
        UIMgr.Instance.GetUI<FightUI>("FightUI").RemoveAllCard();
        //��ʾ���˻غ���ʾ
        UIMgr.Instance.ShowTip("Enemy Turn", Color.red, delegate ()
        {

            
            Debug.Log("ִ�е���AI");
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
