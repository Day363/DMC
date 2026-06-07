using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InfiniteParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    public Camera _cam;

    private float _spriteWidth;
    
    private void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        _spriteWidth = sr.bounds.size.x;
        _cam = Camera.main;
    }

    

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        transform.localPosition = newPos;

        float camX = _cam.transform.position.x;

        
        if (transform.position.x + _spriteWidth / 2f < camX - _cam.orthographicSize * _cam.aspect)
            transform.position += new Vector3(_spriteWidth * 1.99f, 0, 0);
        else if (transform.position.x - _spriteWidth / 2f > camX + _cam.orthographicSize * _cam.aspect)
            transform.position -= new Vector3(_spriteWidth * 1.99f, 0, 0);
    }
}
