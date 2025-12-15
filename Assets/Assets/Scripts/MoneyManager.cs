using UnityEngine;
using TMPro;
using System.Collections;
using System;

[Serializable]
public class Generator //stats so that it is easy to add new generators
{
    public string name;
    public int production;
    public int level;
    public int cost;
    public int costMultiplier;
    public int productionMultiplier;
    public int isBought;
    public TMP_Text genText;
    public GameObject genButton;
}

public class MoneyManager : MonoBehaviour
{
    //texts
    [SerializeField] TMP_Text moneyDisplaay;
    [SerializeField] TMP_Text anmountOfRebirthsText;
    [SerializeField] TMP_Text rebirthCostText;

    public double money = 0;

    public int[] genProductions;

    public Generator[] generators;

    //rebirth stats
    public int rebirthAmount = 0;
    public float rebirthMultiplier = 1;
    public float rebirthCost = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(moneyGeneration());//starts the loop below on start of the game
    }


    private void Update()
    {
        moneyDisplaay.text = $"{money}$"; //updates the money display every frame
    }


    private IEnumerator moneyGeneration()// a loop that generates money every second
    {
        yield return new WaitForSeconds(1);

        money = money + (rebirthMultiplier *
            ((generators[0].production * generators[0].isBought) +
            (generators[1].production * generators[1].isBought) +
            (generators[2].production * generators[2].isBought)));

        StartCoroutine(moneyGeneration());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Generators"))
        {

            GeneratorUpgrade(int.Parse(collision.name));
        }

        if (collision.gameObject.CompareTag("Rebirth"))
        {
            Rebirth();
        }
    }

    //Generator upgrade
    public void GeneratorUpgrade(int generator)//upgrades the generator that is clicked on if you have enough money
    {
        if (money >= generators[generator].cost)
        {
            money -= generators[generator].cost;
            generators[generator].level++;
            generators[generator].production = Mathf.RoundToInt(Mathf.Pow(generators[generator].productionMultiplier, generators[generator].level));
            generators[generator].cost = generators[generator].cost * generators[generator].costMultiplier;
            generators[generator].isBought = 1;

                generators[generator].genText.text = $"{generators[generator].name} Level:{generators[generator].level}\n Produces: {generators[generator].production}$/s\n Cost: {generators[generator].cost}$"; //updates the text on the generators
            }
    }

    public void Rebirth()
    {
        if (money >= rebirthCost)
        {
            rebirthAmount++;
            rebirthMultiplier = rebirthAmount+1;
            rebirthCost = rebirthCost * (rebirthMultiplier + 1);

            //changes the text in the rebirth tab
            anmountOfRebirthsText.text = $"You got {rebirthAmount} rebirths \r\nMultiplying your money by {rebirthMultiplier}x\r\n";
            rebirthCostText.text = $"Rebirth Cost: {rebirthCost}$";


            //resets the generators
            generators[0].level = 0;
            generators[1].level = 0;
            generators[2].level = 0;

            generators[0].production = 1;
            generators[1].production = 0;
            generators[2].production = 0;

            generators[0].cost = 4;
            generators[1].cost = 100;
            generators[2].cost = 500;

            //resets the generator texts
            generators[0].genText.text = $"{generators[0].name} Level:0\n Produces: {generators[0].production}$/s\n Cost: {generators[0].cost}$";
            generators[1].genText.text = $"{generators[1].name} Level:0\n Produces: 0 $/s\n Cost: {generators[1].cost}$";
            generators[2].genText.text = $"{generators[2].name} Level:0\n Produces: 0 $/s\n Cost: {generators[2].cost}$";


            //resets money
            money = 0;
        }


    }


}