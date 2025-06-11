using System;
using UnityEngine;

[Flags]
public enum CamouflageType
{
    None    = 0,
    Blue    = 1<<0,
    Red     = 1<<1,
    Yellow  = 1<<2,
}

public class Camouflaged : MonoBehaviour
{
    [SerializeField] public CamouflageType Type;
    bool visible = false;
    [SerializeField] Renderer renderer;



    void Awake()
    {
        Hide();
    }

    public void Reveal()
    {
        visible = true;
        renderer.enabled = true;
    }

    public void Hide()
    {
        visible = false;
        renderer.enabled = false;
    }
}