using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeViewPanel
{
    event EventHandler OnItemSelectButtonClicked;
    event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;

}

public class UpgradeViewPanel : MonoBehaviour, IUpgradeViewPanel, ILoadable
{
    [SerializeField] private RuneIconPanel m_runeIconPanel;
    [SerializeField] private BaseItemPanel m_baseItemPanel;
    [SerializeField] private ItemSelectInventoryPanel m_itemSelectInventoryPanel;

    public event EventHandler OnItemSelectButtonClicked;
    public event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;

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
    public void ShowItemInfoAtDescPanel(ItemData _data)
    {
        m_baseItemPanel.ShowItemInfoAtDescPanel(_data);        
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
    public void Show()
    {
        m_runeIconPanel.Show();
    }

    private void InitRuneIconPanel()
    {
        GameObject prefab = Resources.Load("Playscene/Upgrade/RuneIconPanel") as GameObject;

        m_runeIconPanel = ((GameObject)Instantiate(prefab)).GetComponent<RuneIconPanel>();
        m_runeIconPanel.transform.SetParent(this.transform);
        m_runeIconPanel.Init();
        m_runeIconPanel.OnRuneButtonClicked += M_runeIconPanel_OnRuneButtonClicked;
    }
    private void InitBaseItemPanel()
    {
        GameObject prefab = Resources.Load("PlayScene/Upgrade/BaseItemPanel") as GameObject;

        m_baseItemPanel = ((GameObject)Instantiate(prefab)).GetComponent<BaseItemPanel>();
        m_baseItemPanel.transform.SetParent(this.transform);
        m_baseItemPanel.Init();
        m_baseItemPanel.OnItemSelectButtonClicked += M_baseItemPanel_OnItemSelectButtonClicked;
    }
    private void InitItemSelectInventoryPanel()
    {
        GameObject prefab = Resources.Load("PlayScene/Upgrade/ItemSelectInventoryPanel") as GameObject;
        m_itemSelectInventoryPanel = ((GameObject)Instantiate(prefab)).GetComponent<ItemSelectInventoryPanel>();
        m_itemSelectInventoryPanel.transform.SetParent(this.transform);
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
}
