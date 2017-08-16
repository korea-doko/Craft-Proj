using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemView<T> : IView<T>
{

}

public class ItemView : MonoBehaviour, IItemView<ItemModel>
{
    public void InitView(ItemModel _model)
    {

    } 
}
