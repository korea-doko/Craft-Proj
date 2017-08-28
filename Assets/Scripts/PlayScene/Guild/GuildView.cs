﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGuildView<T> : IView<T> , ILoadable
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;
}
public class GuildView : MonoBehaviour , IGuildView<IGuildModel>{

    [SerializeField] private GuildViewPanel m_guildViewPanel;

    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    public event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;

    public void InitView(IGuildModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Guild/GuildViewPanel") as GameObject;

        m_guildViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<GuildViewPanel>();
        m_guildViewPanel.Init();
        m_guildViewPanel.OnGuildHeroInfoPanelClicked += M_guildViewPanel_OnGuildHeroInfoPanelClicked;
        m_guildViewPanel.OnEquipItemPanelClicked += M_guildViewPanel_OnEquipItemPanelClicked;
    }

    private void M_guildViewPanel_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        OnEquipItemPanelClicked(this, e);
    }

    internal void ShowEquipItemInventory(List<SlotData> slotDataList)
    {
        m_guildViewPanel.ShowEquipItemInventory(slotDataList);
    }

    private void M_guildViewPanel_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        OnGuildHeroInfoPanelClicked(this, e);
    }

    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Guild);

        if (parent == null)
            return false;

        parent.SetGameObjectAsChild(m_guildViewPanel.gameObject);

        return true;
    }

    internal void ShowHeroInfoDetailPanel(HeroData data)
    {
        m_guildViewPanel.ShowHeroInfoDetailPanel(data);
    }

    internal void HideAll()
    {
    }
    internal void ShowAll(List<HeroData> _heroList)
    {
        m_guildViewPanel.Show(_heroList);
    }
}