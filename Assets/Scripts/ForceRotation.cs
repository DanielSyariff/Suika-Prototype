using UnityEngine;

public class ForceRotation : MonoBehaviour
{
    void LateUpdate()
    {
        // Reset rotasi objek anak sehingga selalu menghadap ke atas
        transform.up = Vector2.up;
    }
}
