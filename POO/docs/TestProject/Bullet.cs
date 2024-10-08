﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ShootMeUp;
using static System.Net.Mime.MediaTypeNames;

namespace ShootMeUp
{
    class Bullet : Entity
    {
        private static Random rand = new Random();

        public Bullet(Vector2 position, Vector2 velocity)
        {
            image = Art.Bullet;
            Position = position;
            Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8;
        }

        public override void Update()
        {
            if (Velocity.LengthSquared() > 0)
                Orientation = Velocity.ToAngle();

            Position += Velocity;
            GameRoot.Grid.ApplyExplosiveForce(0.5f * Velocity.Length(), Position, 80);

            // delete bullets that go off-screen
            if (!GameRoot.Viewport.Bounds.Contains(Position.ToPoint()))
            {
                IsExpired = true;

                for (int i = 0; i < 30; i++)
                    GameRoot.ParticleManager.CreateParticle(Art.LineParticle, Position, Color.LightBlue, 50, 1,
                        new ParticleState() { Velocity = rand.NextVector2(0, 9), Type = ParticleType.Bullet, LengthMultiplier = 1 });

            }
        }
    }
}
