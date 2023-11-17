using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuikaObject : MonoBehaviour
{
    public SuikaType suikaTypeObject;
    SuikaManager suikaManager;

    private CircleCollider2D childCollider; // Referensi ke CircleCollider2D di objek anak
    private bool canMerge = true;

    private void Start()
    {
        childCollider = GetComponent<CircleCollider2D>();
        suikaManager = SuikaManager.instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMerge && collision.gameObject.CompareTag("Fruit") && (int)collision.gameObject.GetComponent<SuikaObject>().suikaTypeObject == (int)suikaTypeObject)
        {
            Debug.Log("BERSENTUHAN");
            // Hapus collider untuk sementara
            childCollider.enabled = false;

            // Dapatkan titik sentuhan
            Vector2 contactPoint = collision.GetContact(0).point;

            // Buat objek baru (Strawberry) di tempat bersentuhannya
            suikaManager.SpawnTriggeredFruit((int)suikaTypeObject, contactPoint);

            // Hancurkan objek lama
            Destroy(collision.gameObject);

            canMerge = false;

            // Setelah merger, aktifkan collider kembali
            Invoke("EnableCollider", 0.1f);
        }
    }

    void EnableCollider()
    {
        canMerge = true;
        childCollider.enabled = true;
    }
}
