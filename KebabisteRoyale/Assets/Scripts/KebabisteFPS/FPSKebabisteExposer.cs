using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class FPSKebabisteExposer : MonoBehaviour
{
    [SerializeField]
    public Camera camera;
    [SerializeField]
    public Rigidbody rigidbody;

    [SerializeField]
    public Slider lifeSlider;

    public FPSKebabiste kebabiste;
    [SerializeField] 
    public NavMeshAgent agent;
}
