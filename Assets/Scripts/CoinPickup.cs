using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
   [SerializeField] private AudioClip sound;
private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
       AudioSource.PlayClipAtPoint(sound,Camera.main.transform.position);
       FindObjectOfType<GameSession>().Score();
         Destroy(gameObject);   
      }

      
   }
}
