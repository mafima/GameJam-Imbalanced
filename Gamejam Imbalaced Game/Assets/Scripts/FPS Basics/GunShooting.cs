using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class GunShooting : Photon.MonoBehaviour {

    public Weapon weapon;
    int damagePerShot;
    float timeBetweenBullets = 0.2f;
    float range = 100.0f;
    public Animator anim;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;

    // Called when script awake in editor
    void Awake() {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (photonView.isMine) {
            timer += Time.deltaTime;

            bool shooting = CrossPlatformInputManager.GetButton("Fire1");

            // RPC call every client "Shoot" function
            if (shooting && timer >= timeBetweenBullets && Time.timeScale != 0) {
                GetComponent<PhotonView>().RPC("Shoot", PhotonTargets.All);
            }

            anim.SetBool("Firing", shooting);
        }
    }

    [PunRPC]
    public void DisableEffects() {
        gunLine.enabled = false;
    }

    // RPC function for shooting
    [PunRPC]
    void Shoot() {
        timer = 0.0f;

        gunAudio.Play();

        gunParticles.Stop();
        gunParticles.Play();

        // set weapon depending stuff:
        damagePerShot=(int)weapon.damage;
        timeBetweenBullets = 1f/(weapon.AtkPerSec+0.001f);
        range=weapon.range;

        // Only call when is the client itself
        if (photonView.isMine) {
            shootRay = Camera.main.ScreenPointToRay(new Vector3((Screen.width * 0.5f), (Screen.height * 0.5f), 0f));
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) {
                switch (shootHit.transform.gameObject.tag) {
                case "Player":
                    shootHit.collider.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, damagePerShot, PhotonNetwork.player.NickName);
                    PhotonNetwork.Instantiate("impacts/impactFlesh", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Metal":
                    PhotonNetwork.Instantiate("impacts/impactMetal", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Glass":
                    PhotonNetwork.Instantiate("impacts/impactGlass", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Wood":
                    PhotonNetwork.Instantiate("impacts/impactWood", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Brick":
                    PhotonNetwork.Instantiate("impacts/impactBrick", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Concrete":
                    PhotonNetwork.Instantiate("impacts/impactConcrete", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Dirt":
                    PhotonNetwork.Instantiate("impacts/impactDirt", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                case "Water":
                    PhotonNetwork.Instantiate("impacts/impactWater", shootHit.point, Quaternion.Euler(shootHit.normal.x - 90, shootHit.normal.y, shootHit.normal.z), 0);
                    break;
                default:
                    break;
                }
            }
        }
    }

    // Synchronize data on the network
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(anim.GetBool("Firing"));
        } else {
            anim.SetBool("Firing", (bool)stream.ReceiveNext());
        }
    }

}

