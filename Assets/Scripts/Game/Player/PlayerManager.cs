using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//负责管理用户的卡牌信息金币等等
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public static int DEFAULT_ATTACK = 4;
    public List<string> cardList;//存储拥有的卡牌

    public void Init()
    {
        cardList = new List<string>();
        //初始四张攻击卡,四张防御
        for(int i = 0; i < DEFAULT_ATTACK; i++)
        {
            cardList.Add("1000");
            cardList.Add("1001");
        }
        //两张效果卡
        cardList.Add("1002");
        cardList.Add("1002");

    }

 
}
