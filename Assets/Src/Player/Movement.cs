using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.BasicRandomizer;
using UnityEngine.SceneManagement;




public class Movement : MonoBehaviour
{


    [Header("Movement")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float transitionSpeed = 0.5f;

    [Header("Step sound")]
    public BasicRandomizer audioRandomizer;
    public float stepDistance;

    [Header("Trigger")]
    public bool BlockPlayerOnTrigger = false;

    [Header("Dialog audio source")]
    public AudioSource dialogueAudioSource;
    public AudioClip[] SpritOneClips;
    public AudioClip[] SpritTwoClips;
    public AudioClip[] SpritThreeClips;





    [Header("Dialog text")]
    public GameObject subtitlesText;
    public string[] SpiritOneText;
    public string[] SpiritTwoText;
    public string[] SpiritThreeText;




    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private bool inTrigger = false;
    private Vector3 cameraOffset;
    private Vector3 cameraStartPosition;
    private Vector3 cameraEndPosition;
    private float accumulatedDistance;
    private Vector3 newDirection;
    private bool lookTheSky = false;
    private Vector3 targetDirection;
    private Quaternion targetRotation;
    private int countLookTheSky;
    private int textIndex;
    private string[] currentText;
    public AudioClip[] CurrentClips;


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        targetRotation = transform.rotation;


        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        accumulatedDistance = 0f;


        cameraOffset = characterController.transform.position - playerCamera.transform.position;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            //moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);


        if(characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                BasicRandomizer.Trigger(audioRandomizer);
                accumulatedDistance = 0f;
            }

        }

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        //newDirection =  Vector3.RotateTowards(transform.rotation.eulerAngles, transform.rotation.eulerAngles + new Vector3(0,111,0)   , transitionSpeed * Time.deltaTime);

        

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    targetRotation *= Quaternion.AngleAxis(45, -Vector3.right);
        //}
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * transitionSpeed * Time.deltaTime);



        if (lookTheSky)
        {

            countLookTheSky++;

            Debug.Log("Lookthesky " +  lookTheSky);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * transitionSpeed * Time.deltaTime);


            //if (targetRotation -  transform.rotation)
            if(countLookTheSky > 60)
                lookTheSky = false;

        }


        if (!canMove && textIndex <= currentText.Length)
        {

            displayDialogue(currentText);


        }


    }

    private void OnTriggerEnter(Collider other)
    {


        //cameraStartPosition = playerCamera.transform.position;
        //cameraEndPosition = playerCamera.transform.position + new Vector3(-2.82f, 0.49f, -1.13f);
        //cameraEndPosition = playerCamera.transform.position + new Vector3(-10f,10f, -10f);

        //inTrigger = true;


        
        if(other.gameObject.name == "Spirit Test")
        {


            countLookTheSky = 0;

            textIndex = 0;

            //targetDirection = transform.position - new Vector3(transform.position.x + 10, transform.position.y + 10, transform.position.z);

            targetRotation *= Quaternion.AngleAxis(45, -Vector3.right);

            lookTheSky = true;

            //dialogueAudioSource.Play();


            currentText = SpiritOneText;
            CurrentClips = SpritOneClips;


            subtitlesText.GetComponent<TextMesh>().text = currentText[0];
            dialogueAudioSource.GetComponent<AudioSource>().clip = CurrentClips[textIndex];

            dialogueAudioSource.Play();

            subtitlesText.SetActive(true);

            //transform.Rotate(0, 111, 0);
            //transform.Rotate(42.5f, 0, 0);


            Debug.Log("Collided");


            other.gameObject.GetComponent<BoxCollider>().enabled = false;


        }

        if (other.gameObject.name == "Spirit Test (1)")
        {


            countLookTheSky = 0;

            textIndex = 0;

            //targetDirection = transform.position - new Vector3(transform.position.x + 10, transform.position.y + 10, transform.position.z);

            targetRotation *= Quaternion.AngleAxis(45, -Vector3.right);

            lookTheSky = true;

            //dialogueAudioSource.Play();


            currentText = SpiritTwoText;
            CurrentClips = SpritTwoClips;


            subtitlesText.GetComponent<TextMesh>().text = currentText[0];
            dialogueAudioSource.GetComponent<AudioSource>().clip = CurrentClips[textIndex];

            dialogueAudioSource.Play();

            subtitlesText.SetActive(true);

            //transform.Rotate(0, 111, 0);
            //transform.Rotate(42.5f, 0, 0);


            Debug.Log("Collided");


            other.gameObject.GetComponent<BoxCollider>().enabled = false;


        }


        if (other.gameObject.name == "Spirit Test (2)")
        {


            countLookTheSky = 0;

            textIndex = 0;

            //targetDirection = transform.position - new Vector3(transform.position.x + 10, transform.position.y + 10, transform.position.z);

            targetRotation *= Quaternion.AngleAxis(45, -Vector3.right);

            lookTheSky = true;

            //dialogueAudioSource.Play();


            currentText = SpiritThreeText;
            CurrentClips = SpritThreeClips;


            subtitlesText.GetComponent<TextMesh>().text = currentText[0];
            dialogueAudioSource.GetComponent<AudioSource>().clip = CurrentClips[textIndex];

            dialogueAudioSource.Play();

            subtitlesText.SetActive(true);

            //transform.Rotate(0, 111, 0);
            //transform.Rotate(42.5f, 0, 0);


            Debug.Log("Collided");


            other.gameObject.GetComponent<BoxCollider>().enabled = false;


        }







        if (BlockPlayerOnTrigger)
            canMove = false;






        
        

        //playerCamera.transform.position = playerCamera.transform.position + new Vector3(-2.82f, 0.49f, -1.13f);
        //playerCamera.transform.position = new Vector3(0.67f, -0.72f, 0.14f);

        //playerCamera.transform.Rotate(0, 45, 0);


        
    }








    private void displayDialogue( string[] TextArray)
    {

        if (Input.GetButtonDown("Jump") && textIndex < TextArray.Length)
        {
            Debug.Log("Index " + textIndex);
            textIndex++;

            subtitlesText.GetComponent<TextMesh>().text = TextArray[textIndex];

            dialogueAudioSource.GetComponent<AudioSource>().clip = CurrentClips[textIndex];
            dialogueAudioSource.Play();



        }


        if (textIndex == TextArray.Length)
        {
            //lookTheSky = false;
            subtitlesText.SetActive(false);

            countLookTheSky = 0;

            targetRotation *= Quaternion.AngleAxis(45, Vector3.right);
            lookTheSky = true;

            canMove = true;
        }


    }



    private void OnTriggerExit(Collider other)
    {

        playerCamera.transform.position = characterController.transform.position - cameraOffset;
        //playerCamera.transform.Rotate(30, 30, 0);



    }

    //private void FixedUpdate()
    //{
     //   if (inTrigger)
        //{
            //Camera Transition
      //      playerCamera.transform.position = Vector3.Lerp(cameraStartPosition, cameraEndPosition, Time.deltaTime * transitionSpeed);


       // }
    //}

}
