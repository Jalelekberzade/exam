using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private void OnEnable()
    {
        EventHolder.Instance.OnObstacleCollided += SplitIceCream;
    }
    private void SplitIceCream(Transform icecream)
    {
        int colliderSkate = IceCreamHolder.Instance.icecreamList.FindIndex(x => x == icecream);
        if (colliderSkate != -1)
        {
            List<Transform> droppedIceCreams = IceCreamHolder.Instance.icecreamList.GetRange(colliderIceCream,
                IceCreamHolder.Instance.IceCreamListCount - colliderIceCream);

            foreach (var droppedIceCream in droppedIceCreams)
            {
                droppedIceCream.tag = "Collectable";
                droppedIceCream.gameObject.layer = 6;
                IceCreamHolder.Instance.icecreamList.Remove(droppedIceCream);
                Destroy(droppedIceCream.GetComponent<Rigidbody>());
                Destroy(droppedIceCream.GetComponent<CollectedIceCream>());
                Vector3 currentPos = droppedIceCream.transform.position;
                droppedIceCream.transform.DOJump(new Vector3(currentPos.x + Random.Range(-1.5f, 1.5f), 0.1f, currentPos.z + Random.Range(3, 5)), 2f, 1, 0.5f);
                DamageNum.Instance.ShowNumberDecrease(droppedIceCream);
            }
        }
    }
}