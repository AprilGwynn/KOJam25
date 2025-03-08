using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public GameObject EndScreen;
    
    public void EndGame(bool win)
    {
        EndScreen.SetActive(true);
        Time.timeScale = 0.035f;
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
