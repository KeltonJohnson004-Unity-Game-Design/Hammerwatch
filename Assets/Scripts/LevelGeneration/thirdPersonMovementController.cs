using UnityEngine;
using UnityEngine.UIElements;

public class thirdPersonMovementController : MonoBehaviour
{

    public float speed;
    private float storeSpeed;
    private Rigidbody rigidBody;
    public HealthBar healthBar;
    
    public int maxHealth;
    public int health;
    public float attackSpeed;
    [SerializeField]
    private float attackCooldown;

    [SerializeField]
    TestMazeConstructor tmc;
    [SerializeField]
    Camera camera;
    private Vector3 cameraOffset;

    public GameObject projectile;

    InputMaster inputMaster;

    [SerializeField]
    Vector2 movementInput;
    Vector2 mouseInput;
    Vector2 tempVect;
    bool canAttack;
    [SerializeField]
    bool attackOnCooldown;

    bool levelBuild;

    public UIController uiController;


    Vector3 lastDirection;
    public bool canExit;

    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputMaster.Player.Rotation.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();
        inputMaster.Player.Pause.performed += _ => uiController.DisplayPauseMenu();
       // inputMaster.Player.BasicAttack.performed += ctx => BasicAttack();
        inputMaster.Player.BuildLevel.performed += _ => Use();
        levelBuild = true;
        health = maxHealth;
        cameraOffset = camera.transform.position - transform.position;
        attackOnCooldown = false;
        attackCooldown = attackSpeed;
        canExit = false;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Start()
    {
        storeSpeed = speed;

         rigidBody = gameObject.GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {

        tempVect = new Vector2(Screen.width / 2, Screen.height / 2);

        float horizontalSpeed = movementInput.x;
        float verticalSpeed = movementInput.y;

        Vector3 targetInput = new Vector3(horizontalSpeed, 0, verticalSpeed);
        transform.Translate(targetInput.normalized * speed * Time.deltaTime, Space.World);


        camera.transform.position = cameraOffset + transform.position;

        Vector3 newDirection;
        if (mouseInput.magnitude > 1.5)
        {
            newDirection = new Vector3(-tempVect.x + mouseInput.x, 0, -tempVect.y + mouseInput.y).normalized;

        }
        else
        {
            newDirection = new Vector3(mouseInput.x, 0, mouseInput.y).normalized;
        }
        if(newDirection.magnitude >.1 )
        {

            lastDirection = newDirection;
        }
        transform.LookAt(newDirection + transform.position);
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else if(attackOnCooldown)
        {
            attackOnCooldown = false;
        }

        if (canAttack && !attackOnCooldown)
        {
            Invoke(nameof(BasicAttack), 0f);
        }


    }

    public void BasicAttack()
    {
        attackOnCooldown = true;
        attackCooldown = attackSpeed;
        Vector3 newDirection;
        if (mouseInput.magnitude > 1.5)
        {
            newDirection = new Vector3(-tempVect.x + mouseInput.x, 0, -tempVect.y + mouseInput.y).normalized;
        }
        else
        {
            newDirection = new Vector3(mouseInput.x, 0, mouseInput.y).normalized;
        }


        //Attack code goes ehre
        if (newDirection.magnitude > 0)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(Vector3.Normalize(newDirection) * 2, ForceMode.Impulse);
        }
        else
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            Debug.Log(lastDirection);
            rb.AddForce(lastDirection.normalized * 2, ForceMode.Impulse);
            
        }

    }

    public void Use()
    {
        if (canExit)
        {
            Invoke(nameof(BuildLevel), 0f);
        }
    }
    public void BuildLevel()
    {
        canExit = false;
        tmc.BuildLevel();
    }

    public void DamagePlayer(int damage)
    {
        if(damage >= health)
        {
            health -= damage;
            healthBar.SetHealth(health);
            inputMaster.Disable();
            Time.timeScale = 0;
            uiController.DisplayGameOver();
        }
        else
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
    }
    private void OnEnable()
    {
        inputMaster.Enable();
        inputMaster.Player.BasicAttack.started += ctx => canAttack = true;
        inputMaster.Player.BasicAttack.performed += ctx => canAttack = true;
        inputMaster.Player.BasicAttack.canceled += ctx => canAttack = false;

    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

}
