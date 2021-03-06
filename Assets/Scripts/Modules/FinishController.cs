using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
#pragma warning disable 0649
    [HideInInspector] public int currentMultiplierZone = 0;

    [Header("Variables")]
    [SerializeField] private FinishFloorNames _finishFloorName;
#pragma warning restore 0649

    public enum FinishFloorNames
    {
        x1,
        x2,
        x3,
        x4,
        x5,
        x6,
        x7,
        x8,
        x9,
        x10,
        FinishFlag
    }

    private void Start()
    {
        InitValue();

    }

    void InitValue()
    {
        switch (_finishFloorName)
        {
            case FinishFloorNames.x1:
                currentMultiplierZone = 1;
                break;
            case FinishFloorNames.x2:
                currentMultiplierZone = 2;
                break;
            case FinishFloorNames.x3:
                currentMultiplierZone = 3;
                break;
            case FinishFloorNames.x4:
                currentMultiplierZone = 4;
                break;
            case FinishFloorNames.x5:
                currentMultiplierZone = 5;
                break;
            case FinishFloorNames.x6:
                currentMultiplierZone = 6;
                break;
            case FinishFloorNames.x7:
                currentMultiplierZone = 7;
                break;
            case FinishFloorNames.x8:
                currentMultiplierZone = 8;
                break;
            case FinishFloorNames.x9:
                currentMultiplierZone = 9;
                break;
            case FinishFloorNames.x10:
                currentMultiplierZone = 10;
                break;
            case FinishFloorNames.FinishFlag:
                currentMultiplierZone = 10;
                break;
            default:
                break;
        }
    }
}