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
            Reloading = StartCoroutine(ReloadAmmo(other.GetComponent<FPSKebabisteExposer>().id));
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

    private IEnumerator ReloadAmmo(int id)
    {
        Debug.Log("start reloading");
        isReloading = true;
        int cpt = 0;
        while (cpt<5)
        {
            yield return new WaitForSeconds(1.0f);
            cpt++;
        }
        if (id == 0)
        {
            FPSGameController.kebabiste1.ammo = 10;
        } else
        {
            FPSGameController.kebabiste2.ammo = 10;
        }
        isReloading = false;
        Debug.Log("reloading done");
    }
}
