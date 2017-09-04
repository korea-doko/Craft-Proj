using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatus
{
    int GetStatus(ModType _name);        
}
[System.Serializable]
public class Status : IStatus
{
    [SerializeField] private int[] m_value;
    

    public Status()
    {
        int numOfStatusParameter = System.Enum.GetNames(typeof(ModType)).Length;

        m_value = new int[numOfStatusParameter];

        for (int i = 0; i < numOfStatusParameter; i++)
            m_value[i] = 0;
    }

    public int GetStatus(ModType _name)
    {
        return m_value[(int)_name];
    }

    public void ChangeStatusParameterTo(ModType _name, int _value)
    {
        m_value[(int)_name] = _value;
    }

    public void AddStatusParameter(ModType _name,int _value)
    {
        m_value[(int)_name] += _value;
    }
    public void MinusStatusParameter(ModType _name,int _value)
    {
        m_value[(int)_name] -= _value;
    }
    public void AddStatus(Status _status)
    {
        int numOfStats = System.Enum.GetNames(typeof(ModType)).Length;

        for (int i = 0; i < numOfStats; i++)
            m_value[i] = m_value[i] + _status.m_value[i];
    }
    public void MinusStatus(Status _status)
    {
        int numOfStats = System.Enum.GetNames(typeof(ModType)).Length;

        for (int i = 0; i < numOfStats; i++)
            m_value[i] = m_value[i] - _status.m_value[i];
    }

    public string GetStatusInfo()
    {
        int numOfStatus = System.Enum.GetNames(typeof(ModType)).Length;
        string str = "";

        for (int i = 0; i < numOfStatus; i++)
        {
            int value = m_value[i];

            if (value == 0)
                continue;

            str += ((ModType)i).ToString() + " = " + value.ToString() + "\n";
        }

        return str;
    }
}

