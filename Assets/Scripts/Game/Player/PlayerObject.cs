using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : RoleObject
{
    public static string PlayerPrefabLocation = "Model/Bronya2";
    // Start is called before the first frame update
    protected override void Awake()
    {
        //������ص�Awake�߼�һ����Ҫ����
        base.Awake();
        //�����������
        InputMgr.GetInstance().StartOrEndCheck(true);
        //��ȡ����Ȩ��
        GetController();
    }

    protected override void Update()
    {
        //һ��Ҫ�������base.Update�Ĵ��� ��Ϊ �ƶ��߼� ��д�ڸ����е�
        //����֮����Ҫ��д �Ų���Ҫ��
        base.Update();
    }

    /// <summary>
    /// �������Ȩ
    /// </summary>
    public void GetController()
    {
        //�¼����� �м� ���м� һ����Ҫ�����洫 ��ô����ʽ һ�������·�ȥ����һ������
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", CheckX);
        EventCenter.GetInstance().AddEventListener<float>("Vertical", CheckY);
        //����������������
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
    }

    /// <summary>
    /// �������Ȩ
    /// </summary>
    public void RemoveController()
    {
        //�¼����� �м� ���м� һ����Ҫ�����洫 ��ô����ʽ һ�������·�ȥ����һ������
        EventCenter.GetInstance().RemoveEventListener<float>("Horizontal", CheckX);
        EventCenter.GetInstance().RemoveEventListener<float>("Vertical", CheckY);
        //����������������
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
    }

    private void CheckX(float x)
    {
        //x �ͻ��� -1 0 1����ֵ���� 
        // �� A Ϊ-1  ����Ϊ0  ��DΪ1
        //��ȡ�������뷽�� 
        moveDir.x = x;
    }

    private void CheckY(float y)
    {
        //x �ͻ��� -1 0 1����ֵ���� 
        // �� S Ϊ-1  ����Ϊ0  ��WΪ1
        //��ȡ�������뷽��
        moveDir.y = y;
    }

    /// <summary>
    /// ������ ���ƶ������ ��������
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyDown(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.J:
                print("J��");
                break;
            case KeyCode.K:
                print("K��");
                break;
            case KeyCode.L:
                print("L��");
                break;
            case KeyCode.Space:
                print("Space��");
                break;
        }
    }


    private void OnDestroy()
    {
        //�¼� �мӾ��м� �Ƴ�ʱһ��Ҫע���¼�
        RemoveController();
    }
}
