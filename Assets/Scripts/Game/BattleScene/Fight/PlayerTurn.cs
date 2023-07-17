using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : FightUnit
{
    public override void Init()
    {
        UIMgr.Instance.ShowTip("Player Turn", Color.green, delegate ()
        {
            //�������û�п������³�ʼ��
            if(FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.Init();
                //��������������
                UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardHeapCount();
            }

            //����
            Debug.Log("����");
            UIMgr.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4); //����
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            //���µ���
            if (FightManager.Instance.playerNewTurn)
            {
                FightManager.Instance.CurPointCount += 5;
                UIMgr.Instance.GetUI<FightUI>("FightUI").UpdatePoint();
                FightManager.Instance.playerNewTurn = false;
            }

            //���¿�������
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }

    public override void OnUpdate()
    {
       
    }
}
