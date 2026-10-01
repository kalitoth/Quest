using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_18 : MonoBehaviour
{
    [SerializeField]
    Transform _player;

    Vector3 _offset = new Vector3(0, 3, -8);
    [SerializeField]
    float _sharpness = 7f;
  
    private void FixedUpdate()
    {
        float t = 1 - Mathf.Exp(-_sharpness * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, _player.position + _player.rotation * _offset, t);
        transform.rotation = Quaternion.Lerp(transform.rotation, _player.rotation, t);
    }
}
