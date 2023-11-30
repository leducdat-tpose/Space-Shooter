using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePower : Power
{
    protected override void Start()
    {
        base.Start();
        this._tag = "TriplePower";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.receiveTripleBuff();
                AudioManager.Instance.playBuff();
            }
            base.DeSpawn();
        }
    }

}
