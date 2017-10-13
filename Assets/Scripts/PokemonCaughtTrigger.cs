using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonCaughtTrigger : MonoBehaviour {

    public GameObject caughtEffectPrefab;
    public GameObject pokemonCheckListItemTick;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pokemonCheckListItemTick.SetActive(true);

            GameObject catchEffect = Instantiate(caughtEffectPrefab, transform.position, transform.rotation);
       
            Destroy(catchEffect, 2.0f);

            GameManager.instance.caughtPokemonNum += 1;
            if(GameManager.instance.caughtPokemonNum>= GameManager.instance.needCaughtPokemonNumToWin)
            {
                GameManager.instance.ShowWinGameUI();
            }

            transform.parent.gameObject.SetActive(false);

        }
    }
}
