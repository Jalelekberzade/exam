using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPriceHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _tagPrice;

    [SerializeField] private TMP_Text _EndingPrice;
    private int _sum;

    private void OnEnable()
    {
        EventHolder.Instance.OnPriceChange += StartCalculation;
    }

    private void StartCalculation()
    {
        foreach (var skate in IceCreamHolder.Instance.icecreamList)
        {
            if (skate.TryGetComponent<PriceModel>(out var price))
            {
                _sum += price.Price;
            }
        }
        _tagPrice.text = _sum.ToString();
        _EndingPrice.text = _sum.ToString();
        _sum = 0;
    }

    public int GetMoney()
    {
        return int.Parse(_tagPrice.text);
    }
}
