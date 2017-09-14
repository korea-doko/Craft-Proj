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


    public event EventHandler OnItemSelectButtonClicked;

    public void Init()
    {
        m_rect = this.GetComponent<RectTransform>();
        
        m_itemSelectPanel = this.GetComponentInChildren<ItemSelectPanel>();
        m_itemSelectPanel.Init();
        m_itemSelectPanel.OnItemSelectButtonClicked += M_itemSelectPanel_OnItemSelectButtonClicked;        
    }

   
    public void ShowSelectedItem(ItemData itemData)
    {
        m_itemSelectPanel.ShowSelectedItem(itemData);
    }

    public void Hide()
    {
        m_itemSelectPanel.Hide();
    }

    /// 이벤트 핸들러
    private void M_itemSelectPanel_OnItemSelectButtonClicked(object sender, EventArgs e)
    {
        OnItemSelectButtonClicked(this, EventArgs.Empty);
    }
   
}
