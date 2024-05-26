using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRoidAnim : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetState(int state)
    {
        anim.SetInteger("state", state);
    }
}
