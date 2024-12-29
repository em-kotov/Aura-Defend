using UnityEngine;
using System.Collections;

public class AdaptiveFollower : Enemy
{
    protected override void Update()
    {
        CheckHealth();

        transform.position = Vector3.Lerp(transform.position, Hero.transform.position, Speed * Time.deltaTime);
    }

    override public void Initialize(Color color, float alpha, Hero hero)
    {
        base.Initialize(color, alpha, hero);
        Speed = 0.3f;
    }
}
