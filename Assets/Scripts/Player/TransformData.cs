using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class TransformData
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public string tag;
    public string name;

    public TransformData(Transform transform)
    {
        if (transform == null)
        {
            Debug.Log("Transform null");
            return;
        }
        position = new SerializableVector3(transform.position);
        rotation = new SerializableQuaternion(transform.rotation);
        tag = transform.tag;
        name = transform.name;
    }
}
