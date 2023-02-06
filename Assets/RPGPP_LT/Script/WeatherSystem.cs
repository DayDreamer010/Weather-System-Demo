using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
   // [SerializeField] private GameObject rain;
    [SerializeField] private ParticleSystem rain1;
    public AudioSource rainAudio;



    public void Start()
    {
        rainAudio.Stop();
        int isRain = Random.Range(0, 2);
        Debug.Log("number" + isRain);
        if (isRain == 0)
        {
            rain1.enableEmission = false;
        }
        else
        {
            StartCoroutine(RainStart());

        }
        StartCoroutine(WeatherCheck());
       
    }

    IEnumerator WeatherCheck()
    {
       
       
        yield return new WaitForSeconds(30f);
        if (rain1.enableEmission == false)
        {
            StartCoroutine(RainStart());
        }
        else
        {
            StartCoroutine(RainStop());
        }
        yield return new WaitForSeconds(30f);
        StartCoroutine(WeatherCheck());
    }
    IEnumerator RainStart()
    {
        rain1.enableEmission = true;
        var p1 = rain1.emission;
        rainAudio.Play();
        p1.rateOverTime = 200;
        rainAudio.volume = 0.5f;
        yield return new WaitForSeconds(10f);
        p1.rateOverTime = 2000;
        rainAudio.volume = 1f;
        rainAudio.enabled = true;
    }


    IEnumerator RainStop()
    {
        rainAudio.Play();
        rain1.enableEmission = true;
        var p1 = rain1.emission;
        p1.rateOverTime = 900;
        rainAudio.volume = 0.5f;
        yield return new WaitForSeconds(10f);
        p1.rateOverTime = 200;
        rainAudio.volume = 0.2f;
        yield return new WaitForSeconds(2f);
        rain1.enableEmission = false;
        rainAudio.Stop();

    }
    private void Update()
    {
       






    }
}

    



   
