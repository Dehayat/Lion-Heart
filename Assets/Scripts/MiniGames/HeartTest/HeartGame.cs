using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGames.Hearts
{
    public class HeartGame : MiniGameObject
    {
        public Player player;

        private float elapsedTime = 0;

        private void Start()
        {
            player.GameOver += GameOver;
        }
        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 10)
            {
                FinishGame(true);
            }
        }
        public void GameOver(bool win)
        {
            if(myFrame!=null)
                FinishGame(win);
        }
    }
}