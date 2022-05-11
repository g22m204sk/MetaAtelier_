using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCtrl : MonoBehaviour
{
    public SkinnedMeshRenderer STALK_MAIN;
    public MeshRenderer Water;
    public SkinnedMeshRenderer Leaf;
    [Range(0f, 1f)]
    public float ctrler;

    float v;


    MeshRenderer[] LEAF, STALK, PETAL;
     
    private AnimatorStateInfo info;
    public Animator _animation_stalk_main_curve;

    void Start ()
    {   
        info = _animation_stalk_main_curve.GetCurrentAnimatorStateInfo(0);

        GameObject[] cx = GetAll(gameObject).ToArray();
        List<MeshRenderer> L = new List<MeshRenderer>();
        List<MeshRenderer> S = new List<MeshRenderer>();
        List<MeshRenderer> P = new List<MeshRenderer>();

        foreach (GameObject c in cx) 
            if (null != c.GetComponent<MeshRenderer>())
            {
                switch (c.tag)
                {
                    case "LEAF": L.Add(c.GetComponent<MeshRenderer>()); break;
                    case "STALK": S.Add(c.GetComponent<MeshRenderer>()); break;
                    case "PETAL": P.Add(c.GetComponent<MeshRenderer>()); break;
                    default: break;
                }
            }  

        LEAF = L.ToArray();
        STALK = S.ToArray();
        PETAL = P.ToArray();

        L = null;
        S = null;
        P = null; 
    }


    void Update ()
    {    

        if (ctrler > v)
        {
            v += (ctrler - v) / 40;
            if (ctrler - v < 0.0001f)
                v = ctrler;
        }

        if (ctrler < v)
        {
            v -= (v - ctrler) / 40;
            if (v - ctrler < 0.0001f)
                v = ctrler;
        }



        if (!(v <= 1 && v >= 0))
            return; 
        foreach (MeshRenderer m in STALK)
            m.materials[0].color = new Color(0.08f + v * 0.67f, (1 - v) * 0.42f + 0.33f, (1 - v) * 0.04f + 0.02f, 1);

        foreach (MeshRenderer m in PETAL)
            m.materials[0].color = new Color(1 - v * 0.8f,1, 1 - v * 0.8f);

        STALK_MAIN.materials[0].color = new Color(0.08f + v * 0.67f, (1 - v) * 0.42f + 0.33f, (1 - v) * 0.04f + 0.02f, 1);
        Water.materials[0].color = new Color((119 + v * 20)/255, (192 - v * 28)/255  , 1 - (v *30) /255, 0.66f);
        Leaf.materials[0].color = Color.Lerp(Color.white, new Color(1, 0.2F, 1), v);
        _animation_stalk_main_curve.Play(info.shortNameHash, -1, v);
    }

     

    public static List<GameObject> GetAll(GameObject obj)
    {
        List<GameObject> allChildren = new List<GameObject>();
        GetChildren(obj, ref allChildren);
        return allChildren;
    }

    public static void GetChildren(GameObject obj, ref List<GameObject> allChildren)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        if (children.childCount == 0)
            return;
        foreach (Transform ob in children)
        {
            allChildren.Add(ob.gameObject);
            GetChildren(ob.gameObject, ref allChildren);
        }
    }


}  

