using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public GameObject eatingSound;
    public GameObject startSound;
    public GameObject deathSound;
    public GameOverScreen GameOverScreen;
    private bool isDead = false;




    public Transform segmentPrefab;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform); 
        if (startSound != null)
    {
        AudioSource audioSource = startSound.GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {

        for(int i=segments.Count - 1; i > 0; i--){
            segments[i].position=segments[i-1].position;
        }

        this.transform.position = new Vector3(
            (float)(Math.Round(this.transform.position.x) + direction.x),
            (float)(Math.Round(this.transform.position.y) + direction.y),
            0.0f
        );
    }


    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
        ScoreManager.instance.AddPoints();
    }


    private void GameOver(){
        GameOverScreen.SetUp();
    }

    private void ResetState(){
         for(int i=1; i<segments.Count; i++){
            Destroy(segments[i].gameObject);
         }

         segments.Clear();
         segments.Add(this.transform);

         this.transform.position=Vector3.zero;
         ScoreManager.instance.ResetScore();
         ScoreManager.instance.UpdateHighscoreText();


         if (isDead)
         {
            GameOver();
         }

    
          isDead = false;
         
        if (deathSound != null)
           {
                AudioSource audioSource = deathSound.GetComponent<AudioSource>();
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }
            }
         
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            if (eatingSound != null)
            {
                AudioSource audioSource = eatingSound.GetComponent<AudioSource>();
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
            
        }
        else if(other.tag == "Obstacle" && !isDead){
            PlayerDied();
            ResetState();
            
        }
    }

    private void PlayerDied()
    {
        isDead = true;
    }
}
