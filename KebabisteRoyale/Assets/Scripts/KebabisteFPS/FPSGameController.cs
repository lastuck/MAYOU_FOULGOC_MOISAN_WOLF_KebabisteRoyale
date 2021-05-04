using System.Collections;
using System.Collections.Generic;
using Games.Global.Weapons;
using UnityEngine;
using UnityEngine.UI;

public class FPSGameController : MonoBehaviour
{
    public static FPSKebabiste kebabiste1;
    public static FPSKebabiste kebabiste2;
    
    [SerializeField] private bool keb1isAI;
    [SerializeField] private bool keb2isAI;

    [SerializeField] private Slider kebabiste1Life;
    [SerializeField] private Slider kebabiste2Life;
    [SerializeField] private FPSPlayerInputs playerInputs;

    [SerializeField] private Transform keb1Spawn;
    [SerializeField] private Transform keb2Spawn;

    [SerializeField] private GameObject kebabistePrefab;
    [SerializeField] private ObjectPooler objectPooler;

    private bool gameRunning = true;
    void Start()
    {
        kebabiste1 = new FPSPlayerKebabiste();
        kebabiste2 = new FPSPlayerKebabiste();
        
        kebabiste1.opponent = kebabiste2;
        kebabiste2.opponent = kebabiste1;

        kebabiste1.kebabistePrefab = Instantiate(kebabistePrefab, keb1Spawn.position+Vector3.up, Quaternion.identity);
        kebabiste2.kebabistePrefab = Instantiate(kebabistePrefab, keb2Spawn.position+Vector3.up, Quaternion.identity);
        
        kebabiste1.SetupPlayer();
        kebabiste1.exposer.id = 0;
        kebabiste2.SetupPlayer();
        kebabiste2.exposer.id = 1;
        
        if (kebabiste1 is FPSPlayerKebabiste player1)
        {
            player1.playerInputs = playerInputs;
            if (keb1isAI)
            {
                player1.kebabistePrefab.GetComponent<FPSKebabisteExposer>().camera.enabled = false;
            }
        }
        if (kebabiste2 is FPSPlayerKebabiste player2)
        {
            player2.playerInputs = playerInputs;
            if (keb2isAI)
            {
                player2.kebabistePrefab.GetComponent<FPSKebabisteExposer>().camera.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            PlayActions(kebabiste1);
            PlayActions(kebabiste2);
            
            if (kebabiste1.life <= 0)
            {
                Debug.Log("kebabiste2 a gagné");
                gameRunning = false;
            }
            if (kebabiste2.life <= 0)
            {
                Debug.Log("kebabiste1 a gagné");
                gameRunning = false;
            }
        }
    }

    public void PlayActions(FPSKebabiste kebabiste)
    {
        float speed = 20.0f;
        if (kebabiste is FPSPlayerKebabiste player)
        {
            player.RotateView();
        }
        
        FPSKebabiste.Action kebabisteIntent = kebabiste.GetIntent();
        
        switch (kebabisteIntent)
        {
            case FPSKebabiste.Action.GoUp:
                //kebabiste.kebabistePrefab.transform.position+=Vector3.forward*Time.deltaTime;
                kebabiste.wantToGoDown = false;
                kebabiste.wantToGoUp = true;
                break;
            case FPSKebabiste.Action.GoDown:
                kebabiste.wantToGoUp = false;
                kebabiste.wantToGoDown = true;
                break;
            case FPSKebabiste.Action.GoRight:
                kebabiste.wantToGoLeft = false;
                kebabiste.wantToGoRight = true;
                break;
            case FPSKebabiste.Action.GoLeft:
                kebabiste.wantToGoRight = false;
                kebabiste.wantToGoLeft = true;
                break;
            case FPSKebabiste.Action.StopUp:
                //kebabiste.kebabistePrefab.transform.position+=Vector3.forward*Time.deltaTime;
                kebabiste.wantToGoUp = false;
                break;
            case FPSKebabiste.Action.StopDown:
                kebabiste.wantToGoDown = false;
                break;
            case FPSKebabiste.Action.StopRight:
                kebabiste.wantToGoRight = false;
                break;
            case FPSKebabiste.Action.StopLeft:
                kebabiste.wantToGoLeft = false;
                break;
            case FPSKebabiste.Action.Shoot:
                Shoot(kebabiste);
                break;
            case FPSKebabiste.Action.Hide:
                break;
            case FPSKebabiste.Action.Reload:
                break;
            case FPSKebabiste.Action.None:
            default:
                break;
        }

        Vector3 wantedMovement = Vector3.zero;
        if (kebabiste.wantToGoUp)
        {
            wantedMovement+=speed*Time.deltaTime*kebabiste.kebabistePrefab.transform.forward;
        }
        if (kebabiste.wantToGoDown)
        {
            wantedMovement-=speed*Time.deltaTime*kebabiste.kebabistePrefab.transform.forward;
        }
        if (kebabiste.wantToGoLeft)
        {
            wantedMovement-=speed*Time.deltaTime*kebabiste.kebabistePrefab.transform.right;
        }
        if (kebabiste.wantToGoRight)
        {
            wantedMovement+=speed*Time.deltaTime*kebabiste.kebabistePrefab.transform.right;
        }

        if (wantedMovement != Vector3.zero)
        {
            kebabiste.KebabisteRigidbody.AddForce(wantedMovement, ForceMode.Impulse);
        }
        else
        {
            kebabiste.KebabisteRigidbody.velocity = Vector3.zero;
        }
    }

    private void Shoot(FPSKebabiste kebabiste)
    {
        if (kebabiste.ammo > 0)
        {
            kebabiste.ammo -= 1;
            GameObject proj = objectPooler.GetPooledObject(0);
            proj.SetActive(true);
            proj.transform.position =
                kebabiste.kebabisteCam.transform.forward + kebabiste.kebabisteCam.transform.position;
            proj.GetComponent<Rigidbody>().AddForce(kebabiste.kebabisteCam.transform.forward*30,ForceMode.Impulse);
        }
    }
}
