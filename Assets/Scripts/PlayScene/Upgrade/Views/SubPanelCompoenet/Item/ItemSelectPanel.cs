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

    public event EventHandler OnItemSelectButtonClicked;

    public void Init()
    {
        m_itemSelectButton = this.GetComponentInChildren<ItemSelectButton>();
        m_itemSelectButton.Init();

        m_itemSelectButton.OnItemSelectButtonClicked += M_itemSelectButton_OnItemSelectButtonClicked;
    }

    private void M_itemSelectButton_OnItemSelectButtonClicked(object sender, System.EventArgs e)
    {
        OnItemSelectButtonClicked(this, EventArgs.Empty);
    }

    internal void ShowSelectedItem(ItemData itemData)
    {
        m_itemSelectButton.ShowSelectedItem(itemData);
    }

    internal void Hide()
    {
        m_itemSelectButton.Hide();
    }
}
