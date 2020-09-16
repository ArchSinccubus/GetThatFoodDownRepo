using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ModelManager : MonoBehaviour
{
    public Character character;

    public GameObject pukeModel;

    public Transform PukeLoc, RFoodLoc, LFoodLoc, MFoodLoc;

    private GameObject puke;

    public List<Texture> Textures;
    public Material MainMaterial, vomitMaterial;

    public ParticleSystem Crumbs, PukeParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Puke()
    {
        
        puke = Instantiate(pukeModel);

        puke.GetComponent<VomitController>().M = vomitMaterial;

        puke.transform.Find("Puke_Object").gameObject.GetComponent<SkinnedMeshRenderer>().material = vomitMaterial;

        puke.transform.SetParent(PukeLoc, true);

        puke.transform.localPosition = Vector3.zero;

        puke.transform.SetParent(null);

        puke.transform.rotation = Quaternion.identity;

        puke.transform.position = new Vector3(puke.transform.position.x, puke.transform.Find("Puke_Object").gameObject.GetComponent<SkinnedMeshRenderer>().bounds.size.y, puke.transform.position.z);

        PukeParticles.Play();

    }

    public void removePuke()
    {
        if (character is Enemy)
        {
            character.CharCam.GetComponent<Animator>().SetTrigger("Back");
        }

        //Destroy(puke.gameObject);
        puke.GetComponent<Animator>().SetTrigger("Splash");

        PukeParticles.Stop();

    }

    public void grabFood(string Direction)
    {
        character.grabFood = true;

        switch (Direction)
        {
            case "Left":
                character.pickupTransform = LFoodLoc;
                break;

            case "Right":
                character.pickupTransform = RFoodLoc;
                break;

            case "Middle":
                character.pickupTransform = MFoodLoc;
                break;
            default:
                break;
        }
    }

    public void returnDrink()
    {
        character.returnDrink = true;

    }

    public void changeMaterial(string s)
    {
        try
        {
            MainMaterial.mainTexture = Textures.Find(o => o.name == s);

        }
        catch
        {

            throw;
        }

    }

    public void PlayCrumbs()
    {
        Crumbs.Play();
    
    }
}
