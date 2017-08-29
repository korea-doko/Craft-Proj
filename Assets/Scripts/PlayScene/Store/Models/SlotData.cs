using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData
{
    [SerializeField] int m_id;
    [SerializeField] bool m_isInit;
    [SerializeField] ItemData m_itemData;

    public SlotData(int _id)
    {
        m_id = _id;
        m_isInit = false;
    }
    public int Id
    {
        get
        {
            return m_id;
        }
    }
    public ItemData ItemData
    {
        get
        {
            return m_itemData;
        }

        set
        {
            m_itemData = value;

            if (m_itemData == null)
                m_isInit = false;
            else
                m_isInit = true;
        }
    }
    public bool IsInit
    {
        get
        {
            return m_isInit;
        }
    }

    public void RemoveItemData()
    {
        m_itemData = null;
        m_isInit = false;
    }
}
