using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float spd = 1.0f;

    public GameObject target;
    Vector3 direct = Vector3.down;

    public GameObject prefabsExplosion;

    private void Start()
    {
        target = GameObject.Find("Character");
        int rndNum = Random.Range(0, 10);
        if (rndNum % 3 == 0)
        {
            direct = target.transform.position - transform.position;
            direct.Normalize();
        }
    }

    private void Update()
    {
        transform.position = transform.position + direct * spd * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject gameManager = GameObject.Find("GameManager");
            ScoreManager scoreManager = gameManager.GetComponent<ScoreManager>();
            AudioSource audioSoucre = gameManager.GetComponent<AudioSource>();

            scoreManager.nowScore++;
            scoreManager.nowScoreUI.text = "Now Score : " + scoreManager.nowScore;  

            if(scoreManager.nowScore > scoreManager.bestScore)
            {
                scoreManager.bestScore = scoreManager.nowScore;
                scoreManager.bestScoreUI.text = "Best Score : " + scoreManager.bestScore;
                PlayerPrefs.SetInt("bestscore", scoreManager.bestScore);
            }

            GameObject explosionObj = Instantiate(prefabsExplosion);
            explosionObj.transform.position = transform.position;

            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
    }
}
