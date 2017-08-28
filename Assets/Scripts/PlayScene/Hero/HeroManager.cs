using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHeroManager  : IManager, ILoadable
{

}
public class HeroManager : MonoBehaviour,IHeroManager
{

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
        m_view.OnHeroPanelClicked += M_view_OnHeroPanelClicked;
    }

    private void M_view_OnHeroPanelClicked(object sender, HeroPanelClickedArgs e)
    {        
        HeroData heroData = m_model.GetHeroData(e.m_clickedID);
        m_model.RemoveAvailableHeroData(e.m_clickedID);
        m_view.Show(m_model);

        PlayerManager.Inst.HiredHero(heroData);
    }

    public bool Load()
    {
        return m_view.Load();
    }

    


    internal void MenuButtonClicked(MenuName menuName)
    {
        if (menuName == MenuName.Traveller)
            m_view.Show(m_model);// HeroMenuButtonClicked(m_model);
        else
            m_view.Hide();

    }
}
