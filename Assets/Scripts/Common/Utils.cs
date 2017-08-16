using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Utils
{
    public static T MakeGameObjectWithComponent<T>(GameObject _parent = null) where T : Component
    {
        GameObject temp = new GameObject();

        if (_parent != null)
            temp.transform.SetParent(_parent.transform);

        temp.AddComponent<T>();

        T component = temp.GetComponent<T>();

        temp.name = component.GetType().ToString();

        return temp.GetComponent<T>();
    }
    public static GameObject MakeGameObjectWithType(Type _type, GameObject _parent = null)
    {
        GameObject temp = new GameObject();

        if (_parent != null)
            temp.transform.SetParent(_parent.transform);

        temp.AddComponent(_type);

        temp.name = _type.ToString();

        return temp;
    }
    
}
