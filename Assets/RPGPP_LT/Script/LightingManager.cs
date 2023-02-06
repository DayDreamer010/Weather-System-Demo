using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
  
    [SerializeField] private Light DirectionalLight;
     [SerializeField, Range(0, 218)] private float TimeOfDay;
    [SerializeField] private Light[] lightPoint;
    [SerializeField] private SpriteRenderer Star;
    [SerializeField] private Animator S1;





    private void Update()
    {
      

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 218;
         
            UpdateLighting(TimeOfDay / 218f);
            if (TimeOfDay>=50)
            {
                for (int i = 0; i < lightPoint.Length; i++)
                {
                    if (TimeOfDay >= 180)
                    {
                        lightPoint[i].enabled = true;
                        S1.SetBool("FadeIn", true);
                    }
                    else
                    {
                        lightPoint[i].enabled = false;
                        S1.enabled = true;
                      
                       
                    }
                  
                }
                
                
            }
            
           
        }
        else
        {
            UpdateLighting(TimeOfDay / 218f);
        }
    }

    public IEnumerator FadeOut(float fadeSpeed)
    {
        SpriteRenderer rend = Star;
        Color matColor = Star.color;
        float alphaValue = rend.color.a;


        while (rend.material.color.a > 0f)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            rend.material.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            yield return new WaitForSeconds(1f);
        }
        rend.material.color = new Color(matColor.r, matColor.g, matColor.b, 0f);
        
    }


    private void UpdateLighting(float timePercent)
    {    if (DirectionalLight != null)
        {
        

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }



    
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
       
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
