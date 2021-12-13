using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    Transform playerTransform;
    public float rotationSpeed = 30f;
    public float delay;
    Transform turrentTransform;
    Transform bulletSpawnPoint= null;
    public float gradesToShoot;

    public float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;

    float timeNextShoot;


    void Start()
    {
        turrentTransform = transform.GetChild(0);
        bulletSpawnPoint = turrentTransform.GetChild(0);
        timeNextShoot = delay;
        playerTransform = GameManager.Instance.player.transform;
    }

    void Update()
    {
        timeNextShoot -= Time.deltaTime;
        if (timeNextShoot < 0f)
        {
            Vector3 dir = playerTransform.position - turrentTransform.position;
            dir.Normalize();

            float gradesDif = Vector2.Angle(dir, turrentTransform.up);

            if(gradesDif < gradesToShoot)
            {
                GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                Destroy(go, 5f);
                timeNextShoot = delay;
            }

            //turrentTransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, dir), rotationSpeed * Time.deltaTime);
        }
    }

}
