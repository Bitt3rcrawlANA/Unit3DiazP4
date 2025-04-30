using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float score;
    private PlayerController playerCtrlScript;

    public Transform startingPoint;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerCtrlScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        playerCtrlScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerCtrlScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerCtrlScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerCtrlScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        playerCtrlScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerCtrlScript.gameOver = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!playerCtrlScript.gameOver)
        {
            if (playerCtrlScript.doubleSpeed == false)
            {
                score += 2;
            }
            else if (playerCtrlScript.doubleSpeed == true)
            {
                score++;
            }
            Debug.Log("Score: " + score);
        }
    }
}
