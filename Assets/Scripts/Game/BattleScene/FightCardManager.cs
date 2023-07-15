using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ս�����ƹ�����
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//���Ѽ���
    public List<string> usedCardList;//���ƶ�
    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //������ʱ����
        List<string> tempList = new List<string>();
        //����ҿ��Ʒŵ���ʱ����
        tempList.AddRange(RoleManager.Instance.cardList);

        while(tempList.Count > 0)
        {
            //����±�
            int temIndex = Random.Range(0, tempList.Count);

            //��ӵ�����
            cardList.Add(tempList[temIndex]);
            

            //remove from temp
            tempList.RemoveAt(temIndex);
        }
        Debug.Log(cardList.Count);
    }
}
