using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStateController : MonoBehaviour
{
    [FormerlySerializedAs("EndScreen")] public GameObject WinScreen;
    public GameObject LoseScreen;
    public MapMovement map;
    public MapRingAssembler mapRing;
    public PlayerController player;
    public PlayerValues playerValues;

    private List<Interactable> _itemList;
    
    public void EndGame(bool win)
    {
        if (win) WinScreen.SetActive(true);
        else LoseScreen.SetActive(true);
        Time.timeScale = 0.035f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        // TODO fade out?
        map.ReInitMapMovement();
        foreach (Interactable item in _itemList)
        {
            item.ReInitItem();
        }
        player.InitLanePos();
        playerValues.InitValues();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        playerValues.UpdateHud();
        player.mapRing.StartAccelerating(player.acceleration);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _itemList = new List<Interactable>();
        foreach (Transform item in mapRing.itemsParent.transform)
        {
            _itemList.Add(item.gameObject.GetComponent<Interactable>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
