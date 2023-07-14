using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϸ���ñ��� ÿ�������Ӧһ��txt���ñ�
public class GameConfigData
{
    //�洢���ñ��е��������ݣ�ÿ��Dictionary�洢һ�е�����
    private List<Dictionary<string, string>> dataDic;

    public GameConfigData(string str)
    {
        //��ʼ��list
        dataDic = new List<Dictionary<string, string>>();
        //�����и�
        string[] lines = str.Split('\n');
        //��һ���Ǵ洢���ݵ����� ��trimɾ���ַ���ͷβ�ո�
        string[] title = lines[0].Trim().Split('\t');//tab
        //�ӵ����п�ʼ�������ݣ��ڶ��������ǽ���˵��, �������Ҫ�����ǼӼ�ֵ�ԣ�
        //key������value�Ǿ�������
        for(int i = 2; i < lines.Length; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] tempArr = lines[i].Trim().Split('\t');

            for(int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);

            }

            dataDic.Add(dic);
        }
        //��������ʼ����������Ϊ��ֵ�Դ�����dataDic�����һ������������ݸ��ݸ�����str

    }

    //��������list
    public List<Dictionary<string,string>> GetLines()
    {
        return dataDic;
    }

    //����id�ҵ�dictionary
    public Dictionary<string,string> GetOneById(string id)
    {
        for(int i = 0; i < dataDic.Count; i++)
        {
            Dictionary<string,string> dic = dataDic[i];
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
        return null;
    }
}
