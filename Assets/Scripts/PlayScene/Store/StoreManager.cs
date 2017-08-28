using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IStoreManager  : IManager, ILoadable
{

}

public class StoreManager : MonoBehaviour,IStoreManager
{

    private StoreModel m_model;
    private StoreView m_view;
    private static StoreManager m_inst;
    public static StoreManager Inst
    {
        get
        {
            return m_inst;
        }
    }

    public List<SlotData> GetSlotDataList { get { return m_model.SlotDataList; } }

    public StoreManager()
    {
        m_inst = this;
    }
    public void InitManager()
    {
        m_model = Utils.MakeGameObjectWithComponent<StoreModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<StoreView>(this.gameObject);
        m_view.InitView(m_model);

        m_view.OnSlotClicked += M_view_OnSlotClicked;
    }

    private void M_view_OnSlotClicked(object sender, SlotClickedArgs e)
    {
        SlotData slotdata = GetSlotData(e.m_slot);

        if (!slotdata.IsInit)
            return;

        ItemData itemData = slotdata.ItemData;

        m_view.ShowItemInfoPanel(itemData);    
    }
    internal void MenuButtonClicked(MenuName menuName)
    {
        if (menuName == MenuName.Store)
            m_view.Show(m_model);
        else
            m_view.Hide();
    }

    public bool Load()
    {
        return m_view.Load();
    }
    
    public SlotData GetSlotData(Slot _data)
    {
        return GetSlotData(_data.Id);
    }
    public SlotData GetSlotData(int _id)
    {
        SlotData data = m_model.GetSlotData(_id);
        return data;
    }
    public Slot GetSlot(SlotData _data)
    {
        return GetSlot(_data.Id);
    }
    public Slot GetSlot(int _id)
    {
        Slot slot = m_view.GetSlot(_id);
        return slot;
    }

    public int GetNumOfSlotRow()
    {
        return m_model.NumOfSlotRow;
    }
    public int GetNumOfSlotCol()
    {
        return m_model.NumOfSlotCol;
    }

    public void AddItemData(ItemData _data)
    {
        m_model.AddItemData(_data);
    }
}
