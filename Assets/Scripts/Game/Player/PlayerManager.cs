using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��������û��Ŀ�����Ϣ��ҵȵ�
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public static int DEFAULT_ATTACK = 4;
    public List<string> cardList;//�洢ӵ�еĿ���

    public void Init()
    {
        cardList = new List<string>();
        //��ʼ���Ź�����,���ŷ���
        for(int i = 0; i < DEFAULT_ATTACK; i++)
        {
            cardList.Add("1000");
            cardList.Add("1001");
        }
        //����Ч����
        cardList.Add("1002");
        cardList.Add("1002");

    }

 
}
