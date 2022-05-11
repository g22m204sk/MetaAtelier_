using UnityEngine;
 
public class PlayerMove : MonoBehaviour
{
    public bool LocalPlayer;
    Rigidbody rigitBody;
    float x;
    Vector2 input_;
    public Transform camera_;
    public bool MoveReverseX, MoveReverseY;
    public float moveSpeed;
    public Vector2 cameraRotationSensivirity;
     
    public CameraInputMinimumThreshold cameraInputMinimumThreshold;
    public PlayerSightRange XaxisSightRange; 
    public static PlayerMove player;

    public bool isDebugJoyStick;

    //同期用に追加
    MonobitEngine.MonobitView _monobitView;
    private void Awake()
    {
        if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            _monobitView = GetComponentInParent<MonobitEngine.MonobitView>();

        else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)// 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
            _monobitView = GetComponentInChildren<MonobitEngine.MonobitView>();

        else// 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
            _monobitView = GetComponent<MonobitEngine.MonobitView>();

        if (_monobitView == null)
            _monobitView = this.gameObject.AddComponent<MonobitEngine.MonobitView>();
    }

    void Start()
    {
        gameObject.AddComponent<AutoReturnStage>();
       
        rigitBody = GetComponent<Rigidbody>();
        rigitBody.isKinematic = false;
        rigitBody.useGravity = true;
        rigitBody.constraints = RigidbodyConstraints.FreezeRotation;
        if (OS_.isPC() && !isDebugJoyStick)
        {
            DestroyImmediate(GameObject.Find("SP_UI"));
        }
        else
        {
            cameraInputMinimumThreshold.x *= 1.5f;
            cameraInputMinimumThreshold.y *= 1.5f;
            cameraRotationSensivirity /= 3;
        }

        if (!_monobitView.isMine) Destroy(camera_.gameObject);
    }

    void Update()
    {
        if (!_monobitView.isMine&&!LocalPlayer) return;

        if(player == null) player = this;

        // 追記　チャット中は動かないように
        if (ChatScript._IsChat) return;


        bool os_PC = OS_.isPC();
 
        Vector3 mov = new Vector3();

        if (os_PC)
        {
           mov = new Vector3
           (
                   (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? 1 : (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? -1 : 0)) * (MoveReverseX ? -1 : 1),
                   0,
                   (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? -1 : 0)) * (MoveReverseY ? -1 : 1)
           ) ; 
        }
        else
        {
           mov  = new Vector3(JoyStickUI_Manager.joyL.x, 0, JoyStickUI_Manager.joyL.y);
        }
        rigitBody.MovePosition(rigitBody.position + (transform.forward * mov.z+transform.right * mov.x) * moveSpeed * Time.deltaTime);

        //回転量入力入力及び手振れ防止のため小さい値なら0にする------------------------------
        input_ = os_PC ? new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) : new Vector2(JoyStickUI_Manager.joyR.x, JoyStickUI_Manager.joyR.y);
        if (Mathf.Abs(input_.x) < cameraInputMinimumThreshold.x) input_.x = 0;
        if (Mathf.Abs(input_.y) < cameraInputMinimumThreshold.y) input_.y = 0;

        //回転------------------------------------------------------------------------
        transform.Rotate(input_.x * Vector3.up * Time.deltaTime * cameraRotationSensivirity.y, Space.Self);
        x += input_.y * Time.deltaTime * cameraRotationSensivirity.x;
        x = x > XaxisSightRange.upper ?
            XaxisSightRange.upper-1 :
            (x < XaxisSightRange.downer ? XaxisSightRange.downer + 1 : x); //x回転は+-90程度がいい
        camera_.localRotation = Quaternion.Euler(x * Vector3.left);
       
    }

    private void OnDestroy()
    {
        player = null;
    }
}


[System.Serializable]
public class PlayerSightRange
{
    [Range(0,  90)] public float upper;
    [Range(0, -90)] public float downer;
}
 

[System.Serializable]
public class CameraInputMinimumThreshold
{
    [Range(0.0001f, 0.2f)] public float x,y; 
}



public static class OS_
{
    public static bool isPC() { return Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxPlayer; }//  Application.absoluteURL.Replace(" ","").Contains("?os=p"); } 
}