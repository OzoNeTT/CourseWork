using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс "тряски" камеры.
/// <remarks>Данный класс реализует тряску камеры некоторое время с некоторой амплитудой.</remarks>
/// </summary>
public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Корутин, производящий тряску камеры некоторое время.
    /// </summary>
    /// <param name="duration">Длительность тряски.</param>
    /// <param name="magnitude">Амплитуда.</param>
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}