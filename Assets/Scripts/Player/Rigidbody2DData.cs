using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rigidbody2DData
{
    public SerializableVector2 position;
    public SerializableVector2 velocity;
    public Rigidbody2DData(Rigidbody2D rigidbody2D)
    {
        if (rigidbody2D == null)
            return;
        position = new SerializableVector2(rigidbody2D.position);
        velocity = new SerializableVector2(rigidbody2D.velocity);
    }

    internal void Rigidbody2D(Player player)
    {
        player.rb2d.position = this.position.Vector2();
        player.rb2d.velocity = this.velocity.Vector2();
    }
}
