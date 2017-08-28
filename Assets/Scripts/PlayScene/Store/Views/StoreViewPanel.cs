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


    public event EventHandler<SlotClickedArgs> OnSlotClicked;

    public void Init(StoreModel _model)
    {
        InitInventoryPanel(_model.NumOfSlotRow,_model.NumOfSlotCol);
        InitInfoPanel(_model);

    }
  
    private void M_inventoryPanel_OnSlotClicked(object sender, SlotClickedArgs e)
    {
        OnSlotClicked(sender, e);
    }

    public void Show(StoreModel _model)
    {
        m_inventoryPanel.Show(_model);
    }

    internal void Hide()
    {
        m_itemInfoPanel.Hide();
    }
    internal Slot GetSlot(int id)
    {
        return m_inventoryPanel.GetSlot(id);
    }
    void InitInventoryPanel(int _numOfSlotRow, int _numOfSlotCol)
    {
        GameObject prefab = Resources.Load("PlayScene/Store/InventoryPanel") as GameObject;

        m_inventoryPanel = ((GameObject)Instantiate(prefab)).GetComponent<InventoryPanel>();
        m_inventoryPanel.transform.SetParent(this.transform);
        m_inventoryPanel.Init(_numOfSlotRow, _numOfSlotCol);
        m_inventoryPanel.OnSlotClicked += M_inventoryPanel_OnSlotClicked;
    }

    internal void ShowItemInfoPanel(ItemData data)
    {
        m_itemInfoPanel.ShowItemInfoPanel(data);

    }

    void InitInfoPanel(StoreModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Store/ItemInfoPanel") as GameObject;

        m_itemInfoPanel = ((GameObject)Instantiate(prefab)).GetComponent<ItemInfoPanel>();
        m_itemInfoPanel.transform.SetParent(this.transform);
        m_itemInfoPanel.Init();
    }

}
