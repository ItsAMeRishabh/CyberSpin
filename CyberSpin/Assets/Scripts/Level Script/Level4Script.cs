using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Script : MonoBehaviour
{
    public static Level4Script instanceLvl4;

    [SerializeField] private GameObject EndJumpPadParticles;
    [SerializeField] private GameObject JumpPadGameObject;

    [SerializeField] private GameObject forceField;

    [SerializeField] private GameObject forceFieldParticlesUp;

    public bool forceFieldOn;

    public float forceFieldCountdown;

    private void Awake()
    {
        instanceLvl4 = this;
    }

    private void Start()
    {
        EndJumpPadParticles.SetActive(false);
        JumpPadGameObject.GetComponent<JumpPad>().currentBounce = 0f;

        forceField.GetComponent<ForceFieldScript>().magnitude = 2f;
        forceFieldParticlesUp.SetActive(true);

        forceFieldOn = false;

        forceFieldCountdown = 0f;
    }

    private void Update()
    {
        if(ButtonScript.instanceButtonScript.isActivated)
        {
            EndJumpPadParticles.SetActive(true);
            JumpPadGameObject.GetComponent<JumpPad>().currentBounce = 100f;
        }
        else
        {
            JumpPadGameObject.GetComponent<JumpPad>().currentBounce = 0f;
            EndJumpPadParticles.SetActive(false);
        }

        if(forceFieldOn)
        {
            if(forceFieldCountdown > 0f)
            {
                forceField.GetComponent<ForceFieldScript>().magnitude = 0f;
                forceFieldCountdown -= Time.deltaTime;
                forceFieldParticlesUp.SetActive(false);
            }

            else
            {
                forceFieldOn = false;
                forceFieldCountdown = 0f;
            }
        }
        else
        {
            forceField.GetComponent<ForceFieldScript>().magnitude = 2f;
            forceFieldParticlesUp.SetActive(true);
        }
    }
}
