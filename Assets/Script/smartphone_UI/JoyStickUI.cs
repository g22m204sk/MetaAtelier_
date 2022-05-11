using UnityEngine; 
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Side side;
    public Vector2 input_;
    public enum HandleType { Rect, Circle }
    public HandleType type;
    public bool ebl;
    public bool isDrag;
    [Range(0.0001f,0.99f)]
    public float PulledBackPower;
    public RawImage btm,back;
    public RectTransform rt,brt;
    Vector2 border;
    float Circleborder;
    Vector3 e;
    public enum Side { LEFT, RIGHT }

    public void Start()
    { 
        brt = transform.parent.GetComponent<RectTransform>();
        back = transform.parent.GetComponent<RawImage>();
        btm = GetComponent<RawImage>();
        rt  = GetComponent<RectTransform>();

        e = rt.localPosition;
    }

    void FixedUpdate()
    {
        border = brt.sizeDelta / 2;
        Circleborder = brt.sizeDelta.x / 2;
        back.color = btm.color = !ebl ? (Color.gray + Color.black)/2 : Color.white;
    }


    void Update()
    {
        if (ebl)
        {
            if (!isDrag && Input.touchCount == 0)
                rt.localPosition *= (1 - PulledBackPower);
        }
        else
            rt.localPosition = e;

        input_ = new Vector2(rt.localPosition.x/(brt.sizeDelta.x - rt.sizeDelta.x), rt.localPosition.y / (brt.sizeDelta.y - rt.sizeDelta.y)) *2;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        if (!ebl) return;
        var d = transform.InverseTransformPoint(eventData.position);
        float Dw = (eventData.position.x * 1f / Screen.width); 
        btm.color = Color.green;
        if ((side == Side.LEFT && Dw < 0.5f) || (side == Side.RIGHT && Dw >= 0.5f))
        {
            if (type == HandleType.Circle)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.x);
                float ee = Circleborder - rt.sizeDelta.x / 2 + e.x;
                float dis = Vector3.Distance(Vector3.zero, d);
                Debug.Log(ee + ":" + dis);
                rt.localPosition = (dis > ee) ? Vector3.Lerp(Vector3.zero, d, ee / dis) : d;
            }
            else
            {

                Vector2 ee = border - rt.sizeDelta / 2 + new Vector2(e.x, e.y);
                Debug.Log("xxW:" + eventData.position.x + ":" + eventData.position.y + "xxxx : : " + d.x + ":" + d.y);

                rt.localPosition = new Vector3()
                {
                    x = d.x > ee.x ? ee.x : (d.x < -ee.x ? -ee.x : d.x),
                    y = d.y > ee.y ? ee.y : (d.y < -ee.y ? -ee.y : d.y),
                    z = 0
                };
            }
        }
    }
}
