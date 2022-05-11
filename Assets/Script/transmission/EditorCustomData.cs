#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomData))]
public class EditorCustomData : Editor
{
    public override void OnInspectorGUI()
    {
        CustomData data = target as CustomData;

        EditorGUILayout.IntField("ID",data._myID);
         
        

        data._data = EditorGUILayout.TextField("データ",data._data);
        if (GUILayout.Button("Send"))
        {
            data.GetAllData();
        }

        EditorGUILayout.LabelField("----------------全プレイヤー情報-----------------");

        for(int i = 0;i <CustomData.PlayerList.Count; i++)
        {
            EditorGUILayout.LabelField("Player" + (i + 1).ToString()); 
            EditorGUILayout.IntField("ID", CustomData.PlayerList[i]._myID); 
            EditorGUILayout.TextField("データ", CustomData.PlayerList[i]._data);
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif