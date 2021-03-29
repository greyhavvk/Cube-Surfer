using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedCubeController : MonoBehaviour
{
#pragma warning disable 0649

    [Header("General Variables")]
    public CubeStates cubeState;

    [Header("References")]
    [SerializeField] private GameObject backCheckCollObject;

#pragma warning restore 0649

    private Vector3 defScale;
    public enum CubeStates
    {
        OnHold,
        InParent
    }

    private void Start()
    {
        defScale = transform.localScale;
    }

    public void TriggerCollected()
    {
        if (cubeState == CubeStates.OnHold)
        {
            AudioController.audioController.PlayCubeCollectSFX();
            cubeState = CubeStates.InParent;
            Destroy(backCheckCollObject);

            if (DOTween.IsTweening("CollectibleNormalCubeScaleTween"))
            {
                DOTween.Kill("CollectibleNormalCubeScaleTween");
            }

            transform.localScale = defScale;
        }
    }
}
