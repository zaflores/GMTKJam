using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barFill : MonoBehaviour {

    public Slider revoltBar;

    //initial maximum for revolt bar
    public float maxBar;

    //how much meter fills up per match 3 at end of turn
    public float threePenalty;

    //how many threes were on the starting board
    private int initialThrees;

    //how much we add to the slider max per initial three;
    public float threesBuffer;

    private revolt revoltManger;
    public void setInitialThrees(int threes)
    {
        initialThrees = threes;
    }
    //call at end of every turn to fill revolt bar
    public void updateBar(int threesOnBoard)
    {
        revoltBar.value += threePenalty*threesOnBoard;
        if (revoltBar.value >= revoltBar.maxValue)
        {
            revoltBar.value = revoltBar.maxValue;
            revoltManger.startRiot();
        }
    }

    public void resetBar(int initialThrees)
    {
        revoltManger = GetComponent<revolt>();
        revoltBar.value = 0.0f;
        setInitialThrees(initialThrees);
        revoltBar.maxValue = maxBar + threesBuffer;
    }
}
