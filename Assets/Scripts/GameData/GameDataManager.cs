using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance { get => instance; }
    public MusicData musicData;
    private GameDataManager() {
        
        
        //��ʼ����Ϸ����
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "Music") as MusicData;
        
        //���û������Ҫô��falseҪô��0

        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.isOpenBG = true;
            musicData.isOpenEffect = true;
            musicData.bgValue = 1;
            musicData.effectValue = 1;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

        
        }
    
    }

    //�ṩAPI��������Ĵ洢
    //�������߹رձ�������
    public void oepnOrCloseBGMusic(bool isOpen)
    {
        musicData.isOpenBG = isOpen;
        //�洢�ı�������
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }

    //��Ч
    public void oepnOrCloseEffect(bool isOpen)
    {
        musicData.isOpenEffect = isOpen;
        //�洢�ı�������
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }

    public void changeBGValue(float value)
    {
        musicData.bgValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }

    public void changeEffectValue(float value)
    {
        musicData.effectValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }





}
