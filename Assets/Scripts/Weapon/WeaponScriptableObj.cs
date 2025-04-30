using UnityEngine;

[CreateAssetMenu(menuName = "GameData/WeaponData")]
public class WeaponScriptableObj : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    [SerializeField]
    GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get => projectilePrefab; private set => projectilePrefab = value; }
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField]
    float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }
    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
    [SerializeField]
    int range;
    public int Range { get => range; private set => range = value; }

}
