using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Lazers;
    [SerializeField] private GameObject[] Cristals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shotLazers()
    {
        foreach (GameObject cristal in Cristals)
        {
            cristal.GetComponent<CrystalScript>().resetLazerNum();
        }

        foreach (GameObject lazer in Lazers)
        {
            lazer.GetComponent<LightStartPoint>().shotLazer();
        }

        foreach(GameObject cristal in Cristals)
        {
            if(cristal.GetComponent<CrystalScript>().checkBreakLazer())
            {
                Destroy(cristal);
            }
        }
    }
}
