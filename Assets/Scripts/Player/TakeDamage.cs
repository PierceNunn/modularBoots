using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamage : MonoBehaviour
{
    public static TakeDamage Instance;

    private float intensityDecay;
    private PostProcessVolume _volume;
    private Vignette _vignette;

    /// <summary>
    /// Singleton
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings<Vignette>(out _vignette);
        _vignette.enabled.Override(false);
    }

    public void PlayHurtVignette()
    {
        StartCoroutine(TakeDamageEffect());
    }

    IEnumerator TakeDamageEffect()
    {
        intensityDecay = 0.35f;

        _vignette.enabled.Override(true);
        _vignette.intensity.Override(0.35f);

        yield return new WaitForSeconds(0.5f);
        while (intensityDecay > 0)
        {
            intensityDecay -= 0.05f;
            if (intensityDecay < 0)
            {
                intensityDecay = 0;
            }
            _vignette.intensity.Override(intensityDecay);

            yield return new WaitForSeconds(0.1f);
        }

        _vignette.enabled.Override(false);
        yield break;
    }
}
