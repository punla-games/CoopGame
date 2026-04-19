using UnityEngine;
using UnityEngine.InputSystem;

public class Player:MonoBehaviour
{
    public Camera _camera;

    [System.NonSerialized]
    public CharacterController _controller;

    private float pitch = 0f;

    // physics.
    private const float GRAVITY = -19.6f;
    public float fallSpeed = 0f;

    // jumping.
    private const float JUMP_HEIGHT = 1.5f;

    // interaction.
    public BaseInteractable hovered = null;
    public BaseInteractable active = null;
    public float interactTime = 0f;

    // items.
    public Item HeldItem { get; private set; }

    [SerializeField]
    private Transform _heldItemViewHolder;

    private GameObject heldItemView;

    public void Awake()
    {
        _controller=GetComponent<CharacterController>();
    }
    public void Start()
    {
        // hide cursor.
        Cursor.lockState=CursorLockMode.Confined;
        Cursor.visible=false;
    }
    public void Update()
    {
        // falling.
        fallSpeed+=GRAVITY*Time.deltaTime;
        _controller.Move(Vector3.up*fallSpeed*Time.deltaTime);

        // jumping.
        bool jumpInput = Keyboard.current.spaceKey.wasPressedThisFrame;
        if(fallSpeed<=0f&&jumpInput)
        {
            fallSpeed=Mathf.Sqrt(-2f*GRAVITY*JUMP_HEIGHT);
        }

        Look();
        Move();
        Interact();
        Item();
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.normal.y>0.7f&&fallSpeed<=0f)
        {
            fallSpeed=-2f;
        }
    }

    private void Look()
    {
        float lookX = Mouse.current.delta.ReadValue().x;
        float lookY = Mouse.current.delta.ReadValue().y;
        float lookSens = 0.03f;

        float deltaPitch = lookY*lookSens;
        pitch=Mathf.Clamp(pitch-deltaPitch,-85f,85f);
        _camera.transform.localRotation=Quaternion.Euler(pitch,0f,0f);

        float deltaYaw = lookX*lookSens;
        transform.Rotate(Vector3.up,deltaYaw);
    }
    private void Move()
    {
        float forward = Keyboard.current.wKey.isPressed.ToInt()-Keyboard.current.sKey.isPressed.ToInt();
        float strafe = Keyboard.current.dKey.isPressed.ToInt()-Keyboard.current.aKey.isPressed.ToInt();
        bool run = Keyboard.current.leftShiftKey.isPressed;

        float walkSpeed = 4f;
        float runSpeed = 6f;
        float moveSpeed = walkSpeed;
        if(run)
            moveSpeed=runSpeed;

        var moveDir = Quaternion.Euler(0f,transform.eulerAngles.y,0f)*Vector3.ClampMagnitude(new Vector3(strafe,0f,forward),1f);

        var deltaPos = moveDir*moveSpeed*Time.deltaTime;
        _controller.Move(deltaPos);
    }
    private void Interact()
    {
        var interactControl = Keyboard.current.fKey;
        float range = 3f;

        // update the hovered.
        BaseInteractable focus=null;
        float distance = float.MaxValue;

        var hits = Physics.RaycastAll(_camera.transform.position,_camera.transform.forward,range);
        foreach(var hit in hits)
        {
            if(hit.collider.TryGetComponent<BaseInteractable>(out var interactable))
            {
                if(focus==null||hit.distance<distance)
                {
                    focus=interactable;
                    distance=hit.distance;
                }
            }
        }

        hovered=focus;

        // begin interaction.
        if(interactControl.wasPressedThisFrame&&active==null&&hovered!=null&&hovered.CanInteract(this))
        {
            active=hovered;
            active.OnBeginInteract(this);
        }

        // update interaction.
        if(interactControl.isPressed&&hovered==active&&active!=null&&active.CanInteract(this))
        {
            interactTime+=Time.deltaTime;
            if(interactTime>=active.GetInteractDuration(this))
            {
                active.OnInteract(this);
            }
        }

        // end interaction.
        bool endInteract = active!=null&&(hovered!=active||!active.CanInteract(this)||!interactControl.isPressed||interactTime>=active.GetInteractDuration(this));
        if(endInteract)
        {
            active.OnEndInteract(this);
            active=null;
            interactTime=0f;
        }
    }
    private void Item()
    {
        bool drop = Keyboard.current.gKey.isPressed;

        if(HeldItem!=null&&drop)
        {
            HeldItem=null;
        }
    }

    public void HoldItem(Item item)
    {
        if(item!=null)
            DropItem();

        HeldItem=item;
        heldItemView=Instantiate(item.HeldView,_heldItemViewHolder);
    }
    public void DropItem()
    {
        HeldItem=null;
        Destroy(heldItemView);
    }
}