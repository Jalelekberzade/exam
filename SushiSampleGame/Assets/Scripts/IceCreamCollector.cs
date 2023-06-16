using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCollector : MonoBehaviour
{
    [SerializeField] private float lerpDuration;
    [SerializeField] private float stackOffset;
    private Sequence _sequence;
    public bool IsFinished = true;
    void OnEnable()
    {
        EventHolder.Instance.OnIceCreamCollided += CollectIceCream;
    }

    private void FixedUpdate()
    {
        if (IsFinished)
            StackFollow();
    }

    private void CollectIceCream(Transform icecream)
    {
        icecream.tag = "Collected";
        IceCreamHolder.Instance.icecreamList.Add(icecream);
        icecream.gameObject.AddComponent<CollectedIceCream>();

        if (!icecream.gameObject.TryGetComponent<Rigidbody>(out Rigidbody addedRigidbody))
        {
            addedRigidbody = icecream.gameObject.AddComponent<Rigidbody>();
        }

        addedRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        _sequence = DOTween.Sequence();
        _sequence.Kill();
        _sequence = DOTween.Sequence();
        for (int i = IceCreamHolder.Instance.IceCreamListCount - 1; i > 0; i--)
        {
            _sequence.Join(IceCreamHolder.Instance.icecreamList[i].DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(IceCreamHolder.Instance.icecreamList[i].DOScale(1f, 0.1f));
        }
    }

    private void StackFollow()
    {
        float lerpSpeed = lerpDuration;
        for (int i = 1; i < IceCreamHolder.Instance.IceCreamListCount; i++)
        {
            Vector3 previousPos = IceCreamHolder.Instance.icecreamList[i - 1].transform.position +
            Vector3.forward * stackOffset;
            Vector3 currentPos = IceCreamHolder.Instance.icecreamList[i].transform.position;
            IceCreamHolder.Instance.icecreamList[i].transform.position =
            Vector3.Lerp(currentPos, previousPos, lerpSpeed * Time.fixedDeltaTime);
            lerpSpeed += lerpDuration * Time.fixedDeltaTime;
        }
    }
}