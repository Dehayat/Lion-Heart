using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.Hearts
{
    [AddComponentMenu("MiniGames/Hearts/Player")]
    public class Player : MonoBehaviour
    {
        public Vector2 Speed = new Vector2(2, 1);


        public delegate void Done(bool win);
        public event Done GameOver;

        Rigidbody2D rb;

        private Vector2 velocity = Vector2.zero;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void FixedUpdate()
        {
            velocity.x = Speed.x * Input.GetAxis("Horizontal");
            velocity.y = Speed.y * Input.GetAxis("Vertical");
            //transform.position += (Vector3)(velocity * Time.fixedDeltaTime);
            rb.velocity = velocity;
            //rb.velocity = Vector3.zero;
        }
        public void Kill()
        {
            GameOver(false);
        }
    }
}