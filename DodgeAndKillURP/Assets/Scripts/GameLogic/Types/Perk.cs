using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour
{
    public string perkName;
    public float duration;
    public float baseCoolDown;

    public abstract void Action();
}
