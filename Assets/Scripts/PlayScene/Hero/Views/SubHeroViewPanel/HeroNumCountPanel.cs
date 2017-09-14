using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroNumCountPanel : MonoBehaviour {

    [SerializeField] private Text m_text;
	
    public void Init()
    {
        m_text.text = "number of ";
    }

    internal void Show(int _cur,int _max)
    {
        m_text.text = _cur.ToString() + " / " + _max.ToString();
    }
}
