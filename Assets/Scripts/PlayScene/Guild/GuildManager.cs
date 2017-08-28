﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuildManager:IManager,ILoadable
{

}
public class GuildManager : MonoBehaviour ,IGuildManager{

    private GuildModel m_model;
    private GuildView m_view;

    private static GuildManager m_inst;
    public static GuildManager Inst
    {
        get
        {
            return m_inst;
        }
    }
    public GuildManager()
    {
        m_inst = this;
    }

    public void InitManager()
    {
        m_model = Utils.MakeGameObjectWithComponent<GuildModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<GuildView>(this.gameObject);
        m_view.InitView(m_model);
        m_view.OnGuildHeroInfoPanelClicked += M_view_OnGuildHeroInfoPanelClicked;
        m_view.OnEquipItemPanelClicked += M_view_OnEquipItemPanelClicked;
    }

    private void M_view_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        List<SlotData> slotDataList = StoreManager.Inst.GetSlotDataList;
       
        m_view.ShowEquipItemInventory(slotDataList);

        Debug.Log(e.m_clickedParts.ToString() + "is clicked");
    }

    private void M_view_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        HeroData data = PlayerManager.Inst.GetHeroData(e.m_id);

        m_view.ShowHeroInfoDetailPanel(data);
    }

    public bool Load()
    {
        return m_view.Load();
    }

    internal void MenuButtonClicked(MenuName menuName)
    {
        if( menuName != MenuName.Guild)
            m_view.HideAll();        
        else
        {
            List<HeroData> heroList = PlayerManager.Inst.GetOwnedHeroDataList();
            m_view.ShowAll(heroList);
        }
    }
}