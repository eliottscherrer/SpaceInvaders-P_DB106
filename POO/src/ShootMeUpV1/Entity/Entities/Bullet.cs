﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootMeUpV1
{

    public class Bullet : Entity
    {
        public enum Type
        {
            LocalPlayer,
            Enemy,
            SIZE
        }

        public Bullet(Vector2 position, Vector2 direction) : base(position)
        {
            Velocity = direction;
            Rotation = direction.ToAngle() + MathHelper.PiOver2;                     // Adjust with Pi/2 because the texture is pointed up

            AddComponent(new MovementComponent(new BulletMovementLogic(), 500f));   // Speed
            AddComponent(new RenderComponent(Visuals.SwordSlash, 0.065f));          // Texture
            AddComponent(new CollisionComponent(6f));                               // Collision radius

            // TODO: Debug infos
        }

        public override void Update(GameTime gameTime)
        {
            // Destroy if it goes out of bounds
            if (IsOutOfBounds())
                IsDestroyed = true;
        }

        private bool IsOutOfBounds() => !Position.X.IsInRange(0, GameRoot.ScreenSize.X) ||
                                        !Position.Y.IsInRange(0, GameRoot.ScreenSize.Y);

        public override void OnCollision(Entity other)
        {
            // TODO: Collision logic
        }
    }
}
