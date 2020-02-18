using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.Hearts
{
    [AddComponentMenu("MiniGames/Hearts/Spawner")]
    public class Spawner : MonoBehaviour
    {
        public GameObject Enemy;
        public Vector2 Bounds = new Vector2(-1, 1);
        public float spawnRate = 0.5f;
        public float lifeTime = 2f;

        private float lastSpawn = 0;
        private SpriteRenderer EnemyRenderer;

        private void Start()
        {
            EnemyRenderer = Enemy.GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            if (Time.time>lastSpawn+spawnRate)
            {
                Vector3 pos = transform.position;
                pos.y += EnemyRenderer.bounds.size.y*1.5f;
                pos.x += Random.Range(Bounds.x + EnemyRenderer.bounds.size.x / 2, Bounds.y - EnemyRenderer.bounds.size.x / 2);
                Destroy(Instantiate(Enemy,pos,Quaternion.identity,transform.parent),lifeTime);
                lastSpawn = Time.time;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(transform.position.x + Bounds.x,transform.position.y,0), new Vector3(transform.position.x + Bounds.y, transform.position.y, 0));
        }
#endif

    }
}