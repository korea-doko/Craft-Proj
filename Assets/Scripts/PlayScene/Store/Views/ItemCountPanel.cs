using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountPanel : MonoBehaviour {

    [SerializeField] private Text m_text;	

    internal void Init(StoreModel _model)
    {
        m_text.text = "0 / 0";
    }

    public void Show(StoreModel model)
    {
        m_text.text = model.CurrentNumOfStoredItem.ToString() + " / " + model.MaxNumOfSlot.ToString();
    }
}
