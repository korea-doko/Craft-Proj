using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public interface ISlot
{
    event EventHandler OnSlotClicked;
}

public class Slot : MonoBehaviour ,ISlot{
    
     

    [SerializeField] private int m_id;
    [SerializeField] private Button m_btn;
    [SerializeField] private bool m_isActive;
    [SerializeField] private Image m_image;

    [SerializeField] private Color m_emptyColor;
    [SerializeField] private Color m_fullColor;
    
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

    public event EventHandler OnSlotClicked;

    public void Init(int _id)
    {
        m_id = _id;
        m_image = this.GetComponent<Image>();

        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener(() => OnSlotClicked(this, EventArgs.Empty));        
    }

    public void Show(SlotData _data)
    {
        if (_data.IsInit)
            SetClickable();
        else
            SetUnclickable();

        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
    

    void SetClickable()
    {
        m_image.color = m_fullColor;
        m_btn.interactable = true;
    }
    void SetUnclickable()
    {
        m_image.color = m_emptyColor;
        m_btn.interactable = false;
    }
}
