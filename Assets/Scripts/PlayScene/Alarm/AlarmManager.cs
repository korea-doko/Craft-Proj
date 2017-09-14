using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAlarmTrigger
{
    string GetAlarmName { get; }
    string GetAlarmDesc { get; }
}
public class AlarmEventArgs:EventArgs
{
    public IAlarmTrigger m_trigger;
    public AlarmEventArgs(IAlarmTrigger _trig)
    {
        m_trigger = _trig;
    }
}

public interface IAlarmManager : IManager, ILoadable
{

}
public class AlarmManager : MonoBehaviour, IAlarmManager
{
    private bool m_isAlarmActived;

    private AlarmModel m_model;
    private AlarmView m_view;

    private static AlarmManager m_inst;
    public static AlarmManager Inst
    {
        get
        {
            return m_inst;
        }
    }
    public AlarmManager()
    {
        m_inst = this;
    }


    public void InitManager()
    {
        m_isAlarmActived = false;

        m_model = Utils.MakeGameObjectWithComponent<AlarmModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<AlarmView>(this.gameObject);
        m_view.InitView(m_model);
    }

    public bool Load()
    {
        return m_view.Load();
    }
    
    public void Alarm(IAlarmTrigger _args)
    {
        m_view.Show(_args);
    }
}
