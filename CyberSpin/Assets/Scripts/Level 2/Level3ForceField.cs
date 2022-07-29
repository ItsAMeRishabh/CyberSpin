using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3ForceField : MonoBehaviour
{
    [SerializeField] private GameObject forceField;

    [SerializeField] private GameObject forceFieldParticlesDown;
    [SerializeField] private GameObject forceFieldParticlesUp;

    private void Start()
    {
        forceField.GetComponent<ForceFieldScript>().magnitude = 1f;

        forceFieldParticlesDown.SetActive(true);
        forceFieldParticlesUp.SetActive(false);
    }

    private void Update()
    {
        if (ButtonScript.instanceButtonScript.isActivated)
        {
            forceField.GetComponent<ForceFieldScript>().magnitude = 2f;

            forceFieldParticlesDown.SetActive(false);
            forceFieldParticlesUp.SetActive(true);
        }
        else
        {
            forceField.GetComponent<ForceFieldScript>().magnitude = -1f;

            forceFieldParticlesDown.SetActive(true);
            forceFieldParticlesUp.SetActive(false);
        }

    }
}
