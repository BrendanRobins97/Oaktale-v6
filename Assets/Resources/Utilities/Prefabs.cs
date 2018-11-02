using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs Instance;
    public GameObject pickup;
    public GameObject mouse;
    public GameObject itemIcon;

    void Awake()
    {
        GameManager.Set(this);
        Instance = this;
    }
}
