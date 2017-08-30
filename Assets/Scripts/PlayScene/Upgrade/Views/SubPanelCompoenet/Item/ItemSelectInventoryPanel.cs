using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectSlotArgs : EventArgs
{
    public int m_slotID;

    public ItemSelectSlotArgs(int _slotid)
    {
        m_slotID = _slotid;
    }
}

public interface IItemSelectInventoryPanel
{
    event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
}

public class ItemSelectInventoryPanel :MonoBehaviour ,IItemSelectInventoryPanel,ILoadable{

    [SerializeField] private RectTransform m_rect;
    [SerializeField] private GridLayoutGroup m_gridLayout;
    [SerializeField] private bool m_isActive;
    [SerializeField] private List<ItemSelectSlot> m_itemSelectSlotList;
    [SerializeField] private int m_numOfSlotRow;
    [SerializeField] private int m_numOfSlotCol;
    [SerializeField] private int m_numOfSlot;
    [SerializeField] private bool m_isCellSizeInit;

    public event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;

    internal void Init()
    {
        m_rect = this.GetComponent<RectTransform>();
        m_gridLayout = this.GetComponent<GridLayoutGroup>();
        m_itemSelectSlotList = new List<ItemSelectSlot>();
        m_isCellSizeInit = false;

        Hide();
    }

    public void Show(List<SlotData> _dataList)
    {
        if (!m_isCellSizeInit)
            InitCellSize();

        foreach (ItemSelectSlot slot in m_itemSelectSlotList)
            slot.Hide();

        for(int i = 0; i < m_numOfSlot; i++)
        {
            SlotData data = _dataList[i];

            if (!data.IsInit)
                continue;

            ItemSelectSlot selectSlot = m_itemSelectSlotList[i];
            selectSlot.Show(data);         
        }
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {      
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
    public void InitCellSize()
    {
        float cellWidth = m_rect.rect.width / (float)m_numOfSlotCol;
        float cellHeight = m_rect.rect.height / (float)m_numOfSlotRow;

        m_gridLayout.cellSize = new Vector2(cellWidth, cellHeight);

        m_isCellSizeInit = true;
    }
    public bool Load()
    {
        if (m_itemSelectSlotList.Count != 0)
            return true;

        m_numOfSlotCol = StoreManager.Inst.GetNumOfSlotCol();
        m_numOfSlotRow = StoreManager.Inst.GetNumOfSlotRow();

        m_numOfSlot = m_numOfSlotRow * m_numOfSlotCol;

        
        GameObject prefab = Resources.Load("PlayScene/Upgrade/ItemSelectSlot") as GameObject;

        for(int i = 0; i < m_numOfSlot; i++)
        {
            ItemSelectSlot slot = ((GameObject)Instantiate(prefab)).GetComponent<ItemSelectSlot>();
            slot.transform.SetParent(this.transform);
            slot.Init(i);
            slot.OnItemSelectSlotClicked += Slot_OnItemSelectSlotClicked;
            m_itemSelectSlotList.Add(slot);

        }

        return false;
    }

    private void Slot_OnItemSelectSlotClicked(object sender, EventArgs e)
    {
        ItemSelectSlot selectedSlot = (ItemSelectSlot)sender;

        OnItemSelectSlotClicked(this, new ItemSelectSlotArgs(selectedSlot.Id));        
    }

    
}
