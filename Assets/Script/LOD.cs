using UnityEngine;

public class LOD : MonoBehaviour
{ 
    float cnt; 
    public enum CheckIntervel { Constant, Auto }

    public Transform target;
    public float nearLine = 100;
    public CheckIntervel checkIntervel;
    public float interval;
     
    void Start()
    { 
        if (target == null) target = transform;
    }

    void Update()
    {
        if (target != null)
        {
            cnt += Time.deltaTime;
            if (interval <= cnt)
            {
                float distance = Vector3.Distance(Players.Player.position, target.transform.position);
                cnt = 0;
                target.gameObject.active = distance < nearLine;
                if (checkIntervel == CheckIntervel.Auto) interval = distance < nearLine ? 0.05f : (distance < 140 ? distance / 280 : 0.5f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (target != null) 
        { 
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(target.transform.position, nearLine);
        }
    }
}
