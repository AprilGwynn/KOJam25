using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour
{
    public InteractableType itemType = InteractableType.ring;
    public bool isNewItem = false;
    public Collider Collider;
    public Renderer Renderer; // skinned / static mesh renderer
    public GameObject CollectParticle; // a gameobject assumed to have a particle component set to "play on Awake"
    public AudioSource AudioSource;
    
    public void CollideHappens()
    {
        Debug.Log("interactable collided");
        Collider.enabled = false;
        Renderer.enabled = false;
        CollectParticle.SetActive(true);
        AudioSource.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReInitItem()
    {
        if (!isNewItem)
        {
            Collider.enabled = true;
            Renderer.enabled = true;
            CollectParticle.SetActive(false);
            AudioSource.enabled = false;
        }
    }

    public enum InteractableType
    {
        ring,
        wall,
        enemy,
        water,
        item
    }
}
