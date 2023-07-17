using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Dictionary<string, string> data;
    public string drawEffect;
    public string hoverEffect;
    public string placeEffect;


    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
        drawEffect = "Cards/draw";
        hoverEffect = "Cards/cardShove";
        placeEffect = "Cards/cardPlace";
        

    }

    private int index;

    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        //�����Ŵ�1.5ÿ������0.25
        transform.DOScale(1.5f, 0.25f);
        //�������Ǹı俨�Ƶ���Ⱦ˳����Ϊ�Ŵ�Ҫ�ڵ���ϵ
        //���԰������ó������Ⱦ
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        //ͨ���߿��������ɫ
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //����뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        //�ص�1
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private void Start()
    {
        //���ؿ����ϵ�����
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"],data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        //�Ȼ�ÿ������ͣ�Ȼ��ͨ����������ٻ������
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

        //����bg����image����߿����
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }
    Vector2 initPos;//��ק��ʼ��¼����λ��

    //end drag
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //dragging card
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out pos
              ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }    



    }
    //drag card
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //��������

        BattleAudio.Instance.changeEffect(drawEffect);


        
    }

    //try to use the card
    public virtual bool TryUse()
    {
        //�������
        int cost = int.Parse(data["Expend"]);

        if(cost > FightManager.Instance.CurPointCount)
        {
            //���ò���
            BattleAudio.Instance.changeEffect("SFX/Effect/Loss");

            //��ʾ
            UIMgr.Instance.ShowTip("No Enough Point", Color.red);

            return false;

        }
        else
        {
            FightManager.Instance.CurPointCount -= cost;
            //ˢ�·����ı�
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdatePoint();

            //ʹ�õĿ���ɾ��
            UIMgr.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        }
    }

    //��������ʹ�ú����Ч
    public void playEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
       
        effectObj.transform.position = pos;
        //effectObj.transform.SetParent(GameObject.Find("Canvas").transform);
        
        Destroy(effectObj, 2);
    }

   
}
