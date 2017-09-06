using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IEquipItemInventoryPanel : ILoadable
{
    event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;
}

public class EquipItemInventoryPanel : MonoBehaviour ,IEquipItemInventoryPanel{

    [SerializeField] private bool m_isActive;
    [SerializeField] private GameObject m_equipItemInventorySlotParent;
    [SerializeField] private Button m_backBtn;
    [SerializeField] private List<EquipItemInventorySlot> m_equipItemInventorySlotList;
    [SerializeField] private int m_numOfSlotPanel;

    public event EventHandler<EquipItemInventorySlotClickedArgs> OnEquipItemInventorySlotClicked;

    public void Init()
    {
        m_backBtn.onClick.AddListener(() => Hide());

        m_equipItemInventorySlotList = new List<EquipItemInventorySlot>();
     
        Hide();
    }

  
    public void Show(List<SlotData> slotDataList)
    {
        HideAllSlots();

        for(int i = 0; i < slotDataList.Count;i++)
        {
            SlotData slotData = slotDataList[i];

            if (!slotData.IsInit)
                continue;

            EquipItemInventorySlot slot = m_equipItemInventorySlotList[i];
            
            slot.Show(slotData);
        }

        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
    public bool Load()
    {
        m_numOfSlotPanel = StoreManager.Inst.Model.MaxNumOfSlot;

        GameObject prefab = Resources.Load("PlayScene/Guild/EquipItemInventorySlot") as GameObject;

        for (int i = 0; i < m_numOfSlotPanel; i++)
        {
            EquipItemInventorySlot slot = ((GameObject)Instantiate(prefab)).GetComponent<EquipItemInventorySlot>();

            slot.transform.SetParent(m_equipItemInventorySlotParent.transform);
            slot.Init(i);
            slot.OnEquipItemInventorySlotClicked += Slot_OnEquipItemInventorySlotClicked;
            m_equipItemInventorySlotList.Add(slot);
        }
        return true;
    }

    private void HideAllSlots()
    {
        for (int i = 0; i < m_numOfSlotPanel; i++)
            m_equipItemInventorySlotList[i].Hide();
    }

    // 이벤트 핸들러

    private void Slot_OnEquipItemInventorySlotClicked(object sender, EquipItemInventorySlotClickedArgs e)
    {
        OnEquipItemInventorySlotClicked(this, e);
        Hide();
    }
}
