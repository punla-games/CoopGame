using UnityEngine;
using UnityEngine.InputSystem;

public class Player:MonoBehaviour
{
    public enum State
    {
        FREE,
        INTERACT,
    }

    public State state = State.FREE;

    public Transform _cameraRig;
    public Camera _camera;

    [System.NonSerialized]
    public CharacterController _controller;

    private float pitch = 0f;

    // physics.
    private const float GRAVITY = -19.6f;
    public float fallSpeed = 0f;

    // jumping.
    private const float JUMP_HEIGHT = 1.1f;

    // interaction.
    public Interactable hovered = null;
    public Interactable active = null;
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
        // physics.
        fallSpeed+=GRAVITY*Time.deltaTime;
        _controller.Move(Vector3.up*fallSpeed*Time.deltaTime);

        // state machine.
        if(state==State.FREE)
        {
            // jumping.
            bool jumpInput = Keyboard.current.spaceKey.wasPressedThisFrame;
            if(fallSpeed<=0f&&jumpInput)
            {
                fallSpeed=Mathf.Sqrt(-2f*GRAVITY*JUMP_HEIGHT);
            }

            // crouching.
            float targetHeight = 1.7f;

            bool crouchInput = Keyboard.current.ctrlKey.isPressed;
            if(crouchInput)
                targetHeight=0.85f;

            _controller.height=Mathf.Lerp(_controller.height,targetHeight,10f*Time.deltaTime);
            _controller.center=Vector3.up*_controller.height/2f;

            _cameraRig.localPosition=Vector3.up*_controller.height;

            Look();
            Move();
            Interact();
            Item();
        }
        else if(state==State.INTERACT)
        {
            // do nothing.
        }
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
        float forwardInput = Keyboard.current.wKey.isPressed.ToInt()-Keyboard.current.sKey.isPressed.ToInt();
        float strafeInput = Keyboard.current.dKey.isPressed.ToInt()-Keyboard.current.aKey.isPressed.ToInt();
        bool sprintInput = Keyboard.current.leftShiftKey.isPressed;

        float targetFOV = 60f;

        float walkSpeed = 4f;
        float runSpeed = 6f;
        float moveSpeed = walkSpeed;
        if(sprintInput)
        {
            moveSpeed=runSpeed;
            targetFOV=70f;
        }

        _camera.fieldOfView=Mathf.Lerp(_camera.fieldOfView,targetFOV,10f*Time.deltaTime);

        var moveDir = Quaternion.Euler(0f,transform.eulerAngles.y,0f)*Vector3.ClampMagnitude(new Vector3(strafeInput,0f,forwardInput),1f);

        var deltaPos = moveDir*moveSpeed*Time.deltaTime;
        _controller.Move(deltaPos);
    }
    private void Interact()
    {
        float range = 3f;

        // update the hovered.
        Interactable focus=null;
        float distance = float.MaxValue;

        var hits = Physics.RaycastAll(_camera.transform.position,_camera.transform.forward,range);
        foreach(var hit in hits)
        {
            if(hit.collider.TryGetComponent<Interactable>(out var interactable))
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
        if(Keyboard.current.fKey.wasPressedThisFrame&&active==null&&hovered!=null&&hovered.CanInteract(this))
        {
            active=hovered;
            active.OnBeginInteract(this);
        }

        // update interaction.
        if(Keyboard.current.fKey.isPressed&&hovered==active&&active!=null&&active.CanInteract(this))
        {
            interactTime+=Time.deltaTime;
            if(interactTime>=active.GetInteractDuration(this))
            {
                active.OnInteract(this);
            }
        }

        // end interaction.
        if(state==State.FREE)
        {
            bool endInteract = active!=null&&(hovered!=active||!active.CanInteract(this)||!Keyboard.current.fKey.isPressed||interactTime>=active.GetInteractDuration(this));
            if(endInteract)
            {
                active.OnEndInteract(this);
                active=null;
                interactTime=0f;
            }
        }
    }
    private void Item()
    {
        bool dropInput = Keyboard.current.gKey.isPressed;

        if(HeldItem!=null&&dropInput)
        {
            DropItem();
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
        if(heldItemView!=null)
        {
            Destroy(heldItemView);
        }
    }

    public void GainMoney(int amount)
    {
        GameManager.Get.money+=amount;
        PlayerHUD.Get.ShowTipEarned();
    }

    public void StopInteraction()
    {
        active.OnEndInteract(this);
        active=null;
        interactTime=0f;
        state=State.FREE;
    }
}