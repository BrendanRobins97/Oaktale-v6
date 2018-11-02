﻿// File: Player.cs
// Author: Brendan Robinson
// Date Created: 01/03/2018
// Date Last Modified: 07/30/2018
// Description: 

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Controller2D))]
public class Player : Character
{
    public GameObject playerObject;
    public UIHandler uiHandler;
    public ActionBar actionBar;
    public GameObject equip;
    //public DecorationManager decorationManager;
    public bool directionalMining = true;

    public int experience = 0;
    public bool admin = false;
    public bool acceptInput = true;
    public bool acceptClick = true;
    public Item currentItem { get { return inventory.items[actionBar.index]; } }
    public PlayerInventory inventory;
    private Vector2 input = Vector2.zero;
    private float invincibilityDuration = 0.5f;

    public Int2 FeetPosition
    {
        get { return new Int2(transform.position); }
    }

    private void Awake() {
        GameManager.Set(this);
    }
    protected override void Start()
    {
        base.Start();
        inventory.AddItem(new WeaponItem(10001));
        inventory.AddItem(new Item(5001));
        inventory.AddItem(new Item(5101));
        inventory.AddItem(new Item(901), 999);
        inventory.AddItem(new Item(9002), 1, PlayerInventory.bootsIndex);

        if (admin) inventory.AddAllItems();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        HandleMovement();
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        animator.SetFloat("VerticalVelocity", velocity.y);
        animator.SetBool("Airborne", !controller.collisions.below);
    }

    public override void HitCharacter(Vector2 velocity)
    {
        base.HitCharacter(velocity);
        SetInvincible(invincibilityDuration);
    }

    #region Movement

    protected void HandleMovement()
    {
        if (controller.collisions.above || controller.collisions.below) velocity.y = 0;

        if (acceptInput)
        {
            if (acceptClick && currentItem.id != 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButton(0)) ItemDatabase.GetItemData(currentItem).Use1(this);
                if (Input.GetMouseButton(1)) ItemDatabase.GetItemData(currentItem).Use2(this);
            }

            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (input.x > 0 && !facingRight)
            {
                facingRight = true;
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (input.x < 0 && facingRight)
            {
                facingRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) velocity.y = jumpVelocity;
            if (Input.GetKeyDown(KeyCode.LeftShift))
                if (ItemDatabase.GetArmor(inventory.GetBoots().id) != null)
                    ItemDatabase.GetArmor(inventory.GetBoots().id).ability.UseAbility(this);

            if (Input.GetKeyDown(KeyCode.LeftControl)) {
                directionalMining = !directionalMining;
            }
        }
        else
        {
            input = Vector2.zero;
        }

        controller.ignorePlatform = input.y < 0;
        targetVelocity.x = input.x * moveSpeed;

        float smoothTime;

        if (Mathf.Abs(velocity.x) > moveSpeed || !controller.collisions.below)
            smoothTime = accelerationTimeAirborne;
        else
            smoothTime = accelerationTimeGrounded;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref velocityXSmoothing, smoothTime);

        if (affectedByGravity) velocity.y += gravity * Time.deltaTime;
        
        velocity.y = Mathf.Clamp(velocity.y, -maxFallSpeed, float.MaxValue);

        controller.Move(velocity * Time.deltaTime);
    }

    public void Teleport(int x, int y)
    {
        controller.objTransform.position = new Vector2(x, y);
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
        GameManager.Get<ChunkManager>().ResetPools();
    }

    public void Teleport(Int2 pos)
    {
        Teleport(pos.x, pos.y);
    }

    #endregion
}