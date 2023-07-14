using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel; //��ȡExcel,Ҫ�õ�excel��ص�plugin
using System.Data;
using System.Drawing.Printing;

//�༭���ű�
public class MyEditor
{
    //��unityͷ����ӹ���
    [MenuItem("MyTool/excel to txt")]
    public static void ExportExcelToTxt()
    {
        //_Excel�ļ���·��
        string assetPath = Application.dataPath + "/_Excel";


        //����ļ�����excel�ļ�
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");

        for(int i = 0; i < files.Length; i++)
        {
            //replace \ to /
            files[i] = files[i].Replace('\\', '/');

            //ͨ���ļ�����ȡ�ļ�
            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                //�ļ���ת��excel����
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

                //��ȡexcel����
                DataSet dataSet = excelDataReader.AsDataSet();

                //��ȡexcel��һ�ű�
                DataTable table = dataSet.Tables[0];

                //���������ݶ�ȡ��洢����Ӧ��txt�ļ�
                readTableToTxt(files[i], table);



            }
        }

        //ˢ�±�����
        AssetDatabase.Refresh();




    }

    private static void readTableToTxt(string filePath, DataTable table)
    {
        //��ȡ�ļ���(��Ҫ�ļ���׺������֮������ͬ��txt�ļ�) û�к�׺�ļ�������txt xml����
        string fileName = Path.GetFileNameWithoutExtension(filePath);

        //txt�ļ��洢·��
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";

        //�ж�reources/Data�ļ������Ƿ��Ѿ����ڶ�Ӧtxt���������ɾ��
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        //�ļ�������txt�ļ�
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            //�ļ���תд����������д���ַ���
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //����table
                for(int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];
                    string str = "";
                    //������
                    for(int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();

                        str = str + val + "\t"; //ÿһ��tab�ָ�
                    }

                    //д��
                    sw.Write(str);

                    //����������һ�� ����
                    if(row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    
    }


}
