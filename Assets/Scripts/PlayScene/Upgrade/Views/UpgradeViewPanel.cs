using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeViewPanel:ILoadable,IUpdatable
{
    event EventHandler OnItemSelectButtonClicked;
    event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;

}

public class UpgradeViewPanel : MonoBehaviour, IUpgradeViewPanel
{
    [SerializeField] private RuneIconPanel m_runeIconPanel;
    [SerializeField] private BaseItemPanel m_baseItemPanel;
    [SerializeField] private ItemSelectInventoryPanel m_itemSelectInventoryPanel;

    public event EventHandler OnItemSelectButtonClicked;
    public event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    public event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;

    public void Init()
    {
        InitRuneIconPanel();
        InitBaseItemPanel();
        InitItemSelectInventoryPanel();
    }
    public bool Load()
    {
        return m_itemSelectInventoryPanel.Load();
    }
  
    public void ShowItemSelectInventoryPanel(List<SlotData> _dataList)
    {
        m_itemSelectInventoryPanel.Show(_dataList);
    }
    public void ShowSelectedItem(ItemData itemData)
    {
        m_baseItemPanel.ShowSelectedItem(itemData);
    }
    
    public void HideItemSelectInventoryPanel()
    {
        m_itemSelectInventoryPanel.Hide();
    }

    public void Hide()
    {
        m_baseItemPanel.Hide();
        m_itemSelectInventoryPanel.Hide();
    }
    public void Show(int[] _playerOwnedRunes)
    {
        m_runeIconPanel.Show(_playerOwnedRunes);
    }
    public void UpdateThis()
    {
        m_runeIconPanel.UpdateThis();
    }

    private void InitRuneIconPanel()
    {
        m_runeIconPanel.Init();
        m_runeIconPanel.OnRuneButtonClicked += M_runeIconPanel_OnRuneButtonClicked;
        m_runeIconPanel.OnRuneButtonLongPressed += M_runeIconPanel_OnRuneButtonLongPressed;
    }
    private void InitBaseItemPanel()
    {
        m_baseItemPanel.Init();
        m_baseItemPanel.OnItemSelectButtonClicked += M_baseItemPanel_OnItemSelectButtonClicked;
    }
    private void InitItemSelectInventoryPanel()
    {
        m_itemSelectInventoryPanel.Init();
        m_itemSelectInventoryPanel.OnItemSelectSlotClicked += M_itemSelectInventoryPanel_OnItemSelectSlotClicked;
    }

    // 이벤트 핸들러
    private void M_itemSelectInventoryPanel_OnItemSelectSlotClicked(object sender, ItemSelectSlotArgs e)
    {
        OnItemSelectSlotClicked(this, e);
    }
    private void M_baseItemPanel_OnItemSelectButtonClicked(object sender, EventArgs e)
    {
        OnItemSelectButtonClicked(this, EventArgs.Empty);
    }
    private void M_runeIconPanel_OnRuneButtonClicked(object sender, RuneButtonClickArgs e)
    {
        OnRuneButtonClicked(this, e);
    }
    private void M_runeIconPanel_OnRuneButtonLongPressed(object sender, RuneButtonLongPressedArgs e)
    {
        OnRuneButtonLongPressed(this, e);
    }

}
