using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public class UIBase : MonoBehaviour
{
    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    //hide
    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }
    //close destory
    public virtual void Close()
    {
        Destroy(gameObject);
    }
}
