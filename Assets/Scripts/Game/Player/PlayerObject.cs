using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerObject : RoleObject
{
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;

    public static string modelPath;
    private Collider2D playerCollider;
    private static GameObject collideObject;
    private static bool finishBattle = false;
    private FightingObject fightingObject;
    private static int NUMBER_OF_DESTORIED;

    // Start is called before the first frame update
    protected override void Awake()
    {
        
        fightingObject = PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith" + NUMBER_OF_DESTORIED) as FightingObject;
        //������ص�Awake�߼�һ����Ҫ����
        base.Awake();
        playerCollider = GetComponent<Collider2D>();
        //ѡ���Ӧ�����modelԤ���岢������Player
        //loadCharModel(characterData.Instance.characterID);
        loadCharModel(0);
        //�����������
        InputMgr.GetInstance().StartOrEndCheck(true);
        //��ȡ����Ȩ��
        GetController();
        if (finishBattle)
        {
            /*if (collideObject == null)
            {
                print("is null");
            }
            else
            {
                print("Not null");
            }*/
            //����һ������˼������ˣ����ҵ�playerpref֮ǰ�洢�Ĵݻٹ��ĵ��ˣ�����һ��ݻ�
            for(int i = 0; i < NUMBER_OF_DESTORIED;  i++)
            {
                string name = (PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith" + i) as FightingObject).fightingName;
                print("tring to destory " + name);
                GameObject temp = GameObject.Find(name);
                if (temp != null)
                {
                    Destroy(temp);
                    EnemySpawnPoint.Instance.spawnPointCollider.Remove(temp.GetComponent<Collider2D>());
                    print(EnemySpawnPoint.Instance.spawnPointCollider.Count);
                }
                else
                {
                    print("why is null");
                }
            }
            
            //GameObject temp = GameObject.Find((PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith") as FightingObject).fightingName);
            /*print(temp.name);
            Destroy(temp);*/
            finishBattle = false;

        }
    }

    /*private void Start()
    {
        if (finishBattle)
        {
            Destroy(playerCollider);
        }
    }*/

    public static void changeFinish()
    {
        finishBattle = true;
    }

    /*public static GameObject getCollideObject()
    {
        return collideObject;
    }*/





    protected override void Update()
    {
        //print(finishBattle);
        //һ��Ҫ�������base.Update�Ĵ��� ��Ϊ �ƶ��߼� ��д�ڸ����е�
        //����֮����Ҫ��д �Ų���Ҫ��
        base.Update();
        //if (playerCollider.IsTouching(EnemySpawnPoint.Instance.spawnPointCollider))
        bool touchingOrNot = false;
        Collider2D collider = null;
        string collidingName = null;
        
        //����Ƿ����ײ�巢����ײ������о͸ı�prompt��λ��
        foreach(Collider2D each in EnemySpawnPoint.Instance.spawnPointCollider){
            if (playerCollider.IsTouching(each))
            {
                touchingOrNot = true;
                EnemySpawnPoint.Instance.promptSprite.GetComponent<RectTransform>().position = each.GetComponent<Transform>().position;
                collidingName = each.name;
                collider = each;
                break;
            }
            
        }

        if (touchingOrNot)
        {
            //print("Touching enemy");

            EnemySpawnPoint.Instance.promptSprite.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                finishBattle = false;
                //collideObject = EnemySpawnPoint.Instance.spawnedObject;
                /*if(collideObject == null)
                {
                    print("wtfwtf");
                }
                else
                {
                    print(collideObject.name);
                }
                DontDestroyOnLoad(GameObject.Find("LevelBackground"));*/
                print(collidingName);
                fightingObject.fightingName = collidingName;
                PlayerPrefsDataMgr.Instance.SaveData(fightingObject, "FightingWith" + NUMBER_OF_DESTORIED);
                FightingObject temp = PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith" + NUMBER_OF_DESTORIED) as FightingObject;
                
                print("I stored " + temp.fightingName);
                EnemySpawnPoint.Instance.spawnPointCollider.Remove(collider);
                NUMBER_OF_DESTORIED++;
                SceneLoader.Instance.LoadScene("BattleScene");
                return;
                
            }
        } else
        {
            EnemySpawnPoint.Instance.promptSprite.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ͨ������menu��CharID���ؽ�ɫԤ���壬������Ϊ�Ӷ���
    /// </summary>
    /// <param name="CharID"></param>
    public void loadCharModel(int CharID)
    {
        //string modelPath;
        switch (CharID)
        {
            case 0:
                modelPath = "Model/Bronya";
                break;
            case 1:
                modelPath = "Model/Elysia";
                break;
            case 2:
                modelPath = null;
                Debug.Log("Unimplemented charID 2");
                break;
            default:
                modelPath = null;
                Debug.Log("Error during loadCharModel, charID unknown");
                break;
        }
       
        GameObject playerModel = (GameObject)Resources.Load(modelPath);
        playerModel = Instantiate(playerModel);
        playerModel.transform.SetParent(transform, false);
        playerModel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
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
