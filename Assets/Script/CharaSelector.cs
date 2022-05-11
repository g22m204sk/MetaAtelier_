using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelector : MonoBehaviour
{
    [Range(0,1),SerializeField]
    int avator;
    /// <summary>
    /// ���g�̃Z�b�g
    /// </summary>
    public void Start()
    {
        SetChara(AvatorSelect_ver_A.select, transform);
    }

    /// <summary>
    /// ���̃��[�U���������ꂽ�ۂ͂�����...
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

        // �ǋL�@�������g�������Ȃ��悤�ɔ�\��
        MonobitEngine.MonobitView view = GetComponentInParent<MonobitEngine.MonobitView>();
        if (view.isMine) f.SetActive(false);
    }

}

