using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class MoneyManager : MonoBehaviour
{

    [SerializeField] TMP_Text moneyDisplaay;

    public float money = 0;
    
    public float genMK1Production = 1;
    public float genMK1Level =1;
    public bool isGenMK1Bought = true;

    public float genMK2Production = 0;
    public float genMK2Level=0;
    public bool isGenMK2Bought = false;

    public float genMK3Production = 0;
    public float genMK3Level=0;
    public bool isGenMk3Bought = false;

    public float rebirthMultiplier = 1;



    [SerializeField] BoxCollider2D generatorMK1Upgrade;
    [SerializeField] BoxCollider2D generatorMK2Upgrade;
    [SerializeField] BoxCollider2D generatorMK3Upgrade;

    [SerializeField] BoxCollider2D rebirthButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(moneyGeneration());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision == generatorMK1Upgrade)
        {
            isGenMK1Bought=true;
            genMK1Level += 1;
            genMK1Production = genMK1Production * 2;
          
        }
        if (collision == generatorMK2Upgrade)
        {
            isGenMK2Bought=true;
            genMK2Level += 1;
            genMK2Production = genMK2Production * 4;
        }
        if (collision == generatorMK3Upgrade)
        {
            isGenMk3Bought=true;
            genMK3Level+= 1;
            genMK3Production = genMK3Production * 8;
        }

        if (collision == rebirthButton)
        {

        }

    }



    private IEnumerator moneyGeneration()
    {
        if (isGenMK1Bought == false)
        {
            genMK1Production = 0;
        }
        if (isGenMK2Bought == false)
        {
            genMK2Production = 0;
        }
        if (isGenMK1Bought == false)
        {
            genMK3Production = 0;
        }

        yield return new WaitForSeconds(1);
        money = money + (rebirthMultiplier *(genMK1Production + genMK2Production + genMK3Production));
        moneyDisplaay.text = $"{money}$";
        StartCoroutine(moneyGeneration());
    }
}
