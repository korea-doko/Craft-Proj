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

    [SerializeField] private LayoutElement m_layoutEle;
    [SerializeField] private ItemInfoPanel m_itemInfoPanel;

    
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
        m_layoutEle = this.GetComponent<LayoutElement>();
        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener(() => OnSlotClicked(this, EventArgs.Empty));

        GameObject prefab = Resources.Load("PlayScene/Common/ItemInfoPanel") as GameObject;
        m_itemInfoPanel = ((GameObject)Instantiate(prefab)).GetComponent<ItemInfoPanel>();
        m_itemInfoPanel.transform.SetParent(this.transform);
        m_itemInfoPanel.Init();

    }
    public void Show(SlotData _data)
    {
        m_itemInfoPanel.Show(_data.ItemData);
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_itemInfoPanel.Hide();
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
    public void SetHeight(float _height)
    {
        m_layoutEle.preferredHeight = _height;
    } 
}
