using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float PowerOfShake;
    public float DurationOfShake;
    public float DurationMultiplier;
    public Transform CameraToShake;
    public bool CameraShakeNow;

    Vector3 CameraStartPos;
    float InitialDurationOfShake;

    private void Start()
    {
        //CameraToShake = Camera.main.transform;
        CameraStartPos = CameraToShake.localPosition;
        InitialDurationOfShake = DurationOfShake;
    }

    void Update()
    {
        if (CameraShakeNow)
        {
            if(DurationOfShake > 0)
            {
                CameraToShake.localPosition = CameraStartPos + Random.insideUnitSphere * PowerOfShake;
                DurationOfShake -= Time.deltaTime * DurationMultiplier;
            }
            else
            {
                CameraShakeNow = false;
                DurationOfShake = InitialDurationOfShake;
                CameraToShake.localPosition = CameraStartPos;
            }
        }
    }

    public void ILikeToMoveItMoveIt()
    {
        CameraShakeNow = true;
    }

    public void disableCamera()
    {
        CameraToShake.gameObject.SetActive(false);
    }

    public void startTheFuckingGame()
    {
        GameController.instance.Levels.unloadLevel();
        GameController.instance.startGame();
    }
}
