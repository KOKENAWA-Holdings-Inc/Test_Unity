using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public TextMeshProUGUI start;
    public TextMeshProUGUI option;
    public GameObject optionpool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void optiontrigger() 
    {
        start.enabled = false;
        option.enabled = false;
        
    }
}
