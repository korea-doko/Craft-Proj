using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGuildView<T> : IView<T> , ILoadable
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;
    event EventHandler<OnItemChangedButtonClickedArgs> OnItemChangedButtonClicked;
    event EventHandler<OnItemRemoveButtonClickedArgs> OnItemRemoveButtonClicked;

}
public class GuildView : MonoBehaviour , IGuildView<IGuildModel>{

    [SerializeField] private GuildViewPanel m_guildViewPanel;

    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
    public event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;
    public event EventHandler<OnItemChangedButtonClickedArgs> OnItemChangedButtonClicked;
    public event EventHandler<OnItemRemoveButtonClickedArgs> OnItemRemoveButtonClicked;

    public void InitView(IGuildModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Guild/GuildViewPanel") as GameObject;

        m_guildViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<GuildViewPanel>();
        m_guildViewPanel.Init();
        m_guildViewPanel.OnGuildHeroInfoPanelClicked += M_guildViewPanel_OnGuildHeroInfoPanelClicked;
        m_guildViewPanel.OnItemChangedButtonClicked += M_guildViewPanel_OnItemChangedButtonClicked;
        m_guildViewPanel.OnEquipItemInventorySlotClicked += M_guildViewPanel_OnEquipItemInventorySlotClicked;
        m_guildViewPanel.OnItemRemoveButtonClicked += M_guildViewPanel_OnItemRemoveButtonClicked;
    
    }

    

    public void ShowEquipItemInventory(List<SlotData> slotDataList)
    {
        m_guildViewPanel.ShowEquipItemInventory(slotDataList);
    }
    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Guild);

        if (parent == null)
            return false;

        
        parent.SetGameObjectAsChild(m_guildViewPanel.gameObject);

        return m_guildViewPanel.Load();
    }
    public void ShowHeroInfoDetailPanel(HeroData data)
    {
        m_guildViewPanel.ShowHeroInfoDetailPanel(data);
    }
    public void HideAll()
    {
        m_guildViewPanel.Hide();
    }
    public void ShowAll(List<HeroData> _heroList)
    {
        m_guildViewPanel.Show(_heroList);
    }

    // 이벤트 핸들러
    private void M_guildViewPanel_OnItemChangedButtonClicked(object sender, OnItemChangedButtonClickedArgs e)
    {
        OnItemChangedButtonClicked(this, e);
    }
    private void M_guildViewPanel_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        OnGuildHeroInfoPanelClicked(this, e);
    }
    private void M_guildViewPanel_OnEquipItemInventorySlotClicked(object sender, EquipItemInventorySlotClickedArgs e)
    {
        OnEquipItemInventorySlotClicked(this, e);
    }    
    private void M_guildViewPanel_OnItemRemoveButtonClicked(object sender, OnItemRemoveButtonClickedArgs e)
    {
        OnItemRemoveButtonClicked(this, e);
    }
}
