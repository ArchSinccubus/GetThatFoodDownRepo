using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Model
{
    public GameObject test;

    public AnimationClip[] clips;
}

[Serializable]
public struct FoodProperties
{
    public string Name;
    public Mesh M;
    public Material FM;
    public int pieceAmount;
    public float fillAmount;
    public float eatTime;
    public FoodTypes type;

    public bool vanishWhenDone;
    public Mesh MDone;
    public Material FMDone;

    public Vector3 FoodRotation;
}

[Serializable]
public struct DrinkProperties
{
    public string Name;
    public Mesh FullM;
    public Material FullFM;
    public Mesh HalfFullM;
    public Material HalfFullFM;
    public Mesh EmptyM;
    public Material EmptyFM;

    public Material CupFM;


    public int pieceAmount;
    public float fillAmount;
    public float eatTime;
    public FoodTypes type;
}

[Serializable]
public struct SceneTextures
{
    public Mesh TableTest;
    public Material TableM;

    public Mesh FirstDishPlate;
    public Material FirstDishPlateM;

    public Mesh MainDishPlate;
    public Material MainDishPlateM;


    public GameObject Room;
    public GameObject Table;
}

[Serializable]
public struct MultiDimensionalInt
{
    public int FoodType;
    public int FoodAmount;
}
