using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform hedef;          // Takip edilecek karakter
    public Vector3 mesafe = new Vector3(0, 5, -10); // Aradaki uzaklýk
    public float yumusamaSuresi = 0.3f; // Takip gecikme süresi (arttýkça daha yumuþak olur)

    private Vector3 mevcutHiz = Vector3.zero; // SmoothDamp için gerekli referans hýz

    // LateUpdate, tüm hareketler bittikten sonra çalýþtýðý için titremeyi engeller.
    void LateUpdate()
    {
        if (hedef == null) return;

        // Kameranýn gitmesi gereken yer
        Vector3 istenenPozisyon = hedef.position + mesafe;

        // SmoothDamp: Mevcut pozisyondan hedef pozisyona pürüzsüz geçiþ yapar
        transform.position = Vector3.SmoothDamp(transform.position, istenenPozisyon, ref mevcutHiz, yumusamaSuresi);

        // Kamerayý karaktere odaklar
        transform.LookAt(hedef);
    }
}
