using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    public GameStateController GameStateController;
    public int DebugWinThreshold = 5;
    public TMP_Text ringValueHUD;
    public Animator ringHudAnimator;
    // public GameObject WinHUD;
    private int _currRings = 0;

    public void AddRings(int rings)
    {
        _currRings += rings;
        ringHudAnimator.SetTrigger("GetRing");
        CheckForWin();
        UpdateRingsHud();
    }

    public void TakeRings(int rings)
    {
        _currRings -= rings;
        ringHudAnimator.SetTrigger("LoseRing");
        UpdateRingsHud();
    }

    private void CheckForWin()
    {
        if (_currRings >= DebugWinThreshold)
        {
            GameStateController.EndGame(true);
        }
    } 
    public void UpdateRingsHud()
    {
        ringValueHUD.text = _currRings.ToString();
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
