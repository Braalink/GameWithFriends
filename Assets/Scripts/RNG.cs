using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rx;

public class RNG : MonoBehaviour{
    public void Rng(){
        Debug.Log(Rx.Libs.RNG(0, 100));
    }
}
