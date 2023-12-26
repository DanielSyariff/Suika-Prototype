using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuikaController : MonoBehaviour
{
    [Header("Script")]
    private SuikaManager suikaManager;

    [Header("Controller")]
    public float moveSpeed = 5.0f; // Kecepatan pergerakan
    public float moveThreshold = 2.0f; // Threshold batas pergerakan

    [Header("Fruit Control")]
    public List<FruitData> fruitToSpawn;
    public GameObject fruitObject;
    public int setupFruitNumber;

    [Header("Component")]
    public GameObject fallIndicator;
    public static Vector3 initialPosition;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;

    public float dropCD;
    float timer;


    void Start()
    {
        suikaManager = SuikaManager.instance;

        initialPosition = transform.position;
        SetupNextFruit(true);
        SetupCurrentFruit();
    }

    public void SetupNextFruit(bool firstRow)
    {
        //Jika Pertama kali, maka Selalu kasih Potion Paling Kecil
        if (firstRow)
        {
            setupFruitNumber = 0;
        }
        else
        {
            setupFruitNumber = Random.Range(0, fruitToSpawn.Count);
        }
        suikaManager.uiManager.UpdateUINextFruit(fruitToSpawn[setupFruitNumber].fruitSprite);
    }

    public void SetupCurrentFruit()
    {
        //GameObject getFruit = Instantiate(fruitToSpawn[setupFruitNumber].fruitObject, this.transform);

        GameObject getFruit = suikaManager.GetObjectPool(fruitToSpawn[setupFruitNumber].suikaType);
        getFruit.transform.SetParent(this.transform);
        getFruit.transform.localPosition = Vector3.zero;

        moveThreshold = fruitToSpawn[setupFruitNumber].threshold;

        fruitObject = getFruit;

        startingPosition = initialPosition;
        targetPosition = initialPosition;

        rb = fruitObject.GetComponent<Rigidbody2D>();
        circleCollider2D = fruitObject.GetComponent<CircleCollider2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Set body type ke kinematic untuk mengendalikan pergerakan melalui script
        circleCollider2D.enabled = false;

        SetupNextFruit(false);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetupCurrentFruit();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            fallIndicator.SetActive(true);
            Vector3 inputPosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(inputPosition);
            targetPosition.z = transform.position.z;

            // Cek apakah klik terjadi di dalam threshold
            float distance = Mathf.Abs(targetPosition.x - startingPosition.x);
            isDragging = distance <= moveThreshold;
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 inputPosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(inputPosition);
            targetPosition.z = transform.position.z;

            // Batasan pergerakan hanya horizontal
            targetPosition.y = startingPosition.y;
        }

        // Batasan pergerakan
        targetPosition.x = Mathf.Clamp(targetPosition.x, startingPosition.x - moveThreshold, startingPosition.x + moveThreshold);

        // Tetapkan posisi Y ke posisi awal
        targetPosition.y = startingPosition.y;

        // Pindahkan objek
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                timer = dropCD;
                fallIndicator.SetActive(false);
                // Aktifkan Rigidbody2D dan lepaskan kontrol pergerakan
                fruitObject.transform.SetParent(suikaManager.bucket);
                rb.bodyType = RigidbodyType2D.Dynamic;
                circleCollider2D.enabled = true;
                isDragging = false;

                fruitObject.gameObject.GetComponent<SuikaObject>().EnableRedLine();

                SetupCurrentFruit();
            }
        }
    }
}
