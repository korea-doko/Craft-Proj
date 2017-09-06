using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public interface IGuildViewPanel : ILoadable
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    
    event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;

    event EventHandler<OnItemChangedButtonClickedArgs> OnClickItemChangedButton;
}

public class GuildViewPanel : MonoBehaviour , IGuildViewPanel{

    [SerializeField] private GuildHeroPanel m_guildHeroPanel;
    [SerializeField] private GuildHeroInfoDetailPanel m_guildHeroInfoDetailPanel;
    [SerializeField] private EquipItemInventoryPanel m_equipItemInventoryPanel;

    [SerializeField] private Button m_guildBtn;
    [SerializeField] private Button m_heroBtn;


    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    public event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;
    public event EventHandler<OnItemChangedButtonClickedArgs> OnClickItemChangedButton;

    public void Init()
    {
        InitButtonEventHandler();

        InitGuildHeroPanel();
        InitGuildHeroInfoDetailPanel();
        InitEquipItemInventoryPanel();
    }

  
    public void Show(List<HeroData> heroList)
    {
        m_guildHeroPanel.Show(heroList);
    }
    public void Hide()
    {
        m_guildHeroInfoDetailPanel.Hide();
        m_equipItemInventoryPanel.Hide();
    }
    public void ShowEquipItemInventory(List<SlotData> slotDataList)
    {
        m_equipItemInventoryPanel.Show(slotDataList);
    }
    public void ShowHeroInfoDetailPanel(HeroData data)
    {
        m_guildHeroInfoDetailPanel.Show(data);
    }

    public bool Load()
    {
        return m_equipItemInventoryPanel.Load();
    }

    private void InitButtonEventHandler()
    {
        m_guildBtn.onClick.AddListener(() => ShowGuildPanels());
        m_heroBtn.onClick.AddListener(() => ShowHeroPanels());
    }
    private void InitGuildHeroPanel()
    {
        m_guildHeroPanel.Init();
        m_guildHeroPanel.OnGuildHeroInfoPanelClicked += M_guildHeroPanel_OnGuildHeroInfoPanelClicked;
    }
    private void InitGuildHeroInfoDetailPanel()
    {
        m_guildHeroInfoDetailPanel.Init();
        m_guildHeroInfoDetailPanel.OnClickItemChangedButton += M_guildHeroInfoDetailPanel_OnClickItemChangedButton;
    }

   

    private void InitEquipItemInventoryPanel()
    {
        m_equipItemInventoryPanel.Init();
        m_equipItemInventoryPanel.OnEquipItemInventorySlotClicked += M_equipItemInventoryPanel_OnEquipItemInventorySlotClicked;
    }

    private void ShowGuildPanels()
    {

    }
    private void ShowHeroPanels()
    {

    }

    // 이벤트 핸들러 처리

  
    private void M_guildHeroPanel_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        OnGuildHeroInfoPanelClicked(this, e);
    }
    private void M_equipItemInventoryPanel_OnEquipItemInventorySlotClicked(object sender, EquipItemInventorySlotClickedArgs e)
    {
        OnEquipItemInventorySlotClicked(this, e);
    }
    private void M_guildHeroInfoDetailPanel_OnClickItemChangedButton(object sender, OnItemChangedButtonClickedArgs e)
    {
        OnClickItemChangedButton(this, e);
    }

}
