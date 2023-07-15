using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ս������
public class FightUI : UIBase
{
    private Text cardCountText;//��������
    private Text usedCardHeapText;//���ƶ�
    private Text pointText; //����
    private Text hpText; //Ѫ��
    private Image hpImage;//Ѫ��ͼƬ
    private Text defenseText; //����

    private void Awake()
    {
        cardCountText = this.transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardHeapText = this.transform.Find("noCard/icon/Text").GetComponent<Text>();
        pointText = this.transform.Find("mana/Text").GetComponent<Text>();
        hpText = this.transform.Find("hp/Text").GetComponent<Text>();
        hpImage = transform.Find("hp/fill").GetComponent<Image>();
        defenseText = transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    public void Start()
    {
        UpdateHP();
        UpdatePoint();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardHeapCount();

    }

    //����Ѫ����ʾ
    public void UpdateHP()
    {
        hpText.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImage.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    //���µ���
    public void UpdatePoint()
    {
        pointText.text = FightManager.Instance.CurPointCount + "/" + FightManager.Instance.MaxPointCount;
    }

    //��������
    public void UpdateDefense()
    {
        //print("defense is: " + FightManager.Instance.DefenseCount.ToString());
        defenseText.text = FightManager.Instance.DefenseCount.ToString();
    }

    //���¿��ѿ�������
    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    //�������ƶ�����
    public void UpdateUsedCardHeapCount()
    {
        usedCardHeapText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }
}
