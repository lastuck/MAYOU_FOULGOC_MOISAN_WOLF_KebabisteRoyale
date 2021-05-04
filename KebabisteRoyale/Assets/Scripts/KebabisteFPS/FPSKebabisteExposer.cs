using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public int id;
}
