﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGuildViewPanel
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;
}

public class GuildViewPanel : MonoBehaviour , IGuildViewPanel{

    [SerializeField] private GuildHeroPanel m_guildHeroPanel;
    [SerializeField] private GuildHeroInfoDetailPanel m_guildHeroInfoDetailPanel;
    [SerializeField] private EquipItemInventoryPanel m_equipItemInventoryPanel;

    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    public event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;

    public void Init()
    {
        InitGuildHeroPanel();
        InitGuildHeroInfoDetailPanel();
        InitEquipItemInventoryPanel();
    }

    private void InitEquipItemInventoryPanel()
    {
        GameObject prefab = Resources.Load("PlayScene/Guild/EquipItemInventoryPanel") as GameObject;
        m_equipItemInventoryPanel = ((GameObject)Instantiate(prefab)).GetComponent<EquipItemInventoryPanel>();
        m_equipItemInventoryPanel.Init();
        m_equipItemInventoryPanel.transform.SetParent(this.transform);
    }

    internal void Show(List<HeroData> heroList)
    {
        m_guildHeroPanel.Show(heroList);
    }
    void InitGuildHeroPanel()
    {
        GameObject prefab = Resources.Load("PlayScene/Guild/GuildHeroPanel") as GameObject;
        m_guildHeroPanel = ((GameObject)Instantiate(prefab)).GetComponent<GuildHeroPanel>();
        m_guildHeroPanel.Init();
        m_guildHeroPanel.OnGuildHeroInfoPanelClicked += M_guildHeroPanel_OnGuildHeroInfoPanelClicked;
        m_guildHeroPanel.transform.SetParent(this.transform);
    }

    private void M_guildHeroPanel_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        OnGuildHeroInfoPanelClicked(this, e);
    }

    internal void ShowEquipItemInventory(List<SlotData> slotDataList)
    {
        m_equipItemInventoryPanel.Show();
    }

    private void M_guildHeroInfoDetailPanel_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        OnEquipItemPanelClicked(this, e);
    }

    void InitGuildHeroInfoDetailPanel()
    {
        GameObject prefab = Resources.Load("PlayScene/Guild/GuildHeroInfoDetailPanel") as GameObject;

        m_guildHeroInfoDetailPanel = ((GameObject)Instantiate(prefab)).GetComponent<GuildHeroInfoDetailPanel>();
        m_guildHeroInfoDetailPanel.transform.SetParent(this.transform);
        m_guildHeroInfoDetailPanel.Init();
        m_guildHeroInfoDetailPanel.OnEquipItemPanelClicked += M_guildHeroInfoDetailPanel_OnEquipItemPanelClicked;
    }

    
    internal void ShowHeroInfoDetailPanel(HeroData data)
    {
        m_guildHeroInfoDetailPanel.Show(data);
    }
}