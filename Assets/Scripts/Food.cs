using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public enum FoodTypes { Food, Drink }


public class Food : MonoBehaviour
{
    public MeshFilter Filter;
    public MeshRenderer Render;
    public Rigidbody Rigidbody;
    public int PieceAmount;
    public float fillAmount;
    public float eatTime;
    public FoodTypes type;

    public bool vanishWhenDone;

    public MeshCollider col;

    public Vector3 foodRot;

    public void FillProperties(FoodProperties props)
    {
        transform.rotation = Quaternion.identity;

        name = props.Name;
        Filter.mesh = props.M;
        Render.materials = new Material[0];
        Render.material = props.FM;
        PieceAmount = props.pieceAmount;
        fillAmount = props.fillAmount;
        eatTime = props.eatTime;
        type = props.type;

        foodRot = props.FoodRotation;

       // Rigidbody = gameObject.AddComponent<Rigidbody>();
        col.sharedMesh = props.M;

    }

    public void FillProperties(DrinkProperties props)
    {
        name = props.Name;
        Filter.mesh = props.FullM;

        Material[] mats = new Material[2];
        mats[0] = props.CupFM;
        mats[1] = props.FullFM;

        Render.materials = mats;

        PieceAmount = props.pieceAmount;
        fillAmount = props.fillAmount;
        eatTime = props.eatTime;
        type = props.type;

//        Rigidbody = gameObject.AddComponent<Rigidbody>();
        col.convex = true;
        col.sharedMesh = props.FullM;

    }

    public void changeDrinkState(DrinkProperties props, int state)
    {
        Material[] mats = new Material[2];


        mats[0] = props.CupFM;

        switch (state)
        {
            case 5:
            case 4:
            case 3:
            case 2:
                Filter.mesh = props.FullM;
                mats[1] = props.FullFM;
                break;
            case 1:
                mats[1] = props.CupFM;
                Filter.mesh = props.HalfFullM;
                mats[0] = props.HalfFullFM;
                break;
            case 0:
                Filter.mesh = props.EmptyM;
                mats[1] = props.EmptyFM;
                break;
            default:
                break;
        }
        Render.materials = mats;
    }

}
