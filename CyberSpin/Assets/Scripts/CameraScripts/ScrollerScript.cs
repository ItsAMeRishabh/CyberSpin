using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ScrollerScript : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    [SerializeField] private RawImage factoryBackground;
    [SerializeField] private Vector2 parallaxEffectMul;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        Vector2 myVector = new Vector3(deltaMovement.x * parallaxEffectMul.x, deltaMovement.y * parallaxEffectMul.y);

        //transform.position += myVector;

        factoryBackground.uvRect = new Rect(factoryBackground.uvRect.position + myVector * Time.deltaTime, factoryBackground.uvRect.size);
        lastCameraPosition = cameraTransform.position;
    }
}
