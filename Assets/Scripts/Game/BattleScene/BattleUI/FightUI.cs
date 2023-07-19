using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//ս������
public class FightUI : UIBase
{
    private Text cardCountText;//��������
    private Text usedCardHeapText;//���ƶ�
    private Text pointText; //����
    private Text hpText; //Ѫ��
    private Image hpImage;//Ѫ��ͼƬ
    private Text defenseText; //����

    private List<CardItem> cardItemList = new List<CardItem>();//�洢��������ļ�����

    private void Awake()
    {
        cardCountText = this.transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardHeapText = this.transform.Find("noCard/icon/Text").GetComponent<Text>();
        pointText = this.transform.Find("mana/Text").GetComponent<Text>();
        hpText = this.transform.Find("hp/Text").GetComponent<Text>();
        hpImage = transform.Find("hp/fill").GetComponent<Image>();
        defenseText = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnButton);
    }

    //��һ�ͽ������л����˻غ�
    private void onChangeTurnButton()
    {
        //ֻ����һغϲ����л�
        if(FightManager.Instance.fightUnit is PlayerTurn)
        {
            FightManager.Instance.ChnageType(E_FightType.Enemy);
        }
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

    //create card object
    public void CreateCardItem(int count)
    {
        
        if(count > FightCardManager.Instance.cardList.Count)
        {
            print("No more Cards");
            count = FightCardManager.Instance.cardList.Count;
        }
        //��������
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            //�õ���ȡ�Ŀ���id
            string cardId = FightCardManager.Instance.DrawCard();
            //����id�õ�������Ϣ
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            //������UI�ӽű�
            //CardItem item = obj.AddComponent<CardItem>();
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            
            //����cardItem
            item.Init(data);
            //�ѿ��ƷŽ�cardList
            //cardItemList.Add(item);
            cardItemList.Add(item);
        
        }
    }

    //update card position
    public void UpdateCardItemPos()
    {
        float offset = 800.0f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2.0f * offset + offset * 0.5f, -700);
        for(int i = 0;i < cardItemList.Count;i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x = startPos.x + offset;
        }
    }

    //ɾ����������

    public void RemoveCard(CardItem item) {

        //�Ƴ���Ч
        BattleAudio.Instance.changeEffect("SFX/Card/cardShove");
        item.enabled = false;//���ÿ���

        //�������ƶ�
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        //����ʹ�ú�Ŀ�������
        usedCardHeapText.text = FightCardManager.Instance.usedCardList.Count.ToString();
        
        //�Ӽ�����ɾ��
        cardItemList.Remove(item);

        //���¿���λ��
        UpdateCardItemPos();

        //�����Ƶ����ƶ��е�Ч��
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject, 1);
    
    
    }
    //ɾ�����п�
    public void RemoveAllCard()
    {
        for(int i = cardItemList.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardItemList[i]);
        }
    }
}
