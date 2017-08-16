using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour,IManager {

    private HeroModel m_model;
    private HeroView m_view;

    private static HeroManager m_inst;
    public static HeroManager Inst
    {
        get
        {
            return m_inst;
        }        
    }

    public void InitManager()
    {
        m_inst = this;

        m_model = Utils.MakeGameObjectWithComponent<HeroModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<HeroView>(this.gameObject);
        m_view.InitView(m_model);
    }
}
