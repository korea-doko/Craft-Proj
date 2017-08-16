using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour,IManager,IUpdatable{

    private MenuModel m_model;
    private MenuView m_view;

    private static MenuManager m_inst;
    public static MenuManager Inst
    {
        get
        {
            return m_inst;
        } 
    }

    public void InitManager()
    {
        m_inst = this;

        m_model = Utils.MakeGameObjectWithComponent<MenuModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<MenuView>(this.gameObject);
        m_view.InitView(m_model);
    }

    public void UpdateThis()
    {
        m_view.UpdateView();
    }
}
