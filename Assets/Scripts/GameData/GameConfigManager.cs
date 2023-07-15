using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������Ϸ���ñ�Ĺ�����
public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    private GameConfigData cardData;//card
    private GameConfigData enemyData;//enemies
    private GameConfigData levelData;//level

    //����load txt�ļ�ͨ��.txt�ķ�������ת����string
    private TextAsset textAsset;

    //��ʼ�������ļ���txt�ļ��洢���ڴ��У�
    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        //��·����GameConfigData��instantiate��ʱ��ͻ���ɼ�ֵ�ԵĴ洢
        cardData = new GameConfigData(textAsset.text);
        
        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);
        
        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);
        
    }

    //��ȡ���ƵĴ洢��ֵ�Ե�list
    public List<Dictionary<string,string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    //ͨ��id��ȡ����
    public Dictionary<string, string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }

    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }

    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

}
