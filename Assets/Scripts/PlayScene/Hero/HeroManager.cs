using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour,IManager,ILoadable {

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


        for (int i = 0; i < 3; i++)
            MakeAvailableHero();
    }

    public bool Load()
    {
        return m_view.Load();
    }

    public void MakeAvailableHero()
    {
        m_model.MakeAvailableHero();
    }

    public void HeroMenuButtonClicked()
    {
        m_view.HeroMenuButtonClicked(m_model);
    }
}
