using UnityEngine;

public class Display_Still_Image : MonoBehaviour
{ 
    public float size = 33.333f;
    Light light;
    public float dot;
    bool IsImportant, LisImportant;
    float k,intensity;

    void Start()
    {
        light = transform.Find("Spot Light").GetComponent<Light>();
        intensity = light.intensity;
        light.shadows = LightShadows.Soft;
    }
     
    void Upxdate()
    {
        light.spotAngle = size; 
        Vector3 pp = Players.PlayerCam.position;
        light.enabled = pp.y < 6 || (pp.y < 12 && -13 < pp.x && pp.x < -1 && -1 < pp.z && pp.z < 13);

        if (IsImportant != LisImportant)
        {
            k += Time.deltaTime * 4;
            light.intensity = intensity * (IsImportant ? k : 1-k);
            if (k > 1)
            {
                k = 0;
                LisImportant = IsImportant;
                light.renderMode = IsImportant ? LightRenderMode.ForcePixel : LightRenderMode.Auto;
            }
        }
        else if (light.enabled)
        {
            dot = Vector3.Dot((transform.position - Players.PlayerCam.position).normalized, Players.PlayerCam.forward);
            IsImportant = dot >0;
            if(IsImportant != LisImportant)
                k = 0;
            light.shadows = dot > 0.1f ? LightShadows.Soft : LightShadows.None;
        }
    }
}
