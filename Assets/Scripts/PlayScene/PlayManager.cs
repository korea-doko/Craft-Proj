using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayManager : MonoBehaviour {

    private enum PlaySceneManagerName
    {
        Mod,
        Sprite,
        Player,
        Item,
        Hero,
        Menu,
        Upgrade,
        Store,
        Guild,
        Alarm
    }

    private IManager[] m_mgrAry;
    private ILoadable[] m_loadableAry;
    private IUpdatable[] m_updatableAry;

    private int m_numOfMgr;

    private bool[] m_numOfLoaded;

    private void Awake()
    {
        m_numOfMgr = System.Enum.GetNames(typeof(PlaySceneManagerName)).Length;
        m_mgrAry = new IManager[m_numOfMgr];
        m_updatableAry = new IUpdatable[m_numOfMgr];
        m_loadableAry = new ILoadable[m_numOfMgr];

        m_numOfLoaded = new bool[m_numOfMgr];
        for (int i = 0; i < m_numOfMgr; i++)
            m_numOfLoaded[i] = false;
    }

    private void Start()
    {
        for (int i = 0; i < m_numOfMgr; i++)
        {
            string name = ((PlaySceneManagerName)i).ToString() + "Manager";
            Type type = Type.GetType(name);

            GameObject obj = Utils.MakeGameObjectWithType(type, this.gameObject);

            m_mgrAry[i] = obj.GetComponent<IManager>();
            m_updatableAry[i] = obj.GetComponent<IUpdatable>();
            m_loadableAry[i] = obj.GetComponent<ILoadable>();
        }


        for (int i = 0; i < m_numOfMgr; i++)
            m_mgrAry[i].InitManager();

        while(!IsLoadAll())
        {
            for (int i = 0; i < m_numOfMgr;i++)
            {
                if (m_loadableAry[i] == null)
                    m_numOfLoaded[i] = true;

                if (m_numOfLoaded[i])
                    continue;

                Debug.Log(((PlaySceneManagerName)i).ToString() + " Manager is Initializing! ");

                m_numOfLoaded[i] = m_loadableAry[i].Load();
            }            
        }

    }
    private  bool IsLoadAll()
    {
        for(int i = 0; i < m_numOfMgr;i++)
        {
            if (!m_numOfLoaded[i])
                return false;
        }
        return true;
    }

    private void Update()
    {
        for(int i = 0; i < m_numOfMgr;i++)
        {
            if (m_updatableAry[i] != null)
                m_updatableAry[i].UpdateThis();
        }
    }
}
