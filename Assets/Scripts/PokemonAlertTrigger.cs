using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonAlertTrigger : MonoBehaviour {

	public PokemonCubeController pokemonCubeController;

	void OnTriggerEnter(Collider other){

		if (other.gameObject.CompareTag ("Player")) {
            pokemonCubeController.ZoomOut();
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pokemonCubeController.ZoomIn();
        }
    }


}
