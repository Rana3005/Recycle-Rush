using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider player){
        GameManager.Instance.checkEndGame();
    }
}
