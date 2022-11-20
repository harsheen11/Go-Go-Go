using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // public Transform transfrom;

    // public Rigidbody rb;

    public float speed = 1f;
    public float rotationSpeed = 1f;

    // This bool prevents car to turn direction while being still
    private bool isMoving = false;

    // bool is set to be true after the iginition sound is played.
    // once the bool is true, player can move the car.
    private bool ignitionComplete = false;
    public AudioClip ignitionClip;

    //bool used to check if acceleration clip already being played
    private bool acceleraionClipPlayed = false;
    // private Vector3 leftLimit = new Vector3(680, 0, 0);
    //private Vector3 rightLimit = new Vector3(-30, 0, 0);
    //private Vector3 moveTO;

    // private float volumeControl = 1f;
    public AudioClip accelerationClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //rb = GetComponent<Rigidbody>();
        audioSource.clip = ignitionClip;
        audioSource.Play();
    }
    void Update()
    {
        StartCoroutine(ingitionTimer());

        if (ignitionComplete)
        {
            carFrontMovement();
            // If car is moving then user can turn it
            if (isMoving)
            {
                carSideMovement();
            }

        }

        // This bool prevents car to turn direction while being still
        isMoving = false;
    }

    // function which is called in update. Checks for user input of the vertical keys and based on them
    // moves the car
    void carFrontMovement()
    {
        // This bool prevents car to turn direction while being still
        isMoving = true;

        // forward keys (a, upper arrow) return  postive one whereas backwards keys (s, down arrow) returns negative 1
        float translation = Input.GetAxis("Vertical") * speed;
        translation *= Time.deltaTime;


        if (Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1)
        {
            if (!acceleraionClipPlayed)
                playAccelerationClip();
        }


        else
        {
            StartCoroutine(stopAccelerationClip());
            //audioSource.Pause();
        }

        transform.Translate(translation, 0, 0);
        //rb.AddForce(translation, 0, 0, ForceMode.Impulse);
        // moveTO = (0, 0, translation)
        // rb.MovePosition(moveTO);

    }

    void carSideMovement()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        rotation *= Time.deltaTime;

        transform.Rotate(0, rotation, 0);

    }

    IEnumerator ingitionTimer()
    {
        yield return new WaitForSeconds(1.55f);
        ignitionComplete = true;
        audioSource.clip = accelerationClip;

    }



    void playAccelerationClip()
    {

        audioSource.Play();
        Debug.Log("should play");
        acceleraionClipPlayed = true;

    }




    IEnumerator stopAccelerationClip()
    {
        if (acceleraionClipPlayed)
        {
            yield return new WaitForSeconds(0.55f);
            Debug.Log("stopped");
            audioSource.Pause();
        }

        //audioSource.clip = accelerationClip;
        acceleraionClipPlayed = false;
    }


}
