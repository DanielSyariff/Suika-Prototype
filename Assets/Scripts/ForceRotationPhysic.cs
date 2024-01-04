using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRotationPhysic : MonoBehaviour
{
    private Transform parentTransform;
    public float rotationSpeed = 0.1f; // Ganti dengan kecepatan rotasi yang diinginkan

    void Start()
    {
        // Dapatkan referensi ke transform Parent
        parentTransform = transform.parent;
    }

    void Update()
    {
        // Dapatkan perubahan rotasi Parent sejak frame sebelumnya
        Quaternion rotasiDelta = parentTransform.rotation * Quaternion.Inverse(transform.rotation);

        // Ubah perubahan rotasi menjadi sumbu rotasi lokal Child
        Vector3 sudutRotasi = new Vector3(0, 0, Mathf.Atan2(rotasiDelta.x, rotasiDelta.w) * 180 / Mathf.PI);

        // Interpolasi rotasi Child untuk memberikan efek smoothing
        float faktorInterpolasi = Mathf.Clamp01(Time.deltaTime * rotationSpeed);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, sudutRotasi, faktorInterpolasi);
    }
}
