using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public interface IStoreViewPanel
{
    event EventHandler<SlotClickedArgs> OnSlotClicked;
}
public class StoreViewPanel : MonoBehaviour, IStoreViewPanel {

    [SerializeField] private InventoryPanel m_inventoryPanel;
    [SerializeField] private ItemInfoPanel m_itemInfoPanel;
    [SerializeField] private ItemCountPanel m_itemCountPanel;

    public event EventHandler<SlotClickedArgs> OnSlotClicked;

    public void Init(StoreModel _model)
    {
        InitInventoryPanel(_model);
        InitInfoPanel(_model);
        InitItemCountPanel(_model);
    }
    
    public void Show(StoreModel _model)
    {
        m_inventoryPanel.Show(_model);
        m_itemCountPanel.Show(_model);
    }
    public void Hide()
    {
        m_itemInfoPanel.Hide();
    }
    public Slot GetSlot(int id)
    {
        return m_inventoryPanel.GetSlot(id);
    }   
    public void ShowItemInfoPanel(ItemData data)
    {
        m_itemInfoPanel.ShowItemInfoPanel(data);
    }

    private void InitInfoPanel(StoreModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Store/ItemInfoPanel") as GameObject;

        m_itemInfoPanel = ((GameObject)Instantiate(prefab)).GetComponent<ItemInfoPanel>();
        m_itemInfoPanel.transform.SetParent(this.transform);
        m_itemInfoPanel.Init();
    }
    private void InitInventoryPanel(StoreModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Store/InventoryPanel") as GameObject;

        m_inventoryPanel = ((GameObject)Instantiate(prefab)).GetComponent<InventoryPanel>();
        m_inventoryPanel.transform.SetParent(this.transform);
        m_inventoryPanel.Init(_model.MaxNumOfSlot);

        m_inventoryPanel.OnSlotClicked += M_inventoryPanel_OnSlotClicked;
    }
    private void InitItemCountPanel(StoreModel model)
    {
        m_itemCountPanel.Init(model);
    }

    // 이벤트 핸들러
    private void M_inventoryPanel_OnSlotClicked(object sender, SlotClickedArgs e)
    {
        OnSlotClicked(sender, e);
    }

}
