using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreView<T> : IView<T>
{
    event EventHandler<SlotClickedArgs> OnSlotClicked;
}

public class StoreView : MonoBehaviour,IStoreView<StoreModel> , ILoadable
{

    [SerializeField] private StoreViewPanel m_storeViewPanel;
  
    public event EventHandler<SlotClickedArgs> OnSlotClicked;

    public void InitView(StoreModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Store/StoreViewPanel") as GameObject;

        m_storeViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<StoreViewPanel>();
        m_storeViewPanel.Init(_model);
        m_storeViewPanel.OnSlotClicked += M_storeViewPanel_OnSlotClicked;
    }
    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Store);

        if (parent == null)
            return false;

        parent.SetGameObjectAsChild(m_storeViewPanel.gameObject);

        return true;
    }

    public void Show(StoreModel _model)
    {
        m_storeViewPanel.Show(_model);
    }
    internal void Hide()
    {
        m_storeViewPanel.Hide();
    }
    public void ShowItemInfoPanel(ItemData _data)
    {
        m_storeViewPanel.ShowItemInfoPanel(_data);
    }

    private void M_storeViewPanel_OnSlotClicked(object sender, SlotClickedArgs e)
    {
        OnSlotClicked(sender, e);
    }
    
    internal Slot GetSlot(int id)
    {
        return m_storeViewPanel.GetSlot(id);
    }

    
}
