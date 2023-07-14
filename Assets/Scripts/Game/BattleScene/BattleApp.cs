using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleApp : MonoBehaviour
{
    public string baseBackground;
   
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ�����ñ���װ�ں����е�data
        //�����з�װ�˷�����ȡÿ��table����ֵ
        //���Ը���id��ȡһ�л�������table
        //��Ϊinit�л����·��������txt�ļ�ת��Ϊstringȥ
        //instantiate GameConfigData����ʵ�����Ĺ�����
        //�ͻ�ѱ��������Ϣ���ü�ֵ�Դ���list����
        //���ǿ���ͨ��idȥ��������
        GameConfigManager.Instance.Init();

        //ͨ����������ʾ����
        UIMgr.Instance.ShowUI<UIBackground>(baseBackground);
        
        //test, ��ȡidΪ1001�Ŀ��Ƶ�����
        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
