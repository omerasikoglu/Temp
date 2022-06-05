using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CogaltBeni : MonoBehaviour
{
    private Action<CogaltBeni> killAction;

    public void Init(Action<CogaltBeni> killAction)
    {
        this.killAction = killAction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            killAction(this);
        }
    }
}
