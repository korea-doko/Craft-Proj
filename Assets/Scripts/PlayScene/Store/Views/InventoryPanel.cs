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
    [SerializeField] private List<Slot> m_slotList;
    
    [SerializeField] private int m_numOfSlotRow;
    [SerializeField] private int m_numOfSlotCol;
    [SerializeField] private int m_maxNumOfSlot;

    [SerializeField] private GameObject m_slotParent;
    [SerializeField] private int m_numOfSeenSlot;
    [SerializeField] private float m_slotPreferHeight;
    [SerializeField] private bool m_isSlotHeightInit;

    public event EventHandler<SlotClickedArgs> OnSlotClicked;

    public void Init(int _maxNumOfSlot)
    {
        m_numOfSeenSlot = 3;
        m_slotPreferHeight = 0.0f;
        m_isSlotHeightInit = false;

        m_rect = this.GetComponent<RectTransform>();

        m_rect.anchorMin = new Vector2(0.0f, 0.0f);
        m_rect.anchorMax = new Vector2(1.0f, 0.8f);

        m_rect.offsetMax = Vector2.zero;
        m_rect.offsetMin = Vector2.zero;
        

        m_maxNumOfSlot = _maxNumOfSlot;

        InitSlot();
    }

    internal Slot GetSlot(int id)
    {
        return m_slotList[id];
    }

    public void Show(StoreModel _model)
    {
        if (!m_isSlotHeightInit)
            InitSlotHeight();

        HideAllSlot();
        
        for(int i = 0; i < m_maxNumOfSlot;i++)
        {
            Slot slot = m_slotList[i];
            SlotData data = _model.SlotDataList[i];

            if (!data.IsInit)
                continue;

            slot.Show(data);
        }
    }
      
    void InitSlot()
    {
        GameObject prefab = Resources.Load("PlayScene/Store/Slot") as GameObject;

        m_slotList = new List<Slot>();

        for(int i = 0; i < m_maxNumOfSlot;i++)
        {
            Slot slot = ((GameObject)Instantiate(prefab)).GetComponent<Slot>();
            slot.transform.SetParent(m_slotParent.transform);
            slot.Init(i);
            m_slotList.Add(slot);
            slot.OnSlotClicked += Slot_OnSlotClicked;
        }
    }
    void InitSlotHeight()
    {

        m_slotPreferHeight = m_rect.rect.size.y / (float)m_numOfSeenSlot;

        if (m_slotPreferHeight == 0.0f)
            m_isSlotHeightInit = false;
        else
        {
            for(int i = 0; i < m_maxNumOfSlot;i++)
            {
                Slot slot = m_slotList[i];
                slot.SetHeight(m_slotPreferHeight);
            }

            m_isSlotHeightInit = true;
        }
    }
    void HideAllSlot()
    {
        for (int i = 0; i < m_maxNumOfSlot; i++)
            m_slotList[i].Hide();
    }
    private void Slot_OnSlotClicked(object sender, EventArgs e)
    {
        Slot slot = (Slot)sender;

        OnSlotClicked(this, new SlotClickedArgs(slot));
    }


}

