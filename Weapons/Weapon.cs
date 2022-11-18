using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponUI _weaponUI;


    [SerializeField] float rateOfFire;
    [SerializeField] float reloadSpeed;
    [SerializeField] float shotForce;

    [SerializeField] int amunation;
    public int GetAmunation => amunation;

    


    [SerializeField] GameObject shotLocation;
    
    [SerializeField] Camera cam;


    [SerializeField] int selectedProjectile = 0;
    [SerializeField] List<GameObject> projectiles;
    public void AddProjectile(GameObject projectile)
    {
        foreach (var prj in projectiles)
            if (prj.Equals(projectile))
                return;

        EventsSet.NewProjectileAdded.Invoke(projectile.GetComponent<Projectile>().GetSprite);
        this.projectiles.Add(projectile);
       
    }

    


    
    int roundsUsed = 0;
    bool isReload = false;

    private void Update()
    {
        if (Input.mouseScrollDelta != new Vector2(0,0))
        {
            ChangeProjectile((int)Input.mouseScrollDelta.y);
        }
        
        if (Input.GetMouseButtonDown(0))
            StartCoroutine("Shot");
        if (Input.GetMouseButtonUp(0))
            StopCoroutine("Shot");
    }


    IEnumerator Shot()
    {
        while (true && !isReload)
        {

            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 targetPoint;
                
            targetPoint = ray.GetPoint(20);

            Vector3 dirWithoutSpread = targetPoint - shotLocation.transform.position;

            var projectile = Instantiate(this.projectiles[selectedProjectile], shotLocation.transform.position, Quaternion.identity, transform.parent);

            projectile.transform.parent = null;

            projectile.transform.forward = dirWithoutSpread.normalized;

            projectile.GetComponent<Rigidbody>().AddForce(dirWithoutSpread.normalized * shotForce, ForceMode.Impulse);


            roundsUsed++;
            _weaponUI.SetUIAmunationText(amunation - roundsUsed);
            if (roundsUsed == amunation)
                StartCoroutine("Reload");

            yield return new WaitForSeconds(rateOfFire);
        }


    }


    IEnumerator Reload()
    {
        isReload = true;
        yield return new WaitForSeconds(reloadSpeed);
        roundsUsed = 0;
        isReload = false;
        _weaponUI.SetUIAmunationText(amunation);
    }

    void ChangeProjectile(int value)
    {
        if (value != 1 && value != -1) return;

        selectedProjectile += value;
        if (selectedProjectile < 0)
            selectedProjectile = projectiles.Count - 1;
        else if (selectedProjectile == projectiles.Count)
            selectedProjectile = 0;

        EventsSet.ProjectileChanged.Invoke(selectedProjectile);
    }

}
