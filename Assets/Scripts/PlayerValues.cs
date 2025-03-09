using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerValues : MonoBehaviour
{
    public int maxHP = 50;
    
    public GameStateController GameStateController;
    public int DebugWinThreshold = 5;
    public TMP_Text ringValueHUD;
    public Animator ringHudAnimator;
    // private GameObject mapRing;
    public PlayerController _playerController;
    
    public Slider HPsliderHUD;
    // public GameObject WinHUD;
    private int _currRings;
    private float _currHealth;
    public bool isInvul = false;

    public GameObject ringPrefab;


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
            StartCoroutine(Invul(1));
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
        Vector3 spawnPos = this.transform.position;
        GameObject newRing; 
        for (int j = 0; j < takenRings; j++)
        {
            newRing = Instantiate(ringPrefab, spawnPos, Quaternion.identity, _playerController.mapRing.transform);
            newRing.transform.localScale *= 0.65f;
            spawnPos += new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));

            newRing.GetComponent<Interactable>().isNewItem = true;
        }
        
    }

    private IEnumerator Invul(float duration)
    {
        isInvul = true;
        _playerController.playerAnimator.SetBool("isInvul", true);
        yield return new WaitForSeconds(duration);
        isInvul = false;
        _playerController.playerAnimator.SetBool("isInvul", false);
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

    public void InitValues()
    {
        _currRings = 0;
        _currHealth = maxHP;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitValues();
        InitHud();
        UpdateHud();
        // _playerController.mapRing = this.GetComponent<PlayerController>().mapRing.GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
