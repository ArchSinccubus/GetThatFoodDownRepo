using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public MeshFilter Filter;
    public MeshRenderer Renderer;

    public List<Food> Foods, Drinks;

    protected float reductionIntervals;
    protected float StomachCounter;
    public float WaitBeforeEmptyTime;
    public float StomachEmptyRate;

    public bool grabFood, returnDrink;
    public ModelManager MM;
    public Transform pickupTransform;

    public Dictionary<string, AnimationClip> AnimationDictionary;
    public Animation anim;

    [SerializeField]
    private float stomachCapacity;
    public float MaxStomachCapacity;

    [SerializeField]
    public bool IsControl, IsVomiting, IsWaiting;

    public bool cancelAnim;

    public Camera CharCam;

    public float StomachCapacity { get => stomachCapacity; set 
        {
            stomachCapacity = value;
            if (stomachCapacity >= MaxStomachCapacity)
            {
                stomachCapacity = MaxStomachCapacity;
                ToggleControl(false);

                StartCoroutine(Vomit());
            }
            else if(stomachCapacity < 0)
            {
                stomachCapacity = 0;
            }
            //Debug.Log(stomachCapacity);
        }

    }

    public void init()
    {
        reductionIntervals = 0;
        StomachCounter = 0;
        IsControl = true;
        IsWaiting = true;
        grabFood = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    protected virtual void StomachControl()
    {
        StomachCounter += Time.deltaTime;

        if (StomachCounter > WaitBeforeEmptyTime)
        {
            reductionIntervals += Time.deltaTime;
            if (reductionIntervals > 1f)
            {
                StomachCapacity -= StomachEmptyRate;
                reductionIntervals = 0;
            }
        }
    }

    public void DressModel(int modelNum)
    {
        if (this is Enemy)
        {
        if (modelNum == 4)
        {
            transform.SetParent(GameController.instance.PonyLoc);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(GameController.instance.NomralELoc);
            transform.localPosition = Vector3.zero;
        }
        }



        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        
        Model model = GameController.getModel(modelNum);

        GameObject mod = Instantiate(model.test);

        anim = mod.AddComponent<Animation>();

        mod.transform.SetParent(transform, false);


        foreach (var item in model.clips)
        {
            item.legacy = true;
            anim.AddClip(item, item.name);
            //AnimationDictionary.Add(item.name, item);
        }

        mod.GetComponent<ModelManager>().character = this;
        this.MM = mod.GetComponent<ModelManager>();
    }

    public void playAnimation(string S, float speed, bool lockControl)
    {
        anim.Stop();

        anim[S].speed = speed;

        anim.Play(S);

        if (lockControl)
        {
            StartCoroutine(playAnimationCurontine());
        }

    }

    public IEnumerator playAnimationCurontine()
    {
        ToggleControl(false);

        do
        {
            yield return null;
        } while (anim.isPlaying);

        ToggleControl(true);
        cancelAnim = true;

    }

    public IEnumerator Consume(Food F)
    {
        StomachCounter = 0;
        IsWaiting = false;
        ToggleControl(false);
        switch (F.tag)
        {
            case "FirstDish":
                playAnimation("L_Eat_ANIM", 1 / F.eatTime, false);
                break;
            case "MainDish":
                playAnimation("M_Eat_ANIM", 1 / F.eatTime, false);
                break;
            case "Drink":
                playAnimation("Drink_ANIM", 1 / F.eatTime, false);
                break;
            default:
                break;
        }
        //playAnimation("Eat", 1 / F.eatTime);

        do
        {
            yield return null;

            if (grabFood)
            {
                grabFood = false;

                //F.Rigidbody.isKinematic = true;
                F.transform.SetParent(pickupTransform);
                F.transform.localPosition = Vector3.zero;

                if (F.type != FoodTypes.Drink)
                {
                    F.transform.localRotation = Quaternion.Euler(F.foodRot);
                }
            }
            if (returnDrink)
            {
                returnDrink = false;
                returnDrinkToPlace(F);
            }


        } while (anim.isPlaying);


        FillStomach(F);
        if (!IsVomiting)
        {
            playAnimation("Idle_ANIM", 1, false);
            IsWaiting = true;
            ToggleControl(cancelAnim);
        }

    }

    public virtual void FillStomach(Food F)
    {
        F.PieceAmount--;
        if (F.type == FoodTypes.Drink && F.PieceAmount >= 0)
        {
            F.changeDrinkState(GameController.instance.DrinkDetails, F.PieceAmount);
        }
        else if (F.type == FoodTypes.Food && F.PieceAmount <= 0)
        {
             GameController.returnFoodToPool(F, F.type == FoodTypes.Drink ? Drinks : Foods);
        }

        StomachCapacity += F.fillAmount;
    }

    public IEnumerator Vomit()
    {
        IsVomiting = true;
        ToggleControl(false);


        playAnimation("Puke_ANIM", (0.83333333f), false);

        if (this is Enemy)
        {
            CharCam.GetComponent<Animator>().SetTrigger("Forward");

            //CharCam.GetComponent<Animation>()["Puke_Cam"].speed = 0.425f;
            //CharCam.GetComponent<Animation>().Play();
        }

        do
        {
            yield return null;
        } while (anim.isPlaying);

        StomachCapacity = 0;

        ToggleControl(cancelAnim);
        if (this.tag == "Player")
        {
            UImanager.FillStomachBar(0);
        }
        playAnimation("Idle_ANIM", 1, false);
        IsWaiting = true;
        IsVomiting = false;

    }

    public virtual void ControlCharacter()
    { }

    public void ToggleControl(bool toggle)
    {
        IsControl = toggle;
    }

    public void returnDrinkToPlace(Food F)
    {
        if (this is Player)
        { 
            F.transform.SetParent(GameController.instance.Levels.PlayerTableLoc.Find("Drink"), false);
        }
        else if (this is Enemy)
        {
            F.transform.SetParent(GameController.instance.Levels.EnemyTableLoc.Find("Drink"), false);

        }

        F.transform.localPosition = Vector3.zero;
        F.transform.rotation = Quaternion.identity;
    }

}
