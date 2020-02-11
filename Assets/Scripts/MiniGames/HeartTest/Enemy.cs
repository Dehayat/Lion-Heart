using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.Hearts
{
    [AddComponentMenu("MiniGames/Hearts/Enemy")]
    public class Enemy : MonoBehaviour
    {
        private Rigidbody2D rb;
        public Vector2 speed = new Vector2(0,-5);

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb.velocity = speed;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Environment")
                return;
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().Kill();
            }
            Destroy(gameObject);
        }
    }
}