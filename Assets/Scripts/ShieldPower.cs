using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour, IBuffItem
{
    [SerializeField]
    private float _speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        MoveDown();
        outOfBound();
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
            Destroy(this.gameObject);
        }
    }

    public void MoveDown()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
    }

    public void outOfBound()
    {
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
