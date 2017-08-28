using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IItemSelectSlot
{
    event EventHandler OnItemSelectSlotClicked;
}

public class ItemSelectSlot : MonoBehaviour , IItemSelectSlot{

    [SerializeField] private int m_id;
    [SerializeField] private Color m_emptyColor;
    [SerializeField] private Color m_fullColor;
    [SerializeField] private SlotData m_slotData;
    [SerializeField] private Image m_image;
    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_btn;

    public event EventHandler OnItemSelectSlotClicked;

    public int Id
    {
        get
        {
            return m_id;
        }
    }
    public bool IsActive
    {
        get
        {
            return m_isActive;
        }        
    }

    internal void Init(int _id)
    {
        m_image = this.GetComponent<Image>();
        m_id = _id;
        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener( ()=> OnItemSelectSlotClicked(this, EventArgs.Empty));
        Hide();
    }

    public void Show(SlotData _slotData)
    {
        m_slotData = _slotData;
        m_image.color = m_fullColor;

        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;

        m_image.color = m_emptyColor;
        this.gameObject.SetActive(m_isActive);
    }
}
