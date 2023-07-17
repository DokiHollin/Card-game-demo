using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance;
    private Transform canvasTF;//�����任���
    private List<UIBase> uiList;//�洢���ع��Ľ���ļ���

    private void Awake()
    {
        Instance = this;
        canvasTF = GameObject.Find("Canvas").transform;
        //��ʼ��
        uiList = new List<UIBase>();

    }

    public UIBase ShowUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null)
        {
            //instantiate as child of canvas
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;

            /*float width = canvasTF.GetComponent<RectTransform>().position.x;
            float height = canvasTF.GetComponent<RectTransform>().position.y;
            //obj.transform.position = new Vector2(obj.transform.InverseTransformPoint, 1110);
            RectTransform objRec = obj.GetComponent<RectTransform>();
            RectTransform rec = new RectTransform();
            
            obj.transform.SetParent(canvasTF);*/


            obj.name = uiName;
            //�����Ҫ�Ľű�
            ui = obj.AddComponent<T>();
            //��ӵ����Ͻ��д洢
            uiList.Add(ui);
        }
        else
        {

            ui.Show();
        }
        return ui;
    }
    //hide ui
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui! != null)
        {
            ui.Hide();
        }
    }

    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            uiList.Remove(ui);
            ui.Close();
        }

    }

    public void CloseAllUI()
    {
        foreach (UIBase ui in uiList)
        {
            ui.Close();
        }
        uiList.Clear();
    }

    //�Ӽ������ҵ�ָ�����ֵĽ���ű�
    public UIBase Find(string uiName)
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
        }

        return null;
    }

    //��������ͷ�����ж�����
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTF) as GameObject;
        obj.transform.SetAsLastSibling();//���ø��������һλ
        return obj;
    }
    //�������˵ײ�Ѫ������
    public GameObject CreateHPItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTF) as GameObject;
        obj.transform.SetAsLastSibling();//���ø��������һλ
        return obj;
    }

    //������ʾ����
    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTF) as GameObject;
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        //����bg��background���Ĵ�С��1
        Tween scale1 = obj.transform.Find("bg").DOScaleY(1, 0.4f);
        //�ص�ԭ�������ܱ�����
        Tween scale2 = obj.transform.Find("bg").DOScaleY(0, 0.4f);

        //��ʾ0.5�룬����һ�������������ִ��
        //callback�Ķ���
        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.8f);
        seq.Append(scale2);

        seq.AppendCallback(delegate () {
            if (callback != null)
            {
                callback();
            }

        });

        
        Destroy(obj, 2);


    }

    //���ĳ������Ľű�
    public T GetUI<T>(string uiName) where T:UIBase
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;
    }

   

   

}
