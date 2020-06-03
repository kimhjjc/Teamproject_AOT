using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform mainCam;
    Rigidbody m_rigidbody;
    Animator animator;
    WireAction wireAction;

    Vector3 moveDirection;

    float speed;
    float jumpPower;

    Transform weapon;
    Transform getItemParent;

    float AttackCooltime;
    float RollCooltime;
    float ObjectCooltime;

    bool isGrounded;

    void Start()
    {
        mainCam = transform.GetChild(1);
        m_rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Move", false);
        animator.SetBool("JumpAble", false);
        animator.SetBool("Dash", false);
        wireAction = GetComponent<WireAction>();

        moveDirection = Vector3.zero;

        jumpPower = 7;

        weapon = GameObject.Find("Weapon").transform;
        getItemParent = null;
        AttackCooltime = 0.0f;
        RollCooltime = 0.0f;
        ObjectCooltime = 0;

        isGrounded = false;
    }

    void Update()
    {
        CooltimeManager();

        //Move();
        Attack();
    }

    public void Move()
    {
        animator.SetBool("Dash", false);
        animator.SetBool("Move", false);


        // --------------------------------------------------------

        // wire를 사용중인 경우
        if (wireAction.IsWireAction())
        {
            animator.SetBool("JumpAble", false);        // 와이어를 사용할 때 애니메이션. 이건 아직 적용안됨.
            return;
        }
        else
            animator.SetBool("JumpAble", true);

        // wire탄 후 슬라이딩 중인 경우
        if (wireAction.IsSliding())
            return;

        // 이동 속도
        speed = 10.0f;

        // 달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.6f;
            animator.SetBool("Dash", true);
        }

        // Ground에서의 이동
        Vector3 curVelocity = m_rigidbody.velocity;

        curVelocity.x = 0.0f;
        curVelocity.z = 0.0f;
        m_rigidbody.velocity = curVelocity;

        Vector3 addVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            addVelocity += (transform.forward);
        if (Input.GetKey(KeyCode.S))
            addVelocity += (-transform.forward);
        if (Input.GetKey(KeyCode.A))
            addVelocity += (-transform.right);
        if (Input.GetKey(KeyCode.D))
            addVelocity += (transform.right);

        addVelocity = addVelocity.normalized * speed;
        m_rigidbody.velocity += addVelocity;


        if (addVelocity.x != 0.0f || addVelocity.z != 0.0f)
        {
            animator.SetBool("Move", true);

            // 플레이어가 이동하는 방향으로 회전
            this.transform.GetChild(0).rotation = Quaternion.Slerp(this.transform.GetChild(0).rotation, Quaternion.LookRotation(addVelocity), 0.6f);
        }

        // --------------------------------------------------------------------

        //    if (IsGrounded() && !IsRoll())
        //    {
        //        speed = 5.0f;
        //        if (Input.GetKey(KeyCode.LeftShift))
        //        {
        //            speed *= 1.6f;
        //            animator.SetBool("Dash", true);
        //        }

        //        if (Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f)
        //        {
        //            Vector3 temp = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //            //moveDirection = 메인카메라가 보는 방향
        //            moveDirection = temp.z * mainCam.forward + temp.x * mainCam.right;
        //            moveDirection.y = 0.0f;

        //            //누른 키 방향으로 플레이어 방향 전환
        //            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), 0.6f);
        //        }

        //        if (Input.GetButton("Jump"))
        //        {
        //            moveDirection.y = jumpPower;
        //            animator.SetBool("JumpAble", false);
        //        }
        //        else if (Input.GetKeyDown(KeyCode.LeftControl))
        //        {
        //            Roll();
        //        }

        //    }

        //    //이동
        //    if (IsRoll())
        //        speed *= 1.02f;

        //    if (IsRoll() || !IsGrounded() ||
        //        (Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f))
        //    {
        //        //this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //        if (rb.velocity.magnitude < speed)
        //            rb.velocity += moveDirection.normalized * speed * 5.0f * Time.deltaTime;
        //    }
        //    else
        //        rb.velocity = Vector3.zero;


        //    if (IsGrounded())
        //        animator.SetFloat("Move", rb.velocity.magnitude);
        //    else
        //        animator.SetFloat("Move", 0);

    }

    bool IsGrounded()
    {
        if (isGrounded)
            return true;
        else
            return false;
    }

    //void Roll()
    //{
    //    animator.Play("Roll");
    //    if (!IsGrounded() || IsRoll())
    //        return;
    //    if (RollCooltime > -1.0f) // 1.5초 구르기 재사용 대기시간
    //        return;

    //    //animator.SetBool("Roll", true);
    //    RollCooltime = 0.50f; // 구르는 시간
    //}

    //bool IsRoll()
    //{
    //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
    //        animator.SetBool("Roll", false);

    //    if (RollCooltime == 0.0f)
    //        rb.velocity = Vector3.zero;

    //    if (RollCooltime <= 0.0f)
    //        return false;
    //    else
    //        return true;
    //}

    void Attack()
    {
        //animator.SetBool("Attack", false);

        if (Input.GetMouseButtonDown(0) && AttackCooltime <= 0.0f)
        {
            //animator.SetBool("Attack", true);
            animator.Play("Attack1");
            AttackCooltime = 0.5f;
        }

    }

    void CooltimeManager()
    {
        if (AttackCooltime > 0.0f)
            AttackCooltime -= Time.deltaTime;

        if (RollCooltime > -1.0f)
            RollCooltime -= Time.deltaTime;

        if (ObjectCooltime > 0.0f)
            ObjectCooltime -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("JumpAble", true);
        }

        if (Input.GetKeyDown(KeyCode.F) && getItemParent && ObjectCooltime <= 0)
        {
            Vector3 temp = transform.position - new Vector3(0, transform.position.y, 0);
            weapon.GetChild(0).position = temp + transform.forward * 3.0f;
            weapon.GetChild(0).rotation = Quaternion.identity;
            weapon.GetChild(0).gameObject.tag = "Object";
            weapon.GetChild(0).SetParent(getItemParent);
            getItemParent = null;

            ObjectCooltime = 0.2f;
        }
        if (other.gameObject.tag == "Object" && Input.GetKeyDown(KeyCode.F) && !getItemParent && ObjectCooltime <= 0)
        {
            getItemParent = other.transform.parent;
            other.transform.SetParent(weapon);
            other.transform.localPosition = Vector3.zero;
            other.transform.localEulerAngles = Vector3.zero;
            other.gameObject.tag = "Item";

            ObjectCooltime = 0.2f;
        }
    }
}