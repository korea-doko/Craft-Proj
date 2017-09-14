using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUpgradeModel : IModel
{

}

public class UpgradeModel : MonoBehaviour , IUpgradeModel{

    private ItemData m_selectedItemData;
    private bool m_isSelectedItemExist;

    public ItemData SelectedItemData
    {
        get
        {
            return m_selectedItemData;
        }

        set
        {
            m_selectedItemData = value;

            if (m_selectedItemData == null)
                m_isSelectedItemExist = false;
            else
                m_isSelectedItemExist = true;
        }
    }

    public bool IsSelectedItemExist
    {
        get
        {
            return m_isSelectedItemExist;
        }
    }

    public void InitModel()
    {
        m_selectedItemData = null;
    }    
}
