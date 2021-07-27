using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour {

    public float temperature, co2, timeStir;
    public Text blockText, sensorText, bubbleText;
    public Animator animator;

    string[] texts = new string[] { "<size=38><i>Step 1</i></size>\n\nPlace the sensor in the pan.", "<size=38><i>Step 2</i></size>\n\nHeat up the olive oil and add the salt. Meanwhile, also chop the potatoes and onion in thin slices.",
    "<size=38><i>Step 3</i></size>\n\nAdd the potatoes and onion to the pan. Meanwhile, beat the eggs in a bowl with a fork.", "<size=38><i>Step 4</i></size>\n\nAdd the eggs.",
    "<size=38><i>Step 5</i></size>\n\nWith the help of a big dish, cover the pan and flip it upside down. \nCAREFUL, this is the most difficult step! Practise the technique if you are not sure about it.",
    "<size=38><i>Step 6</i></size>\n\nFinally, it is ready. Remove the omelette from the pan and enjoy your Spanish meal!\n\n¡Que aproveche!"};
    float tMin = 23f, tMax = 26f, co2Max = 0.36f, timeStirMax = 72f;
    int i;

    public void AssignText()
    {
        blockText.text = texts[i];
        i++;
    }

    public IEnumerator ShowBlocks()
    {
        i = 0;
        AssignText();

        //while (temperature < 30f)
        //    yield return null;
        yield return new WaitForSeconds(4f);

        animator.SetTrigger("Go");

        while (temperature < 26f)
            yield return null;

        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Go");
        yield return new WaitForSeconds(4f);

        animator.SetTrigger("Go");
        yield return new WaitForSeconds(2.5f);

        animator.SetTrigger("Go");
        yield return new WaitForSeconds(7f);

        animator.SetTrigger("Go");
        yield return new WaitForSeconds(6f);

        animator.SetTrigger("Spain");
    }

    void Update ()
    {
        string temperatureS, coS, timeS, bubbleS = "";

        //Temperature
        if (temperature < tMin)
        {
            temperatureS = "<color=red>" + temperature + "</color>";
            bubbleS += "Hey your potatoes are going to freeze!\nTry to rise temperature to at least " + tMin + " ºC :D";
        }
        else if (temperature > tMax)
        {
            temperatureS = "<color=red>" + temperature + "</color>";
            bubbleS += "Hey you! Are you trying to burn those potatoes?\nTemperature should be " + tMax + " ºC at most :0";
        }
        else
            temperatureS = "" + temperature;

        //CO2
        if (co2 > co2Max)
        {
            coS = "<color=red>" + co2 + "</color>";
            bubbleS += "The omelette is overcooking!\nKeep an eye on it... ";
        }
        else
            coS = "" + co2;

        //Movement
        if (timeStir > timeStirMax)
        {
            if (timeStir < 60)
                timeS = "<color=red>" + timeStir + " </color>";
            else
                timeS = "<color=red>1m" + (timeStir - 60) + "</color>";
            bubbleS += "It's been too long since you last stirred the potatoes.\nDon't you like them anymore? :(";
        }
        else
        {
            if (timeStir < 60)
                timeS = timeStir + " ";
            else
                timeS = "1m" + (timeStir - 60);
        }
        
        if (bubbleS == "")
        {
            bubbleText.GetComponentInParent<Image>().color = new Vector4(0.62f, 1f, 0.69f, 0.78f);
            bubbleText.text = "You are doing great!";
        }
        else
        {
            bubbleText.GetComponentInParent<Image>().color = new Vector4(1f, 0.62f, 0.62f, 0.78f);
            bubbleText.text = bubbleS;
        }

        sensorText.text = "Temperature: " + temperatureS + " ºC\nCO<size=14>2</size> levels: " + coS + " %\nLast stirred " + timeS + "s ago";
	}
}
