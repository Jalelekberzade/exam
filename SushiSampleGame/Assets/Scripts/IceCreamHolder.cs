using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamHolder : MonoBehaviour
{
    [SerializeField] public List<Transform> icecreamList;
    public int IceCreamListCount => icecreamList.Count;
    public static IceCreamHolder Instance { get; private set; }

    private void Awake()
    {
        Instance ??= this;
    }

    /// <summary>
    /// Dont use it in foreach
    /// </summary>
    /// <param name="removeTransform"></param>
    public void RemoveIceCreamFromList(Transform removeTransform)
    {
        if (icecreamList.FindIndex(x => x == removeTransform) != -1)
        {
            icecreamList.Remove(removeTransform);
        }
    }
}
