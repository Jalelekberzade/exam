using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHolder : MonoBehaviour
{
    private static EventHolder _instance;

    public static EventHolder Instance
    {
        get
        {
            _instance = FindObjectOfType<EventHolder>();
            return _instance;
        }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Action OnFinishCollider;
    public Action<Transform> OnIceCreamCollided;
    public Action<Transform> OnObstacleCollided;
    public Action<Transform> OnSellIceCream;

    public Action OnPriceChange;

    public void PriceChangeEvent()
    {
        OnPriceChange?.Invoke();
    }

    public void IceCreamCollided(Transform icecream)
    {
        OnIceCreamCollided?.Invoke(icecream);
    }

    public void ObstacleCollided(Transform icecream)
    {
        OnObstacleCollided?.Invoke(icecream);
    }

    public void SellIceCreamCollider(Transform icecream)
    {
        OnSellIceCream?.Invoke(icecream);
    }

    public void FinishCollider()
    {
        OnFinishCollider?.Invoke();
    }
}