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
    [SerializeField] private SlotData m_slotData;
    [SerializeField] private Image m_image;
    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_btn;
    [SerializeField] private LayoutElement m_layoutEle;
    [SerializeField] private ItemInfoPanel m_itemInfoPanel;

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
    public void Init(int _id)
    {
        m_id = _id;

        m_image = this.GetComponent<Image>();
        m_btn = this.GetComponent<Button>();
        m_layoutEle = this.GetComponent<LayoutElement>();
        m_btn.onClick.AddListener( ()=> OnItemSelectSlotClicked(this, EventArgs.Empty));

        GameObject prefab = Resources.Load("PlayScene/Common/ItemInfoPanel") as GameObject;
        m_itemInfoPanel = ((GameObject)Instantiate(prefab)).GetComponent<ItemInfoPanel>();
        m_itemInfoPanel.transform.SetParent(this.transform);
        m_itemInfoPanel.Init();

        Hide();
    }

    public void Show(SlotData _slotData)
    {
        m_slotData = _slotData;
        m_itemInfoPanel.Show(_slotData.ItemData);
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        m_itemInfoPanel.Hide();
        this.gameObject.SetActive(m_isActive);
    }
    public void SetHeight(float _height)
    {
        m_layoutEle.preferredHeight = _height;
    }
}
