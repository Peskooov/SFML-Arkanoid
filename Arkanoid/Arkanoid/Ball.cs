using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Arkanoid;

internal class Ball
{
    public Sprite sprite;

    private float speed;
    private Vector2f direction;

    public int numberOfAttempts = 3;

    public Ball(Texture texture)
    {
        sprite = new Sprite(texture);
    }

    public void Start(float speed, Vector2f direction)
    {
        if (this.speed != 0) return;

        this.speed = speed;
        this.direction = direction;
    }

    public void Move(Vector2i boundsPos, Vector2i boundsSize)
    {
        sprite.Position += direction * speed;

        if (sprite.Position.X > boundsSize.X - sprite.Texture.Size.X || sprite.Position.X < boundsPos.X)
        {
            direction.X *= -1;
        }

        if (sprite.Position.Y < boundsPos.Y)
        {
            direction.Y *= -1;
        }
        if (sprite.Position.Y >= boundsSize.Y)
        {
            direction.Y *= 0;
            numberOfAttempts -= 1;
            sprite.Position = new Vector2f(375, 400);
            speed = 0;
            Program.Hearts();
        }
    }

    public bool CheckCollision(Sprite sprite,string tag)
    {
        if(this.sprite.GetGlobalBounds().Intersects(sprite.GetGlobalBounds())==true)
        {
            if (tag == "Stick")
            {
                direction.Y *= -1;

                float f = ((this.sprite.Position.X + this.sprite.Texture.Size.X * 0.5f) - 
                           (sprite.Position.X + sprite.Texture.Size.X * 0.5f)) / sprite.Texture.Size.X;
                direction.X = f * 2;
            }

            if (tag == "Block")
            {
                direction.Y *= -1;

                float j = ((this.sprite.Position.X + this.sprite.Texture.Size.X * 0.5f) -
                          (sprite.Position.X + sprite.Texture.Size.X * 0.5f)) / sprite.Texture.Size.X;
                direction.X = j ;
            }
            return true;
        }
        return false;
    }
}
