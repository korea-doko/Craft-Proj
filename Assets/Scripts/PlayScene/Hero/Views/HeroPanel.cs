using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : MonoBehaviour {

    [SerializeField] private bool m_isHidden;
    [SerializeField] private LayoutElement m_layoutEle;

    public bool IsHidden
    {
        get
        {
            return m_isHidden;
        }

        set
        {
            m_isHidden = value;
        }
    }

    public void Init()
    {
        m_layoutEle = this.GetComponent<LayoutElement>();
        Hide();
    }
    public void Load(float _width, float _height)
    {
        m_layoutEle.preferredWidth = _width;
        m_layoutEle.preferredHeight = _height;
    }
    public void Hide()
    {
        m_isHidden = true;
        this.gameObject.SetActive(false);
    }
    public void Show(HeroData data)
    {
        m_isHidden = false;
        this.gameObject.SetActive(true);
    }

}
