using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelector : MonoBehaviour
{
    [Range(0,1),SerializeField]
    int avator;
    /// <summary>
    /// 自身のセット
    /// </summary>
    public void Start()
    {
        SetChara(AvatorSelect_ver_A.select, transform);
    }

    /// <summary>
    /// 他のユーザが生成された際はこいつを...
    /// </summary>
    /// <param name="v"></param>
    /// <param name="root"></param>
    public void SetChara(int v, Transform root)
    {

        var i = Resources.Load("CHARA" + v);
        GameObject f = Instantiate(i as GameObject);
        f.transform.parent = root;
        Vector3[] pos = new Vector3[3]
        {
            new Vector3(0,-0.676f,0),
            new Vector3(0,0.12f,0),
            new Vector3(),
        };
        float[] spin = { 0, 0, 0 };
        f.transform.localPosition = pos[v];
        f.transform.localRotation = Quaternion.Euler(0, spin[v],0);

        // 追記　自分自身が見えないように非表示
        MonobitEngine.MonobitView view = GetComponentInParent<MonobitEngine.MonobitView>();
        if (view.isMine) f.SetActive(false);
    }

}

