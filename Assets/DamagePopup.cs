using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI; // Text (UI) ÇégÇ§èÍçá
using TMPro; // TextMeshPro ÇégÇ§èÍçá

public class DamagePopup : MonoBehaviour
{
    public float displayDuration = 1f; // É_ÉÅÅ[ÉWï\é¶éûä‘
    public float moveSpeed = 1f; // è„è∏ë¨ìx
    public Color startColor = Color.white; // äJénéûÇÃêF
    public Color endColor = new Color(1, 1, 1, 0); // èIóπéûÇÃêF (ìßñæ)
    public Vector3 offset = new Vector3(0, 0.5f, 0); // ï\é¶à íuÇÃÉIÉtÉZÉbÉg

    private TextMeshProUGUI damageText; // TextMeshPro ÇégÇ§èÍçá
    // private Text damageText; // Text (UI) ÇégÇ§èÍçá
=======
using UnityEngine.UI; // Text (UI) „Çí‰Ωø„ÅÜÂ†¥Âêà
using TMPro; // TextMeshPro „Çí‰Ωø„ÅÜÂ†¥Âêà

public class DamagePopup : MonoBehaviour
{
    public float displayDuration = 1f; // „ÉÄ„É°„Éº„Ç∏Ë°®Á§∫ÊôÇÈñì
    public float moveSpeed = 1f; // ‰∏äÊòáÈÄüÂ∫¶
    public Color startColor = Color.white; // ÈñãÂßãÊôÇ„ÅÆËâ≤
    public Color endColor = new Color(1, 1, 1, 0); // ÁµÇ‰∫ÜÊôÇ„ÅÆËâ≤ (ÈÄèÊòé)
    public Vector3 offset = new Vector3(0, 0.5f, 0); // Ë°®Á§∫‰ΩçÁΩÆ„ÅÆ„Ç™„Éï„Çª„ÉÉ„Éà

    private TextMeshProUGUI damageText; // TextMeshPro „Çí‰Ωø„ÅÜÂ†¥Âêà
    // private Text damageText; // Text (UI) „Çí‰Ωø„ÅÜÂ†¥Âêà
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b

    private float timer;

    void Awake()
    {
<<<<<<< HEAD
        // GetComponentInChildren ÇÃï˚Ç™ÅAÉeÉLÉXÉgÇ™éqÉIÉuÉWÉFÉNÉgÇ≈Ç‡à¿ëSÇ…éÊìæÇ≈Ç´Ç‹Ç∑
        damageText = GetComponentInChildren<TextMeshProUGUI>();
=======
        damageText = GetComponent<TextMeshProUGUI>(); // TextMeshPro „Çí‰Ωø„ÅÜÂ†¥Âêà
        // damageText = GetComponent<Text>(); // Text (UI) „Çí‰Ωø„ÅÜÂ†¥Âêà
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
    }

    public void Setup(float damageAmount)
    {
<<<<<<< HEAD
        // ÅöïœçX: è¨êîì_à»â∫ÇêÿÇËéÃÇƒÇƒÅAÇ´ÇÍÇ¢Ç»êÆêîÇ≈ï\é¶Ç∑ÇÈ
        damageText.text = Mathf.FloorToInt(damageAmount).ToString();
        timer = displayDuration;
=======
        damageText.text = damageAmount.ToString();
        timer = displayDuration;
        // ÂàùÊúü‰ΩçÁΩÆ„ÇíË™øÊï¥ (‰æã: Êïµ„ÅÆÈ†≠‰∏ä„Å™„Å©)
        // transform.position = transform.parent.position + offset; // Ë¶™Ë¶ÅÁ¥†„Åå„ÅÇ„ÇãÂ†¥Âêà
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
    }

    void Update()
    {
        timer -= Time.deltaTime;

<<<<<<< HEAD
        // è„è∏
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // ÉtÉFÅ[ÉhÉAÉEÉg
=======
        // ‰∏äÊòá
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // „Éï„Çß„Éº„Éâ„Ç¢„Ç¶„Éà
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
        damageText.color = Color.Lerp(endColor, startColor, timer / displayDuration);

        if (timer <= 0)
        {
<<<<<<< HEAD
            Destroy(gameObject); // ï\é¶éûä‘åoâﬂÇ≈îjä¸
=======
            Destroy(gameObject); // Ë°®Á§∫ÊôÇÈñìÁµåÈÅé„ÅßÁ†¥Ê£Ñ
>>>>>>> 06cf35643ef73d7b82988806f5780709285f365b
        }
    }
}
