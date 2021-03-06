using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    // References
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private SceneLoader1 sceneManager;
    [SerializeField] private RespawnEnemy respawnEnemy;
    [SerializeField] private Transform player;
    // [SerializeField] private RectTransform slidingArea;
    // [SerializeField] private Scrollbar scrollbarUI;
    // [SerializeField] private Text questionUI;

    // Player settings
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveInputDeadZone;

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;
    private bool disableLookAround = false;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;
    // Handle Health
    public GameObject healthBarUI;
    public Slider slider;
    public float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        player.position = new Vector3(51,1,34);
        // Player Health
        maxHealth = 100f;
        health = maxHealth;
        slider.value = CalculateHealth();
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Player Health
        slider.value = CalculateHealth();
        if(health<=0)
        {
            health = 100f;
            player.position = new Vector3(51,1,34);
            respawnEnemy.Reset();
            sceneManager.LoadEndScreen();
            
        }

        if(respawnEnemy.wave >= 11)
        {
            health = 100f;
            player.position = new Vector3(51,1,34);
            respawnEnemy.Reset();
            sceneManager.Finish();
            
        }
        // Handles input
        GetTouchInput();


        if (rightFingerId != -1 && !disableLookAround) {
            // Ony look around if the right finger is being tracked
            // Debug.Log("Rotating");
            LookAround();
        }

        if (leftFingerId != -1)
        {
            // Ony move if the left finger is being tracked
            // Debug.Log("Moving");
            Move();
        }
    }

    void GetTouchInput() {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        if(!disableLookAround)
                        {
                            EnableLookAround();
                        }
                        Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId) {

                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround() {

        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move() {

        // Don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        // Multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        // Move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }

    public void DisableLookAround()
    {
        disableLookAround = true;
        // Debug.Log("Disabling");
    }

    public void EnableLookAround()
    {
        disableLookAround = false;
        Debug.Log("Enabling");
    }

    // public void calculateSlidingPos()
    // {
    //     // posisi baru = posisi lama + perubahan posisi * persentasi handler
    //     // float currentPos = slidingArea.offsetMin.x;
    //     Debug.Log("slider");
    //     float maxPos = 633f;
    //     float newPos = (1 - scrollbarUI.value) * maxPos;
    //     slidingArea.offsetMax = new Vector2(slidingArea.offsetMax.x, newPos);
    //     slidingArea.offsetMin = new Vector2(slidingArea.offsetMin.x, newPos);
    // }
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void Attacked(float damage)
    {
        health -= damage;
    }


}