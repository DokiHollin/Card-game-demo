using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum E_ActionType
{
    None,
    Defend,
    Attack
}

public class Enemy : MonoBehaviour
{
    //enemy data
    private Dictionary<string, string> data;
    public E_ActionType actionType;
    public GameObject hpItemObj;
    public GameObject actionObj;

    //�����ж���ui
    public Transform attackTf;
    public Transform defendTf;
    //Ѫ������ui
    public Text defendText;
    public Text hpText;
    public Image hpImage;

    //��ֵ���,�������ֵ������Text��UI��
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        //���ó�ʼ�ж�ΪNone
        actionType = E_ActionType.None;
        //�������������UI
        hpItemObj = UIMgr.Instance.CreateHPItem();
        actionObj = UIMgr.Instance.CreateActionIcon();

        //�õ�attack defense��transform
        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        //�õ���ֵtext
        defendText = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpText = hpItemObj.transform.Find("hpText").GetComponent<Text>();
        hpImage = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //����Ѫ�� �ж���λ��
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Bottom").position);
        //actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").position);
        Vector3 enemyVec = transform.Find("Head").position;
        enemyVec.z = 0;
        actionObj.transform.position = Camera.main.WorldToScreenPoint(enemyVec);

        SetRandomAction();

        //��ʼ����ֵ
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        UpdateHp();
        UpdateDefend();
    }



    //���һ���ж�
    public void SetRandomAction()
    {
        int rand = Random.Range(1, 3);//����action�е�һ��
        actionType = (E_ActionType)rand;
        switch(actionType)
        {
            case E_ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
            case E_ActionType.None:
                break;
            case E_ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
        }
    }

    //����Ѫ��
    public void UpdateHp()
    {
        hpText.text = CurHp + "/" + MaxHp;
        hpImage.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //���·���
    public void UpdateDefend()
    {
        defendText.text = Defend.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
