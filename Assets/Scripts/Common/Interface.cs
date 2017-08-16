using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel
{
    void InitModel();
}
public interface IView<T>
{
    void InitView(T _model);
}
public interface IUpdatable
{
    void UpdateThis();
}
public interface IManager
{
    void InitManager();
}
public interface ILoadable
{ 
    bool Load();
}