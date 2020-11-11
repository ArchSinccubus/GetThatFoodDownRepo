using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Character
{
    Touch T;
    public int modelNum;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsControl)
        {
            ControlCharacter();
        }

        if (IsWaiting)
        {
            StomachControl();
        }


    }

    public override void ControlCharacter()
    {
        if (Input.touchCount > 0)
        {
            T = Input.touches[0];

            if (T.phase == TouchPhase.Began)
            {
                Ray touchRay = Camera.main.ScreenPointToRay(T.position);

                RaycastHit hit;

                if (Physics.Raycast(touchRay, out hit))
                {

                    if (GameController.instance.Tutorial.activeSelf)
                    {
                        GameController.instance.Tutorial.SetActive(false);
                    }

                    if (hit.transform.tag.Contains("Dish") || hit.transform.tag == "Drink")
                    {
                        Food F;
                        if (hit.transform.tag != "Drink")
                        {

                            F = Foods.FindLast(o => o.tag == hit.transform.tag);

                            if (F != null)
                            {
                                StartCoroutine(Consume(F));

                            }
                        }
                        else
                        {
                            F = Drinks.Find(o => o.tag == hit.transform.tag);

                            if (F.PieceAmount > 0)
                            {
                                StartCoroutine(Consume(F));
                            }
                        }


                    }

                    //Debug.Log("touching object name=" + hit.collider.name);

                }


                //Debug.Log(T.position);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 test = Input.mousePosition;

            Ray touchRay = Camera.main.ScreenPointToRay(test);

            RaycastHit hit;

            if (Physics.Raycast(touchRay, out hit))
            {

                if (GameController.instance.Tutorial.activeSelf)
                {
                    GameController.instance.Tutorial.SetActive(false);
                }

                if (hit.transform.tag.Contains("Dish") || hit.transform.tag == "Drink")
                {
                    Food F;
                    if (hit.transform.tag != "Drink")
                    {

                        F = Foods.FindLast(o => o.tag == hit.transform.tag);

                        if (F != null)
                        {
                            StartCoroutine(Consume(F));

                        }
                    }
                    else
                    {
                        F = Drinks.Find(o => o.tag == hit.transform.tag);

                        if (F.PieceAmount > 0)
                        {
                            StartCoroutine(Consume(F));
                        }
                    }


                }

                //Debug.Log("touching object name=" + hit.collider.name);

            }
        }
    }

    protected override void StomachControl()
    {
        StomachCounter += Time.deltaTime;

        if (StomachCounter > WaitBeforeEmptyTime)
        {
            reductionIntervals += Time.deltaTime;
            if (reductionIntervals > 1f)
            {
                StomachCapacity -= StomachEmptyRate;
                UImanager.FillStomachBar(StomachCapacity / MaxStomachCapacity);
                reductionIntervals = 0;
            }
        }
    }

    public override void FillStomach(Food F)
    {
        base.FillStomach(F);

        UImanager.FillStomachBar(StomachCapacity / MaxStomachCapacity);
    }


}
