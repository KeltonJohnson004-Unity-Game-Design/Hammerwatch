using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTorchFlicker : MonoBehaviour
{
    //private float randomStartTime;
    //private Animator anim;
    //private bool started = false;

    public GameObject flameEffect;
    void Start()
    {

        int shouldBuildInt = Random.Range(0, 10);
        if(shouldBuildInt < 4)
        {
            Destroy(flameEffect);
            Destroy(gameObject);
        }

    }
    private void Update()
    {


    }



}
