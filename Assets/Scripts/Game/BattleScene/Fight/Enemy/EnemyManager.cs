using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���˹�����
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();
    
    //�洢ս���еĵ���
    private List<Enemy> enemyList;

    //���ص���
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        //Id	Name	EnemyIds	           Pos
        //10003	 3	 10001=10002=10003  3,0,1=0,0,1=-3,0,1

        //��ȡ�ؿ���
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);

        //����id��Ϣ
        string[] enemyIds = levelData["EnemyIds"].Split('=');

        //����λ����Ϣ
        string[] enemyPos = levelData["Pos"].Split('=');

        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //����λ��
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //���ݵ���id��ȡ����������Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);

            //����Դ·������
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;
            Vector3 pos = new Vector3(x, y, z);
            obj.GetComponent<RectTransform>().localPosition = pos;
            /*Transform canvas = GameObject.Find("Canvas").transform;
            Texture2D enemy = Resources.Load<Texture2D>("Sprites/Character/cheche_bgremoved");*/

            //��ս�����˽ű�
            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);//�洢��Ϣ
            enemyList.Add(enemy);//����list

        }

    }
}
