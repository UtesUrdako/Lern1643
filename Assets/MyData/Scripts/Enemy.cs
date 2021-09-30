using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void Start()
    {

        Debug.Log($"Hit {player.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
