using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField] Image healthbarSlider;
    [SerializeField] FloatValueSO playerHealth;
    [SerializeField] GameObject healthBarObject;
    [SerializeField] GameObject deathBarObject;


    void Start()
    {
        deathBarObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthbarSlider.fillAmount = playerHealth.floatValue / 100f;
    }

}
