using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class GunFirstPersonView : MonoBehaviour {

public Weapon weapon;
    float timeBetweenBullets = 0.2f;
     float range = 100.0f;
    public Transform moveme;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    Vector3 originalpos;
    Quaternion orginalrot;

    // Called when script awake in editor
    void Awake() {
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        originalpos=moveme.transform.localPosition;
        orginalrot=moveme.transform.localRotation;
        //updateRecoil();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        bool shooting = CrossPlatformInputManager.GetButton("Fire1");

        if (shooting && timer >= timeBetweenBullets && Time.timeScale != 0) {

        Shoot();
        }

        //anim.SetBool("Firing", shooting);
    }

    // Disable the shooting effects
    public void DisableEffects() {
        gunLine.enabled = false;
    }

    // Shoot!
    void Shoot() {
        // set weapon depending stuff:
        timeBetweenBullets = 1f/((float)weapon.AtkPerSec+0.001f);
        range=weapon.range;
        timer = 0.0f;
        gunParticles.Stop();
        gunParticles.Play();
        updateRecoil();
    }
    IEnumerator updateRecoil (){
        Quaternion randomrot=Random.rotation;
        randomrot.y=Quaternion.Euler(0,0,0).y;

        moveme.localPosition-=Vector3.forward*weapon.damage;
        moveme.localPosition-=Vector3.forward*weapon.damage*0.1f;
        moveme.rotation = Quaternion.Slerp(moveme.rotation,randomrot,0.01f*weapon.damage/(weapon.damage+100));
        yield return new WaitForSeconds(timeBetweenBullets/10f);
        moveme.localPosition-=Vector3.forward*weapon.damage*0.1f;
        moveme.rotation = Quaternion.Slerp(moveme.rotation,randomrot,0.01f*weapon.damage/(weapon.damage+100));
        yield return new WaitForSeconds(timeBetweenBullets/10f);
        for (int i =0;i<8;i++){
            moveme.localPosition = Vector3.Lerp(moveme.localPosition,originalpos,0.1f);
            moveme.rotation = Quaternion.Slerp(moveme.rotation,randomrot,0.01f*weapon.damage/(weapon.damage+100));
            yield return new WaitForSeconds(timeBetweenBullets/10f);
        }
        moveme.localPosition=originalpos;
        moveme.localRotation=orginalrot;
        yield return null;
    }

}

