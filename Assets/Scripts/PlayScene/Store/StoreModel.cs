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
    [SerializeField] private int m_numOfSlotRow;        //세로갯수
    [SerializeField] private int m_numOfSlotCol;        //가로갯수
    [SerializeField] private int m_maxNumOfSlot;

    [SerializeField] private List<SlotData> m_slotDataList;

    


    public int NumOfSlotRow
    {
        get
        {
            return m_numOfSlotRow;
        }        
    }
    public int NumOfSlotCol
    {
        get
        {
            return m_numOfSlotCol;
        }
    }
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

    public void InitModel()
    {
        m_numOfSlotCol = 5;
        m_numOfSlotRow = 9;

        m_maxNumOfSlot = m_numOfSlotCol * m_numOfSlotRow;

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

            return;
        }

        Debug.Log("Item Slot is full");
    }

    internal SlotData GetSlotData(int id)
    {
        return m_slotDataList[id];
    }
}
