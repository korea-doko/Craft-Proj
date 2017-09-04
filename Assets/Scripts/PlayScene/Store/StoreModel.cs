using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreModel: IModel
{

}

[System.Serializable]
public class StoreModel : MonoBehaviour, IStoreModel
{
    [SerializeField] private int m_maxNumOfSlot;
    [SerializeField] private List<SlotData> m_slotDataList;
    [SerializeField] private int m_currentNumOfStoredItem;
    

   
    public List<SlotData> SlotDataList
    {
        get
        {
            return m_slotDataList;
        }
    }
    public int MaxNumOfSlot
    {
        get
        {
            return m_maxNumOfSlot;
        }
    }
    public int CurrentNumOfStoredItem
    {
        get
        {
            return m_currentNumOfStoredItem;
        }        
    }

    public void InitModel()
    {
    
        m_maxNumOfSlot = 40;
        m_currentNumOfStoredItem = 0;

        m_slotDataList = new List<SlotData>();

        for (int i = 0; i < m_maxNumOfSlot; i++)
        {
            SlotData data = new SlotData(i);
            m_slotDataList.Add(data);
        }
    }

    internal void AddItemData(ItemData data)
    {

        for (int i = 0; i < m_maxNumOfSlot; i++)
        {
            SlotData slotData = m_slotDataList[i];

            if (slotData.IsInit)
                continue;

            slotData.ItemData = data;
            m_currentNumOfStoredItem++;
            return;
        }

        Debug.Log("Item Slot is full");
    }

    internal SlotData GetSlotData(int id)
    {
        return m_slotDataList[id];
    }
}
