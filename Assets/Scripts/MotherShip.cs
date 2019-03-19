using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MotherShip : MonoBehaviour 
{
    public Text txt;
	public static int collectedEnergy = 0;
	public static int neededEnergy;
    public string gameOver = "GAME OVER!";
    public string missionComplete = "MISSION COMPLETE!";
	public GameObject[] energy;
	public  int totalEnergy;

	public float difficultyPercentage = 0.5f;
	
	private PlayerInventory playerInventory;

	private Animator anim;

	public float restartDelay = 3f;
	private float restartTimer;
	
	void Awake()
	{
		playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
		energy = GameObject.FindGameObjectsWithTag("Energy");
		totalEnergy = energy.Length;
		neededEnergy = Mathf.RoundToInt (totalEnergy * difficultyPercentage);
		anim = GameObject.Find ("HUDCanvas").GetComponent<Animator>();
	}

	void Update()
	{
		if(totalEnergy < neededEnergy )
		{
            //print ("Game Over!");
            txt.text = gameOver.ToString();
			anim.SetTrigger("IsGameOver");
            
			restartTimer+= Time.deltaTime;

			if(restartTimer >= restartDelay)
			{
                collectedEnergy = 0;
				Application.LoadLevel(Application.loadedLevel);
			}

		}
        if (collectedEnergy == neededEnergy) {
            txt.text = missionComplete.ToString();
            anim.SetTrigger("IsGameOver");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay) {
                collectedEnergy = 0;
                Application.LoadLevel(Application.loadedLevel);
            }

        }
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//Take collected count and add it to the energy of the reactor, then reset the players collected energy to zero
			if(playerInventory.collectedEnergy != 0)
			{
				collectedEnergy += playerInventory.collectedEnergy;
				playerInventory.collectedEnergy = 0;
			}
			//Need ten to win the game.
			//if(collectedEnergy == neededEnergy)
			//{
				//print ("You win!");
				//anim.SetTrigger("IsGameOver");
			//}
			
		}
	}
}
