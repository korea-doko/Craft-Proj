using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayManager : MonoBehaviour {

    private enum PlaySceneManagerName
    {
        Player,
        Item,
        Hero,
        Menu
    }

    private IManager[] m_mgrAry;
    private IUpdatable[] m_updatableAry;
    private int m_numOfMgr;

    private void Awake()
    {
        m_numOfMgr = System.Enum.GetNames(typeof(PlaySceneManagerName)).Length;
        m_mgrAry = new IManager[m_numOfMgr];
        m_updatableAry = new IUpdatable[m_numOfMgr];
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
        }


        for (int i = 0; i < m_numOfMgr; i++)
            m_mgrAry[i].InitManager();
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
