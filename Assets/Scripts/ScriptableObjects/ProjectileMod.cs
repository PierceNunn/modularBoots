using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileMod : GenericMod
{
    [SerializeField] private GameObject[] projectiles;

    public GameObject[] Projectiles { get => projectiles; set => projectiles = value; }

    public override string DetailedInfoString()
    {
        string output = "";
        foreach(GameObject g in projectiles)
        {
            ProjectileController p = g.GetComponent<ProjectileController>();
            output = output + "Projectile Speed: " + p.ProjectileSpeed + "\n Projectile Damage: " + p.ProjectileSpeed;
        }
        return output + "\n" + base.DetailedInfoString();
    }

    public (string, float) DetailedPostModInfoString(Queue<BasicStatModifier> mods, float cooldown = -1)
    {
        string output = "";
        float c = cooldown;
        foreach (GameObject g in projectiles)
        {
            ProjectileController p = Instantiate(g).GetComponent<ProjectileController>(); //instantiate projectile so mods aren't added directly to prefab
            c = p.ApplyModifiers(mods, c);
            output = output + "Projectile Speed: " + p.ProjectileSpeed + "\n Projectile Damage: " + p.ProjectileSpeed;
            DestroyImmediate(p.gameObject); //destroy instantiated projectile so it doesn't actually do anything irl
        }
        return (output + "\n", c); //+ base.DetailedInfoString();
    }
}
