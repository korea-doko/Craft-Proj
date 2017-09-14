using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDownPanel : MonoBehaviour {

    [SerializeField] private Text m_text;
    
    internal void Init()
    {

    }

    internal void Show(float _limitedTime)
    {
        m_text.text = "남은 시간 \n " + _limitedTime.ToString();
    }
}
