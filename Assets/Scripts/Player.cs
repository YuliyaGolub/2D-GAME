using System;
using UnityEngine;

public class Player : MonoBehaviour 
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    [SerializeField] private int startAmmoCount = 50;
    private int ammoCount;
    public int AmmoCount
    {
        get { return ammoCount; }
        set
        {
            ammoCount = value;
            AmmoCountChanged?.Invoke(ammoCount);
        }
    }

    private Inventory inventory = new Inventory();
    public Inventory Inventory
    {
        get { return inventory; }
        private set
        {
            inventory = value;
        }
    }

    public event Action<int> AmmoCountChanged;

    public IWeapon Weapon { get; private set; }
    public Animator Animator { get; private set; }
    public bool IsFacingRight;
    public bool IsMoving;

    private void Start()
    {
        Weapon = GetComponentInChildren<Weapon>();
        Animator = GetComponent<Animator>();
    }

    public void Init()
    {
        AmmoCount = startAmmoCount;
        Weapon.IsShooting = false;
        IsMoving = false;
    }
}
