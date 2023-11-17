using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidBoundaries : MonoBehaviour
{
    public CircleCollider2D lingkaranCollider;

    void FixedUpdate()
    {
        // Periksa apakah objek berada di dalam lingkaran
        if (!lingkaranCollider.bounds.Contains(transform.position))
        {
            // Hitung vektor dari pusat lingkaran ke posisi objek
            Vector3 vektorKePusat = transform.position - lingkaranCollider.bounds.center;

            // Normalisasi vektor dan kalikan dengan jari-jari lingkaran
            vektorKePusat.Normalize();
            vektorKePusat *= lingkaranCollider.radius;

            // Atur posisi objek kembali ke batas lingkaran
            transform.position = lingkaranCollider.bounds.center + vektorKePusat;
        }
    }
}
