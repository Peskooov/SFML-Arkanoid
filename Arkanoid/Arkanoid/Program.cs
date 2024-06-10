using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Arkanoid
{
    internal class Program
    {
        static RenderWindow window;

        static Font font = new Font("Andre_V.ttf");

        static Text textMenu;
        static Text textMenuInfo;

        static Texture backgroundTexture;
        static Texture ballTexture;
        static Texture stickTexture;
        static Texture blockTexture;
        static Texture blockEasyTexture;
        static Texture heartTexture;

        static Sprite background;
        static Sprite stick;
        static Sprite infoBlock;
        static Sprite infoBlockEasy;
        static Sprite[] heart;
        static Sprite[] blocks;
        static Sprite[] blocksEasy;

        static Ball ball;

        static int[] blocksHP;

        static int lvl;
        static int blockCount;
        static int blockInLine;
        static int blockOnWindow;
        public static int heartCount;
        public static void LoadTexture()
        {
            
            
            backgroundTexture = new Texture("Background.png");
            ballTexture = new Texture("Ball.png");
            stickTexture = new Texture("Stick.png");
            blockTexture = new Texture("Block2.png");
            blockEasyTexture = new Texture("Block.png");
            heartTexture = new Texture("Heart.png");

            background = new Sprite(backgroundTexture);
            ball= new Ball(ballTexture);
            stick= new Sprite(stickTexture);
            infoBlock= new Sprite(blockTexture);
            infoBlockEasy= new Sprite(blockEasyTexture);
            heart = new Sprite[heartCount];
            for(int i= 0; i < heart.Length;i++) heart[i] = new Sprite(heartTexture);
            blocks = new Sprite[blockCount];
            for (int i = 0; i < blocks.Length; i++)
            {
                 blocks[i] = new Sprite(blockTexture);
            }
            blocksEasy = new Sprite[blockCount];
            for (int i = 0; i < blocksEasy.Length; i++)
            {
                blocksEasy[i] = new Sprite(blockEasyTexture);
            }
        }
        public static void SetStartPosition()
        {
            blocksHP=new int[blockCount];
            int index = 0;
            int ind = 0;

            for(int y = 0; y < blockInLine; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    blocks[index].Position = new Vector2f(x * (blocks[index].TextureRect.Width + 15) + 80, 
                                                          y * (blocks[index].TextureRect.Height + 15) + 50);
                    blocksHP[index] = 2;
                    index++;
                }
            }

            for (int y = 0; y < blockInLine; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    blocksEasy[ind].Position = new Vector2f(x * (blocksEasy[ind].TextureRect.Width + 15) + 80,
                                                          y * (blocksEasy[ind].TextureRect.Height + 15) + (50 * blockInLine));
                    ind++;
                }
            }

            for (int x = 0; x < heart.Length; x++)
            {
                heart[x].Position = new Vector2f(x * heart[x].TextureRect.Width + 10, 5);
            }
 
            stick.Position=new Vector2f(400,500);
            ball.sprite.Position = new Vector2f(375, 400);
        }
        public static void SetStickControl()
        {
            stick.Position = new Vector2f(Mouse.GetPosition(window).X - stick.TextureRect.Width * 0.5f, stick.Position.Y);
        }
        public static void Hearts()
        {
            heart[heartCount - 1].Position = new Vector2f(1000, 1000);
            heartCount -= 1;
        }
        public static void MainMenu()
        {
            textMenu = new Text("      Выберите Уровень:" + "\n" + "\n" +
                                "Уровень 1: 40 блоков 5 попыток" + "\n" +
                                "Уровень 2: 60 блоков 4 попытки" + "\n" +
                                "Уровень 3: 100 блоков 3 попытки", font, 27)
            {
                Color = new Color(255, 255, 145),
                Position = new Vector2f(170, 200)
            };

            textMenuInfo=new Text("Правила игры:" + "\n" + 
                                  "Управляйте платформой с помошью мыши" + "\n" + "Используйте ЛКМ для старта" + "\n" + "\n" + 
                                  "- Для уничтожения необходимо 2 попадания" + "\n" + "- Для уничтожения достаточно 1 попадания", font,20)
            {
                Color = new Color(255, 255, 145),
                Position = new Vector2f(220, 20)
            };

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1) == true|| Keyboard.IsKeyPressed(Keyboard.Key.Numpad1) == true) lvl = 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2) == true|| Keyboard.IsKeyPressed(Keyboard.Key.Numpad2) == true) lvl = 2;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3) == true|| Keyboard.IsKeyPressed(Keyboard.Key.Numpad3) == true) lvl = 3;

            if (lvl == 1)
            {
                blockInLine = 2;
                blockCount = 20;
                ball.numberOfAttempts = 5;
                heartCount = ball.numberOfAttempts;
                blockOnWindow = blockCount * 2;
            }
            if (lvl == 2)
            {
                blockInLine = 3;
                blockCount = 30;
                ball.numberOfAttempts = 4;
                heartCount = ball.numberOfAttempts;
                blockOnWindow = blockCount * 2;
            }
            if (lvl == 3)
            {
                blockInLine = 5;
                blockCount = 50;
                ball.numberOfAttempts = 3;
                heartCount = ball.numberOfAttempts;
                blockOnWindow = blockCount * 2;
            }
        }
        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(800, 600), "Arkanoid");
            window.SetFramerateLimit(60);
            window.Closed += Window_Closed;

            LoadTexture();
            SetStartPosition();

            while (window.IsOpen == true)
            {
                while (lvl == 0)
                {
                    window.Clear();

                    window.DispatchEvents();

                    window.Draw(background);

                    MainMenu();
                    LoadTexture();
                    SetStartPosition();

                    infoBlock.Position = new Vector2f(150, 120);
                    infoBlockEasy.Position = new Vector2f(150, 145);

                    window.Draw(infoBlock);
                    window.Draw(infoBlockEasy);
                    window.Draw(textMenu);
                    window.Draw(textMenuInfo);

                    window.Display();              
                }
                while (lvl != 0 && lvl != -1)
                {
                    window.Clear();

                    window.DispatchEvents();

                    if (Mouse.IsButtonPressed(Mouse.Button.Left) == true)
                    {
                        ball.Start(5, new Vector2f(0, -1));
                    }

                    if (heartCount == 0||blockOnWindow == 0) lvl = -1;

                    ball.Move(new Vector2i(0, 0), new Vector2i(800, 600));

                    //Draw
                    window.Draw(background);
                    window.Draw(ball.sprite);
                    window.Draw(stick);

                    for (int i = 0; i < heart.Length; i++)
                    {
                        window.Draw(heart[i]);
                    }
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        window.Draw(blocks[i]);
                    }

                    for (int i = 0; i < blocksEasy.Length; i++)
                    {
                        window.Draw(blocksEasy[i]);
                    }

                    ball.CheckCollision(stick, "Stick");
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (ball.CheckCollision(blocks[i], "Block") == true)
                        {
                            if (blocksHP[i] == 1)
                            {
                                blocks[i].Position = new Vector2f(1000, 1000);
                                blockOnWindow--;
                                break;
                            }
                            else
                            {
                                blocksHP[i]--;
                            }                         
                        }
                    }
                    for (int i = 0; i < blocksEasy.Length; i++)
                    {
                        if (ball.CheckCollision(blocksEasy[i], "Block") == true)
                        {
                                blocksEasy[i].Position = new Vector2f(1000, 1000);
                                blockOnWindow--;
                                break;
                        }
                    }

                    SetStickControl();

                    window.Display();
                }
                while (lvl == -1)
                {
                    window.Clear();

                    window.DispatchEvents();

                    window.Draw(background);

                    if (heartCount == 0)
                    {
                        if (Keyboard.IsKeyPressed(Keyboard.Key.R) == true ) lvl = 0;                     

                        textMenu = new Text("              Проигрыш!!!" + "\n" + "\n" + 
                                            "Для возврата в главное меню нажмите \"R\"", font, 27)
                        {
                            Color = new Color(255, 255, 145),
                            Position = new Vector2f(150, 200)
                        };
                        window.Draw(textMenu);
                    }
                    if (blockOnWindow == 0)
                    {
                        if (Keyboard.IsKeyPressed(Keyboard.Key.R) == true) lvl = 0;

                        textMenu = new Text("              Победа!!!" + "\n" + "\n" +
                                            "Для возврата в главное меню нажмите \"R\"", font, 27)
                        {
                            Color = new Color(255, 255, 145),
                            Position = new Vector2f(150, 200)
                        };
                        window.Draw(textMenu);
                    }
                    window.Display();
                }
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}