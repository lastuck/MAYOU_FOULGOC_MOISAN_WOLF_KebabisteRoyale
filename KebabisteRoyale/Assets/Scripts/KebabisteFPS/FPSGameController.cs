using System.Collections;
using System.Collections.Generic;
using Games.Global.Weapons;
using KebabisteFPS;
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
    [SerializeField] private Transform[] covers;
    [SerializeField] private Transform[] reloadingZones;

    public static bool gameRunning = true;
    async void Start()
    {
        kebabiste1 = !keb1isAI ? (FPSKebabiste) new FPSPlayerKebabiste() : new FPSAgentKebabiste();
        kebabiste2 = !keb2isAI ? (FPSKebabiste) new FPSPlayerKebabiste() : new FPSAgentKebabiste();
        
        kebabiste1.opponent = kebabiste2;
        kebabiste2.opponent = kebabiste1;

        kebabiste1.kebabistePrefab = Instantiate(kebabistePrefab, keb1Spawn.position+Vector3.up, Quaternion.identity);
        kebabiste2.kebabistePrefab = Instantiate(kebabistePrefab, keb2Spawn.position+Vector3.up, Quaternion.identity);
        
        kebabiste1.SetupPlayer();
        kebabiste1.exposer.kebabiste = kebabiste1;
        kebabiste2.SetupPlayer();
        kebabiste2.exposer.kebabiste = kebabiste2;
        
        if (keb1isAI)
        {
            if (kebabiste1 is FPSAgentKebabiste agent1)
            {
                agent1.ComputeIntent();
            }
            kebabiste2.kebabistePrefab.GetComponent<FPSKebabisteExposer>().camera.enabled = false;
        }
        else
        {
            if (kebabiste1 is FPSPlayerKebabiste player1)
            {
                player1.playerInputs = playerInputs;
            }

            kebabiste1.exposer.agent.enabled = false;
        }

        if (keb2isAI)
        {
            if (kebabiste2 is FPSAgentKebabiste agent2)
            {
                agent2.ComputeIntent();
            }
            kebabiste2.kebabistePrefab.GetComponent<FPSKebabisteExposer>().camera.enabled = false;
        }
        else
        {
            if (kebabiste2 is FPSPlayerKebabiste player2)
            {
                player2.playerInputs = playerInputs;
            }

            kebabiste2.exposer.agent.enabled = false;
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

            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(GoNear(kebabiste2));
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(Hide(kebabiste2));
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Reload(kebabiste2));
            }
        }
    }

    public void PlayActions(FPSKebabiste kebabiste)
    {
        float speed = 20.0f;
        kebabiste.RotateView();

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
                kebabiste.hiding = StartCoroutine(Hide(kebabiste));
                break;
            case FPSKebabiste.Action.Reload:
                kebabiste.goingToreload = StartCoroutine(Reload(kebabiste));
                break;
            case FPSKebabiste.Action.GoNear:
                kebabiste.goingNear = StartCoroutine(GoNear(kebabiste));
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

    private IEnumerator Hide(FPSKebabiste kebabiste)
    {
        if (kebabiste.isGoingNearOpponent)
        {
            if (kebabiste.goingNear != null)
            {
                StopCoroutine(kebabiste.goingNear);
            }
            kebabiste.isGoingNearOpponent = false;
        }
        if (kebabiste.isGoingToReload)
        {
            if (kebabiste.goingToreload != null)
            {
                StopCoroutine(kebabiste.goingToreload);
            }
            kebabiste.isGoingToReload = false;
        }
        if (!kebabiste.isGoingTohide)
        {
            Debug.Log("going");
            kebabiste.isGoingTohide = true;
            Vector3 cover = FindClosestIn(covers, kebabiste.kebabistePrefab.transform.position).position;
            kebabiste.exposer.agent.SetDestination(cover);
            while (Vector3.Distance(kebabiste.kebabistePrefab.transform.position,cover) > 0.5f)
            {
                yield return new WaitForSeconds(0.1f);
            }
            Debug.Log("done");
            kebabiste.isGoingTohide = false;
        }
    }
    
    private IEnumerator GoNear(FPSKebabiste kebabiste)
    {
        if (kebabiste.isGoingTohide)
        {
            if (kebabiste.hiding != null)
            {
                StopCoroutine(kebabiste.hiding);
            }
            kebabiste.isGoingTohide = false;
        }
        if (kebabiste.isGoingToReload)
        {
            if (kebabiste.goingToreload != null)
            {
                StopCoroutine(kebabiste.goingToreload);
            }
            kebabiste.isGoingToReload = false;
        }
        if (!kebabiste.isGoingNearOpponent)
        {
            Debug.Log("going");
            kebabiste.isGoingNearOpponent = true;
            kebabiste.exposer.agent.SetDestination(kebabiste.opponent.kebabistePrefab.transform.position);
            while (Vector3.Distance(kebabiste.kebabistePrefab.transform.position,kebabiste.opponent.kebabistePrefab.transform.position) > 15.0f)
            {
                yield return new WaitForSeconds(0.1f);
            }

            Debug.Log("done");
            kebabiste.exposer.agent.SetDestination(kebabiste.kebabistePrefab.transform.position);
            kebabiste.isGoingNearOpponent = false;
        }
    }
    
    private IEnumerator Reload(FPSKebabiste kebabiste)
    {
        if (kebabiste.isGoingTohide)
        {
            if (kebabiste.hiding != null)
            {
                StopCoroutine(kebabiste.hiding);
            }

            kebabiste.isGoingTohide = false;
        }
        if (kebabiste.isGoingNearOpponent)
        {
            if (kebabiste.goingNear != null)
            {
                StopCoroutine(kebabiste.goingNear);
            }

            kebabiste.isGoingNearOpponent = false;
        }
        if (!kebabiste.isGoingToReload)
        {
            Debug.Log("going");
            kebabiste.isGoingToReload = true;
            Vector3 reloadZone = FindClosestIn(reloadingZones, kebabiste.kebabistePrefab.transform.position).position;
            kebabiste.exposer.agent.SetDestination(reloadZone);
            while (Vector3.Distance(kebabiste.kebabistePrefab.transform.position,reloadZone) > 0.5f)
            {
                yield return new WaitForSeconds(0.1f);
            }
            Debug.Log("done");
            kebabiste.isGoingToReload = false;
        }
    }

    private Transform FindClosestIn(Transform[] transformList, Vector3 position)
    {
        Transform closestTransform = null;
        float minDist = 100000;
        foreach (Transform transform in transformList)
        {
            float dist = Vector3.Distance(position, transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestTransform = transform;
            }
        }

        return closestTransform;
    }
}
