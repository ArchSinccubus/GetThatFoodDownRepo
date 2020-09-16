using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitController : MonoBehaviour
{
    public Material M;

    float yTex;

    // Start is called before the first frame update
    void Start()
    {
        yTex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        yTex += 0.01f;
        M.SetTextureOffset("_MainTex", new Vector2(yTex, yTex));
    }

    public void takeOutVomit()
    {
        M.SetTextureOffset("_MainTex", new Vector2(0, 0));

        Destroy(gameObject);
    }
}
