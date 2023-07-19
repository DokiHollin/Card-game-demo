using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//attack card
public class AttackCardItem : CardItem, IPointerDownHandler
{

    public override void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        
    }



    //����
    public void OnPointerDown(PointerEventData eventData)
    {
        //��������
        BattleAudio.Instance.changeEffect("Cards/draw");

        //��ʾ���߽���
        UIMgr.Instance.ShowUI<LineUI>("LineUI");


        //���ÿ�ʼ��λ��
        UIMgr.Instance.GetUI<LineUI>("LineUI").SetStarPos(transform.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 100));

        //�������
        Cursor.visible = false;
        //�ر�1����Эͬ����
        StopAllCoroutines();
        //���������Эͬ����
        StartCoroutine(OnMouseDownRight(eventData));
    }

    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //����ٴΰ�������Ҽ�����ѭ��1
            if (Input.GetMouseButton(1))
            {
                break;
            }

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    transform.parent.GetComponent<RectTransform>(),
                    pData.position,
                    pData.pressEventCamera,
                    out pos
                
                ))
            {
                //���ü�ͷλ��
                UIMgr.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
                //�������߼���Ƿ���������
                CheckRayToEnemy();
            }
            yield return null;
        }

        //����ѭ���� ��ʾ���
        Cursor.visible = true;
        //�ر�����
        UIMgr.Instance.CloseUI("LineUI");
    }

    Enemy hitEnemy; //���߼�⵽���˽ű�
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 v = ray.GetPoint(0);
        //print(v.ToString());

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            //print("ѡ��������������������������������");
            hitEnemy = hit.transform.GetComponent<Enemy>();

            hitEnemy.OnSelect();//ѡ��

            //����������ʹ�ù�����
            if(Input.GetMouseButtonDown(0)) {
                //�ر�����Эͬ����
                StopAllCoroutines();

                //�����ʾ
                Cursor.visible = true;
                UIMgr.Instance.CloseUI("LineUI");
                if (TryUse() == true)
                {
                    //������Ч
                    playEffect(hitEnemy.transform.position);

                    //�����Ч
                    BattleAudio.Instance.changeEffect("Effect/sword");
                    //��������
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                
                }
                //����δѡ��
                hitEnemy.OnUnSelect();
                //���õ��˽ű�Ϊnull
                hitEnemy = null;
            }
            

        }
        else
        {
            //δ�䵽����
            if (hitEnemy != null)
            {
               hitEnemy.OnUnSelect();
                hitEnemy = null;
            }
        }

    }
}
