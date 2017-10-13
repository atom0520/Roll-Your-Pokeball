using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonCubeController : MonoBehaviour {

    public Animator animator;
    public GameObject body;
    public GameObject alertTrigger;
    public GameObject bulletPrefab;
	public Transform bulletFireTransform;

    public string[] messages;
    public GameObject popupMessage;

    public float popupMesCoolTimeDuration = 5.0f;
    float popupMesCounter;
    public float mesShowingDuration = 2.0f;
    float mesShowingCounter;

    public float bulletFireCoolDownDuration = 1.0f;
	float bulletFireCounter;

    public float averageHideDuration = 2.0f;
    public float hideDurationVar = 2.0f;
    float hideCounter;
    public float averageShowDuration = 6.0f;
    public float showDurationVar = 2.0f;
    float showCounter;

    bool canFire;
    bool isZooming;
    public float zoomSpeed = 1.0f;

    public float minSpawnPosX = -9.0f;
    public float maxSpawnPosX = 9.0f;
    public float minSpawnPosZ = -9.0f;
    public float maxSpawnPosZ = 9.0f;

    GameObject target;
  

    // Use this for initialization
    void Start () {

        body.SetActive(false);
        alertTrigger.SetActive(false);

        target = GameObject.FindWithTag("Player");
        bulletFireCounter = 0;
        hideCounter = averageHideDuration + Random.Range(-hideDurationVar, hideDurationVar);
        showCounter = 0;

        popupMesCounter = 0;
        mesShowingCounter = 0;

        animator.speed = zoomSpeed;
        isZooming = false;
    }

	// Update is called once per frame
	void Update () {
        if (GameManager.instance.gameState != GameManager.GameState.STARTED)
            return;

        if (body.activeSelf == false)
        {
            if (!isZooming && hideCounter <= 0)
            {
                ZoomIn();
            }
            else
            {
                hideCounter -= Time.deltaTime;
            } 
       
        }
        else
        {
            if (!isZooming && showCounter <= 0)
            {
                
                ZoomOut();
            }
            else
            {
                showCounter -= Time.deltaTime;
            }

            if (!popupMessage.activeSelf)
            {
                if (popupMesCounter <= 0)
                {
                    ShowMessage();
                }
                else
                {
                    popupMesCounter -= Time.deltaTime;
                }
            }
            else
            {
                if (mesShowingCounter <= 0)
                {
                    HideMessage();
                }
                else
                {
                    mesShowingCounter -= Time.deltaTime;
                }
            }
           
        }

		if (bulletFireCounter <= 0) {
			if (canFire) {
				GameObject bullet = GameObject.Instantiate (bulletPrefab, bulletFireTransform.position, bulletFireTransform.rotation);

                if(target != null)
                    bullet.GetComponent<BulletController> ().Init (target);

				bulletFireCounter = bulletFireCoolDownDuration;
			}			
		} else {
			bulletFireCounter -= Time.deltaTime;
		}

	}

	public void OnZoomInBegin(){
        
        showCounter = averageShowDuration + Random.Range(-showDurationVar, showDurationVar);

        body.SetActive (true);
        alertTrigger.SetActive(true);
    }

	public void OnZoomOutEnd(){
       
        hideCounter = averageHideDuration + Random.Range(-hideDurationVar, hideDurationVar);

        body.SetActive (false);
        alertTrigger.SetActive(false);

        transform.position = new Vector3(Random.Range(minSpawnPosX, maxSpawnPosX), 0, Random.Range(minSpawnPosZ, maxSpawnPosZ));

        if (popupMessage.activeSelf)
            HideMessage();

        isZooming = false;
    }

	public void OnZoomInEnd(){
		canFire = true;
        isZooming = false;
    }

	public void OnZoomOutBegin(){
		canFire = false;
    }

    public void ZoomOut()
    {
        animator.SetTrigger("zoomOut");
        isZooming = true;
    }

    public void ZoomIn()
    {
        animator.SetTrigger("zoomIn");
        isZooming = true;
    }

    void ShowMessage()
    {
        popupMessage.GetComponent<TextMesh>().text = messages[Random.Range(0, messages.Length)];
        popupMessage.SetActive(true);
        mesShowingCounter = mesShowingDuration;
    }

    void HideMessage()
    {
   
        popupMesCounter = popupMesCoolTimeDuration;
        popupMessage.SetActive(false);
    }
}
