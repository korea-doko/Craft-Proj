using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotClickedArgs : EventArgs
{
    public Slot m_slot;

    public SlotClickedArgs(Slot _slot)
    {
        m_slot = _slot;
    }

}

public interface IInventoryPanel
{
    event EventHandler<SlotClickedArgs> OnSlotClicked;
}

public class InventoryPanel : MonoBehaviour ,IInventoryPanel{

    [SerializeField] private RectTransform m_rect;
    [SerializeField] private GridLayoutGroup m_gridLayout;
    //[SerializeField] private Slot[][] m_slotAry;
    [SerializeField] private List<Slot> m_slotList;

    [SerializeField] private float m_cellWidth;
    [SerializeField] private float m_cellHeight;

    [SerializeField] private bool m_isCellSizeInit;

    [SerializeField] private int m_numOfSlotRow;
    [SerializeField] private int m_numOfSlotCol;
    [SerializeField] private int m_maxNumOfSlot;

    public event EventHandler<SlotClickedArgs> OnSlotClicked;

    public void Init(int _numOfSlotRow, int _numOfSlotCol)
    {
        m_rect = this.GetComponent<RectTransform>();

        m_rect.anchorMin = new Vector2(0.0f, 0.0f);
        m_rect.anchorMax = new Vector2(1.0f, 1.0f);

        m_rect.offsetMax = Vector2.zero;
        m_rect.offsetMin = Vector2.zero;

        m_gridLayout = this.GetComponent<GridLayoutGroup>();

        m_numOfSlotRow = _numOfSlotRow;
        m_numOfSlotCol = _numOfSlotCol;
        m_maxNumOfSlot = m_numOfSlotCol * m_numOfSlotRow;

        InitSlot();

    }

    internal Slot GetSlot(int id)
    {
        return m_slotList[id];
    }

    public void Show(StoreModel _model)
    {
        if (!m_isCellSizeInit)
            InitCellSize();

        
        for(int i = 0; i < m_maxNumOfSlot;i++)
        {
            Slot slot = m_slotList[i];
            SlotData data = _model.SlotDataList[i];            
            slot.Show(data);
        }
    }


    private void Slot_OnSlotClicked(object sender, EventArgs e)
    {
        Slot slot = (Slot)sender;

        OnSlotClicked(this, new SlotClickedArgs(slot));
    }

    void InitSlot()
    {
        GameObject prefab = Resources.Load("PlayScene/Store/Slot") as GameObject;

        m_slotList = new List<Slot>();

        for(int i = 0; i < m_maxNumOfSlot;i++)
        {
            Slot slot = ((GameObject)Instantiate(prefab)).GetComponent<Slot>();
            slot.transform.SetParent(this.transform);
            slot.Init(i);
            m_slotList.Add(slot);
            slot.OnSlotClicked += Slot_OnSlotClicked;
        }
    }
    void InitCellSize()
    {
        m_cellWidth = m_rect.rect.width / (float)m_numOfSlotCol;
        m_cellHeight = m_rect.rect.height / (float)m_numOfSlotRow;

        m_gridLayout.cellSize = new Vector2(m_cellWidth, m_cellHeight);

        if (m_cellWidth != 0.0f && m_cellHeight != 0.0f)
            m_isCellSizeInit = true;
        else
            m_isCellSizeInit = false;
    }


}

