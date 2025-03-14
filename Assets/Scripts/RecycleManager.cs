using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecycleManager : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text paperScore;
    public TMP_Text glassScore;
    public TMP_Text metalScore;
    public TMP_Text plasticScore;

    public GameObject barrier;
    public Dictionary<string, bool> maxItemCheck = new Dictionary<string, bool>() {
        {"Paper", false},
        {"Glass", false},
        {"Metal", false},
        {"Plastic", false},
    };


    public void UpdateScore(string scoreText, int score, int maxScore){
        if(scoreText == "Paper"){
            paperScore.text = ItemCheck(score, maxScore) + " / " + maxScore;
        }
        else if(scoreText == "Glass"){
            glassScore.text = ItemCheck(score, maxScore) + " / " + maxScore;
        }
        else if(scoreText == "Metal"){
            metalScore.text = ItemCheck(score, maxScore) + " / " + maxScore;
        }
        else if(scoreText == "Plastic"){
            plasticScore.text = ItemCheck(score, maxScore) + " / " + maxScore;
        }
    }

    private int ItemCheck(int itemNum, int itemMax){
        int checkedScore;
        if(itemNum >= itemMax){
            checkedScore = itemMax;
        } 
        else {
            checkedScore = itemNum;
        }

        return checkedScore;
    }

    public void AllItemSorted(){
        int sortedItems = 0;
        foreach(bool complete in maxItemCheck.Values){
            if(complete){
                sortedItems++;
            }
        }
        Debug.Log(maxItemCheck.Values);
        Debug.Log(sortedItems);

        if(sortedItems == 4){
            Debug.Log("completed");
            KeyAvailable();
        }
    }

    private void KeyAvailable(){
        barrier.SetActive(false);
        heading.text = "Recycle Complete - Key Released";
    }
}
