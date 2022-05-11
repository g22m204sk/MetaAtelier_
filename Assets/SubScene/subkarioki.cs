using UnityEngine;

public class subkarioki : MonoBehaviour
{
    void Start() => Invoke("X", SceneLoader.FadeOutTime);
    void X() => SceneLoader.Load("MainScene 1");
}