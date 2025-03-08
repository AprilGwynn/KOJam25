using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour
{
    public GameStateController GameStateController;
    public int DebugWinThreshold = 5;
    public TMP_Text ringValueHUD;
    public Animator ringHudAnimator;

    public Slider HPsliderHUD;
    // public GameObject WinHUD;
    private int _currRings = 0;
    private float _currHealth = 50;

    private const float HEALTH_PER_RING = 5;
    private const float DAMAGE_PER_WALL = 15;
    private const int RINGS_LOST_PER_WALL = 3;

    public void AddRings(int rings)
    {
        _currRings += rings;
        ringHudAnimator.SetTrigger("GetRing");
        _currHealth += HEALTH_PER_RING;
        CheckForWinOrDeath();
        UpdateHud();
    }

    public void HitObstacle(Interactable item)
    { // if I was smarter this should probably be a callback..
        if (item.itemType == Interactable.InteractableType.wall)
        {
            TakeRings(RINGS_LOST_PER_WALL);
            _currHealth -= DAMAGE_PER_WALL;
            UpdateHud();
        }
    }

    public void TakeRings(int rings)
    {
        int takenRings = Mathf.Min(rings, _currRings); // to prevent negative rings
        _currRings -= takenRings; 
        ringHudAnimator.SetTrigger("LoseRing");
        CheckForWinOrDeath();
        UpdateHud();

        DropRings(takenRings);
    }

    private void DropRings(int takenRings)
    {
        throw new NotImplementedException();
    }
    private void CheckForWinOrDeath()
    {
        if (_currRings >= DebugWinThreshold)
        {
            GameStateController.EndGame(true);
        }

        if (_currHealth <= 0)
        {
            GameStateController.EndGame(false);
        }
    } 
    
    public void UpdateHud()
    {
        ringValueHUD.text = _currRings.ToString();
        HPsliderHUD.value = _currHealth;
    }

    public void InitHud()
    {
        HPsliderHUD.maxValue = _currHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitHud();
        UpdateHud();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
