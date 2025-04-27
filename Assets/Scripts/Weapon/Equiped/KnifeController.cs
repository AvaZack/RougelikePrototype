using UnityEngine;

public class KnifeController : RangedWeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = transform.position;
        Knife knife = newObject.GetComponent<Knife>();
        knife.speed = speed;
        knife.pierce = pierce;
        knife.dir = Input.mousePosition - transform.position;
    }

}
