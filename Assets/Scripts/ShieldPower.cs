using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : Power
{
    protected override void Start()
    {
        base.Start();
        this._tag = "ShieldPower";
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.ShieldPower(true);
                AudioManager.Instance.playBuff();
            }
            base.DeSpawn();
        }
    }
}
