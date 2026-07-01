using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    private Material bgMaterial;
    private Vector2 offset;
    
    // Сюда в инспекторе перетащи объект твоего игрока (или камеры)
    public Transform player; 
    
    // Коэффициент скорости сдвига (настраивается в инспекторе)
    public float speedMultiplier = 0.1f; 

    void Start()
    {
        // Получаем материал, привязаный к нашему Quad
        bgMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (player != null)
        {
            // Считаем сдвиг текстуры на основе текущей позиции игрока в мире
            offset.x = player.position.x * speedMultiplier;
            offset.y = player.position.y * speedMultiplier;

            // Применяем сдвиг к главной текстуре материала
            bgMaterial.mainTextureOffset = offset;
        }
    }
}