using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour
{
    [SerializeField] private FPSGameController gameController;
    private Coroutine Reloading;
    private bool isReloading;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isReloading)
        {
            Reloading = StartCoroutine(ReloadAmmo(other.GetComponent<FPSKebabisteExposer>().kebabiste));
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isReloading)
        {
            StopCoroutine(Reloading);
            isReloading = false;
        }
    }

    private IEnumerator ReloadAmmo(FPSKebabiste kebabiste)
    {
        isReloading = true;
        while (isReloading)
        {
            int cpt = 0;
            while (cpt<5)
            {
                yield return new WaitForSeconds(1.0f);
                cpt++;
            }
        
            kebabiste.ammo = 10;
        }
    }
}
