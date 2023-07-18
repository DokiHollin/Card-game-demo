using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��������û��Ŀ�����Ϣ��ҵȵ�
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public static int DEFAULT_ATTACK = 4;
    public List<string> cardList;//�洢ӵ�еĿ���
    public string playerObjectLocation;
    public GameObject player;

    public void Init()
    {
        //�õ����ģ�ͣ����볡����
        playerObjectLocation = PlayerObject.PlayerPrefabLocation;

        player = Object.Instantiate(Resources.Load(playerObjectLocation)) as GameObject;
        player.transform.position = new Vector3(217,295,0);
        //player.transform.GetComponentInChildren<Transform>().DOScale()
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
