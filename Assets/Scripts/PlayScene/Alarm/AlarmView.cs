using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlarmView<T> : IView<T> , ILoadable
{

}

public class AlarmView : MonoBehaviour, IAlarmView<AlarmModel>
{

    private AlarmPanel m_alarmPanel;

    public void InitView(AlarmModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Alarm/AlarmPanel") as GameObject;
        m_alarmPanel = ((GameObject)Instantiate(prefab)).GetComponent<AlarmPanel>();
        m_alarmPanel.Init();

    }

    public bool Load()
    {
        MenuCanvas parent = MenuManager.Inst.GetMenuCanvas();
        
        if (parent == null)
            return false;

        m_alarmPanel.transform.SetParent(parent.transform);
        m_alarmPanel.Load();

        return true;
    }

    public void Show(IAlarmTrigger _trigger)
    {
        m_alarmPanel.Show(_trigger);
    }
}
