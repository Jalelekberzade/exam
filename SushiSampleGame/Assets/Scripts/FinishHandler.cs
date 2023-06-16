using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    [SerializeField] private List<Transform> icecreamPlaces;
    [SerializeField] private GameObject _finalMoney;
    private int _placeCount = 0;
    private IceCreamCollector _icecreamCollector;
    private TagPriceHandler _tagPrice;

    void OnEnable()
    {
        EventHolder.Instance.OnFinishCollider += FinishAnimationHandler;
        _icecreamCollector = FindObjectOfType<IceCreamCollector>();
        _tagPrice = FindObjectOfType<TagPriceHandler>();
    }

    private void FinishAnimationHandler()
    {
        StartCoroutine(StartAnimation());
        _icecreamCollector.IsFinished = false;

    }

    IEnumerator StartAnimation()
    {
        List<Transform> skateList = IceCreamHolder.Instance.icecreamList;
      //  CameraManager.Instance.OpenCamera("FinishCamera");
        int tempCount = skateList.Count;
        for (int i = 1; i < tempCount; i++)
        {
            Transform skate = skateList[i];
            IceCreamHolder.Instance.RemoveIceCreamFromList(icecream);
            skate.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(icecream.GetComponent<Animator>());
            Destroy(icecream.GetComponent<CollectedIceCream>());
            tempCount--;
            i--;
            if (_placeCount == 4)
            {
                icecream.parent = icecreamPlaces[_placeCount];
                icecream.DOLocalRotate(new Vector3(0, 0, 0), 0.25f);
                icecream.DOLocalJump(Vector3.zero, 0.05f, 1, .2f);
            }
            else
            {
                icecream.parent = icecreamPlaces[_placeCount];
                icecream.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
                icecream.DOLocalJump(Vector3.zero, 0.05f, 1, .2f);
                _placeCount++;
            }
            yield return new WaitForSeconds(0.3f);
        }
        _finalMoney.transform.DOMoveY((float)(_finalMoney.transform.position.y + (_tagPrice.GetMoney() * 0.07f)), 1f);
    }
}