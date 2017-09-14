using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IItemSelectPanel
{
    event EventHandler OnItemSelectButtonClicked;
}

public class ItemSelectPanel : MonoBehaviour , IItemSelectPanel{

    [SerializeField] private ItemSelectButton m_itemSelectButton;
    [SerializeField] private TestItemInfoPanel m_itemInfoPanel;
    
    public event EventHandler OnItemSelectButtonClicked;

    public void Init()
    {
        GameObject prefab = Resources.Load("PlayScene/Common/ItemInfoPanel") as GameObject;
        m_itemInfoPanel = ((GameObject)Instantiate(prefab)).GetComponent<TestItemInfoPanel>();
        m_itemInfoPanel.transform.SetParent(this.transform);
        m_itemInfoPanel.Init();

        m_itemSelectButton.Init();
        m_itemSelectButton.OnItemSelectButtonClicked += M_itemSelectButton_OnItemSelectButtonClicked;
    }

    public void ShowSelectedItem(ItemData itemData)
    {
        m_itemInfoPanel.Show(itemData);
    }

    public void Hide()
    {
        m_itemInfoPanel.Hide();        
    }
    /// 이벤트 핸들러

    private void M_itemSelectButton_OnItemSelectButtonClicked(object sender, System.EventArgs e)
    {
        OnItemSelectButtonClicked(this, EventArgs.Empty);
    }    
}
