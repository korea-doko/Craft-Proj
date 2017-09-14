using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemInventorySlotClickedArgs : EventArgs
{
    public int m_id;

    public EquipItemInventorySlotClickedArgs(int _id)
    {
        m_id = _id;
    }
}

public interface IEquipItemInventorySlot
{
    event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;
}

public class EquipItemInventorySlot : MonoBehaviour ,IEquipItemInventorySlot{

    [SerializeField] private bool m_isActive;
    [SerializeField] private Text m_text;
    [SerializeField] private Button m_btn;
    [SerializeField] private int m_id;

    public event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;

    public bool IsActive
    {
        get
        {
            return m_isActive;
        }

        set
        {
            m_isActive = value;
        }
    }

    internal void Init(int _id)
    {
        m_btn = this.GetComponent<Button>();
        m_id = _id;
        m_btn.onClick.AddListener(() => OnEquipItemInventorySlotClicked(this, new EquipItemInventorySlotClickedArgs(m_id)));
        m_text.text = "";
        Hide();
    }

    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
    public void Show(SlotData _data)
    {
        m_text.text = _data.ItemData.GetItemInfo();

        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
}
