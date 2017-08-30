using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public interface IBaseItemPanel
{
    event EventHandler OnItemSelectButtonClicked;
}

public class BaseItemPanel : MonoBehaviour, IBaseItemPanel {

    [SerializeField] private RectTransform m_rect;
    [SerializeField] private ItemSelectPanel m_itemSelectPanel;
    [SerializeField] private ItemDescPanel m_itemDescPanel;

    public event EventHandler OnItemSelectButtonClicked;

    public void Init()
    {
        m_rect = this.GetComponent<RectTransform>();

        m_rect.anchorMin = Vector2.zero;
        m_rect.anchorMax = new Vector2(1.0f, 0.8f);

        m_rect.offsetMax = Vector2.zero;
        m_rect.offsetMin = Vector2.zero;

        m_itemSelectPanel = this.GetComponentInChildren<ItemSelectPanel>();
        m_itemSelectPanel.Init();
        m_itemSelectPanel.OnItemSelectButtonClicked += M_itemSelectPanel_OnItemSelectButtonClicked;

        m_itemDescPanel = this.GetComponentInChildren<ItemDescPanel>();
        m_itemDescPanel.Init();
    }

    private void M_itemSelectPanel_OnItemSelectButtonClicked(object sender, EventArgs e)
    {
        OnItemSelectButtonClicked(this, EventArgs.Empty);
    }

    internal void ShowSelectedItem(ItemData itemData)
    {
        m_itemSelectPanel.ShowSelectedItem(itemData);
        m_itemDescPanel.ShowSelectedItem(itemData);
    }

    internal void Hide()
    {
        m_itemDescPanel.Hide();
        m_itemSelectPanel.Hide();
    }
}
