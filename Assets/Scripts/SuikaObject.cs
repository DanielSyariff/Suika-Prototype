using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuikaObject : MonoBehaviour
{
    public SuikaType suikaTypeObject;
    SuikaManager suikaManager;

    private CircleCollider2D childCollider; // Referensi ke CircleCollider2D di objek anak
    private bool canMerge = true;
    public bool isTriggeringRedLine;

    private void Start()
    {
        childCollider = GetComponent<CircleCollider2D>();
        suikaManager = SuikaManager.instance;
    }

    //private void Update()
    //{
    //    if (isTriggeringRedLine)
    //    {
    //        if (this.transform.localPosition.y >= 2)
    //        {
    //            suikaManager.Alert(true);
    //        }
    //        else
    //        {
    //            suikaManager.Alert(false);
    //        }
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMerge && collision.gameObject.CompareTag("Fruit") && (int)collision.gameObject.GetComponent<SuikaObject>().suikaTypeObject == (int)suikaTypeObject)
        {
            //Debug.Log("BERSENTUHAN");
            // Hapus collider untuk sementara
            childCollider.enabled = false;

            // Dapatkan titik sentuhan
            Vector2 contactPoint = collision.GetContact(0).point;

            // Buat objek baru (Strawberry) di tempat bersentuhannya
            suikaManager.SpawnTriggeredFruit((int)suikaTypeObject, contactPoint);
            //Debug.Log("Type Number : " + (int)suikaTypeObject);
            collision.transform.SetParent(suikaManager.poolerManager.objectPool[(int)suikaTypeObject - 1].transform);
            collision.gameObject.GetComponent<PooledObject>().pool.ReturnObject(collision.gameObject);

            // Hancurkan objek lama, Jika pakai Instantiate
            //Destroy(collision.gameObject);

            canMerge = false;

            // Setelah merger, aktifkan collider kembali
            Invoke("EnableCollider", 0.1f);

            suikaManager.audioManager.PlaySFXOneShot("BottleCollide");
        }
    }

    void EnableCollider()
    {
        canMerge = true;
        childCollider.enabled = true;
    }

    public void EnableRedLine()
    {
        Invoke("EnableTriggeringRedLine", 1f);
    }

    void EnableTriggeringRedLine()
    {
        isTriggeringRedLine = true;
    }

    public void DisableRedLine()
    {
        isTriggeringRedLine = false;
    }
}
