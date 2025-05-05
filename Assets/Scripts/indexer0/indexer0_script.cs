using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexer0_script : MonoBehaviour
{
    public GameObject cam;
    public GameObject rainmanager;
    public GameObject[] rainshader;
    public GameObject player;
    public GameObject hitbox;
    public List<string> weapons = new List<string>();
    public int rainmincooltime;
    public int raincool;
    public bool rain = false;
    public bool walk = true;
    public int range;
    public bool whileattack = false;
    public int direction;
    public Vector2 playerposition;
    public float movespeed;
    public int weakrushpower;
    public int rush1power;
    public int rush2power;
    public int backrushpower;
    public int backweakrushpower;

    public void FixedUpdate()
    {
        if (weapons.Count == 0)
        {
            raincool++;
        }

        if (raincool >= rainmincooltime)
        {
            GetComponent<Animator>().SetBool("rain", true);
        }

        Transform playerpos = player.GetComponent<Transform>();
        Transform disabledpos = GetComponent<Transform>();

        if (!whileattack)
        {
            if (playerpos.position.x < disabledpos.position.x)
            {
                direction = -1;
                hitbox.GetComponent<indexer0_hitbox_script>().direction = -1;
            }

            if (playerpos.position.x > disabledpos.position.x)
            {
                direction = 1;
                hitbox.GetComponent<indexer0_hitbox_script>().direction = 1;
            }
        }

        if (!whileattack)
        {
            if (direction < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (direction > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Vector2.Distance(player.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().position) > range)
        {
            gameObject.GetComponent<Animator>().SetBool("walk", true);
            GetComponent<Animator>().SetBool("range", true);
            GetComponent<Animator>().SetBool("melee", false);
            walk = true;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("walk", false);
            GetComponent<Animator>().SetBool("range", false);
            GetComponent<Animator>().SetBool("melee", true);
            walk = false;
        }

        if (!whileattack)
        {
            if (walk)
            {
                playerposition = new Vector2(player.transform.position.x, gameObject.transform.position.y);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerposition, movespeed);
            }
        }

        if (weapons.Contains("katana"))
        {
            GetComponent<Animator>().SetBool("katana", true);
        }

        if (weapons.Contains("rifle"))
        {
            GetComponent<Animator>().SetBool("rifle", true);
        }

        if (weapons.Contains("bigsword"))
        {
            GetComponent<Animator>().SetBool("bigsword", true);
        }

        if (weapons.Contains("spear"))
        {
            GetComponent<Animator>().SetBool("spear", true);
        }

        if (weapons.Contains("shootgun"))
        {
            GetComponent<Animator>().SetBool("shootgun", true);
        }
    }

    public void LookClinet()
    {
        cam.GetComponent<Animator>().SetBool("lookplayer", false);
        cam.GetComponent<Animator>().SetBool("lookclient", true);
    }

    public void LookPlayer()
    {
        cam.GetComponent<Animator>().SetBool("lookplayer", true);
        cam.GetComponent<Animator>().SetBool("lookclient", false);
    }

    public void Startrain()
    {
        rainmanager.GetComponent<indexer0_rainmanager>().DoRain();
        whileattack = true;
    }

    public void Landstart()
    {
        GetComponent<Animator>().SetBool("rainend", false);
        rainmanager.GetComponent<indexer0_rainmanager>().land = true;
        weapons.Add("katana");
        weapons.Add("rifle");
        weapons.Add("bigsword");
        weapons.Add("spear");
        weapons.Add("shootgun");
        raincool = 0;
        GetComponent<Animator>().SetBool("rain", false);
        StartCoroutine(LandTime());
    }

    IEnumerator LandTime()
    {
        yield return new WaitForSeconds(0.08f);
        rainmanager.GetComponent<indexer0_rainmanager>().land = false;
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetBool("landend", true);
        yield return new WaitForSeconds(1f);
        whileattack = false;
        GetComponent<Animator>().SetBool("landend", false);
        
    }

    public void WeakRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * weakrushpower, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * weakrushpower, ForceMode2D.Impulse);

        }
    }

    public void Rush1()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * rush1power, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * rush1power, ForceMode2D.Impulse);

        }
    }

    public void Rush2()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * rush2power, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * rush2power, ForceMode2D.Impulse);

        }
    }

    public void WeakBackRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * backweakrushpower, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * backweakrushpower, ForceMode2D.Impulse);

        }
    }

    public void BackRush()
    {
        if (direction < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * backrushpower, ForceMode2D.Impulse);

        }

        if (direction > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * backrushpower, ForceMode2D.Impulse);

        }
    }

    public void KatanaAttackStart()
    {
        whileattack = true;
    }

    public void KatanaAttackEnd()
    {
        whileattack = false;
        weapons.Remove("katana");
        GetComponent<Animator>().SetBool("katana", false);
    }

    public void RifleAttackStart()
    {
        whileattack = true;
    }

    public void RifleAttackEnd()
    {
        whileattack = false;
        weapons.Remove("rifle");
        GetComponent<Animator>().SetBool("rifle", false);
    }

    public void BigswordAttackStart()
    {
        whileattack = true;
    }

    public void BigswordAttackEnd()
    {
        whileattack = false;
        weapons.Remove("bigsword");
        GetComponent<Animator>().SetBool("bigsword", false);
    }

    public void SpearAttackStart()
    {
        whileattack = true;
    }

    public void SpearAttackEnd()
    {
        whileattack = false;
        weapons.Remove("spear");
        GetComponent<Animator>().SetBool("spear", false);
    }

    public void ShootgunAttackStart()
    {
        whileattack = true;
    }

    public void ShootgunAttackEnd()
    {
        whileattack = false;
        weapons.Remove("shootgun");
        GetComponent<Animator>().SetBool("shootgun", false);
    }

    public void RiflebigswordAttackStart()
    {
        whileattack = true;
    }

    public void RiflebigswordAttackEnd()
    {
        whileattack = false;
        weapons.Remove("rifle");
        weapons.Remove("bigsword");
        GetComponent<Animator>().SetBool("rifle", false);
        GetComponent<Animator>().SetBool("bigsword", false);
    }

    public void SpearShootgunAttackStart()
    {
        whileattack = true;
    }

    public void SpearShootgunAttackEnd()
    {
        whileattack = false;
        weapons.Remove("spear");
        weapons.Remove("shootgun");
        GetComponent<Animator>().SetBool("spear", false);
        GetComponent<Animator>().SetBool("shootgun", false);
    }
}
