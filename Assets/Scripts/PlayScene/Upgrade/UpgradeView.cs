using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUpgradeView<T>: IView<UpgradeModel>, ILoadable,IUpdatable
{
    event EventHandler OnItemSelectButtonClicked;
    event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;
}
public class UpgradeView : MonoBehaviour,IUpgradeView<UpgradeModel>{

    private UpgradeViewPanel m_upgradeViewPanel;

    public event EventHandler OnItemSelectButtonClicked;
    public event EventHandler<ItemSelectSlotArgs> OnItemSelectSlotClicked;
    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    public event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;

    public void InitView(UpgradeModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Upgrade/UpgradeViewPanel") as GameObject;

        m_upgradeViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<UpgradeViewPanel>();
        m_upgradeViewPanel.Init();
        m_upgradeViewPanel.OnItemSelectButtonClicked += M_upgradeViewPanel_OnItemSelectButtonClicked;
        m_upgradeViewPanel.OnItemSelectSlotClicked += M_upgradeViewPanel_OnItemSelectSlotClicked;
        m_upgradeViewPanel.OnRuneButtonClicked += M_upgradeViewPanel_OnRuneButtonClicked;
        m_upgradeViewPanel.OnRuneButtonLongPressed += M_upgradeViewPanel_OnRuneButtonLongPressed;
    }

   

    public void ShowItemSelectInventoryPanel(List<SlotData> _dataList)
    {
        m_upgradeViewPanel.ShowItemSelectInventoryPanel(_dataList);
    }

    public void Show(int[] _playerOwnedRunes)
    {
        m_upgradeViewPanel.Show(_playerOwnedRunes);
    }
    public void Hide()
    {
        m_upgradeViewPanel.Hide();
    }
    public void HideItemSelectInventoryPanel()
    {
        m_upgradeViewPanel.HideItemSelectInventoryPanel();
    }
    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Upgrade);

        if (parent == null)
            return false;
        parent.SetGameObjectAsChild(m_upgradeViewPanel.gameObject);

        bool isLoadDone = m_upgradeViewPanel.Load();

        return isLoadDone;
    }
    public void UpdateThis()
    {
        m_upgradeViewPanel.UpdateThis();
    }

    public void ShowSelectedItem(ItemData itemData)
    {
        m_upgradeViewPanel.ShowSelectedItem(itemData);
    }
    

    // 이벤트 핸들러
    private void M_upgradeViewPanel_OnItemSelectButtonClicked(object sender, EventArgs e)
    {
        OnItemSelectButtonClicked(this, EventArgs.Empty);
    }
    private void M_upgradeViewPanel_OnRuneButtonClicked(object sender, RuneButtonClickArgs e)
    {
        OnRuneButtonClicked(this, e);
    }
    private void M_upgradeViewPanel_OnItemSelectSlotClicked(object sender, ItemSelectSlotArgs e)
    {
        OnItemSelectSlotClicked(this, e);
    }
    private void M_upgradeViewPanel_OnRuneButtonLongPressed(object sender, RuneButtonLongPressedArgs e)
    {
        OnRuneButtonLongPressed(this, e);
    }

}
