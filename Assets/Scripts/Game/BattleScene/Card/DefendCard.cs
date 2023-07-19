using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//defend card add shield
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if(TryUse() == true)
        {
            //ʹ��Ч��
            int val = int.Parse(data["Arg0"]);

            //ʹ�ò��ź������(ÿ�ſ�ʹ�õ��������ܲ�һ��)
            BattleAudio.Instance.changeEffect("Effect/healspell");

            //���ӷ�����
            FightManager.Instance.DefenseCount += val;
            //ˢ���ı�
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

            Vector3 pos = Camera.main.transform.position;
            pos.y = -70;
            pos.x += 1300 ;
            playEffect(pos);

        }
        else
        {
            base.OnEndDrag(eventData);
        }

       
    }
}
