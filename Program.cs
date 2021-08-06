using Raylib_cs;
using System.Numerics;

namespace Gamerpg
{
    public static class Program
    {
        //cam
        private static Camera2D camera;

        //play
        private static Rectangle player;
        private static Vector2 playerPosition = new Vector2(400, 400);
        private static Texture2D playerTex;

        private static Texture2D screenBack;

        private static bool isJumping = false;
        private static bool hasJumped = false;
        private static bool isMoving, isMovingUp, isMovingDown, isMovingLeft, isMovingRight;
        private static bool isUsing = false;
        private static bool gameRunning = false;
        private static float playerRotation = 0;
        private static float gravityAmount = 1;

        private static float jumpDist = 5;
        private static float movementDist = 3;

        private static Music mainTheme;
        private static bool musicIsPlaying = false;

        public static void Main(string[] args)
        {
            Raylib.InitWindow(800, 600, "Woodcutter Adventures");
            //load player texture
            playerTex = Raylib.LoadTexture(@"C:\Users\joshu\source\repos\Gamerpg(develop)\Game assets\Main character sprites\Woodcutter\Woodcutter.png");
            playerTex.height = 150;
            playerTex.width = 150;

            screenBack = Raylib.LoadTexture(@"C:\Users\joshu\source\repos\Gamerpg(develop)\Game assets\Backgrounds\Battleground1\Bright\Battleground1.png");
            screenBack.height = 600;
            screenBack.width = 800;

            mainTheme = Raylib.LoadMusicStream(@"C:\Users\joshu\source\repos\Gamerpg\Game assets\Music\main (OGG).ogg");

            while (!Raylib.WindowShouldClose())
            {
                gameRunning = true;
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                Raylib.DrawFPS(10, 10);
                Raylib.DrawText("This is a rpg, based on a wood cutter main character.", 30, 50, 30, Color.BLACK);

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_M) && musicIsPlaying == false) {
                    Raylib.PlayMusicStream(mainTheme);
                    musicIsPlaying = true;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_N) && musicIsPlaying == true) {
                    Raylib.StopMusicStream(mainTheme);
                    musicIsPlaying = false;
                }

                UpdatePlayer();
                if (gameRunning == true)
                {
                    playerPosition.Y += gravityAmount;
                }

                Raylib.EndMode2D();
                Raylib.EndDrawing();
            }

            gameRunning = false;
            Raylib.CloseWindow();
        }

        private static void UpdatePlayer() {
            Raylib.DrawTexture(playerTex, (int)playerPosition.X, (int)playerPosition.Y, Color.WHITE);
            //Raylib.DrawRectangle((int)playerPosition.X, (int)playerPosition.Y, 150, 150, Color.BROWN);
            player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 150, 150); //player collision rectangle

            //figure out the jump once algorithm (hasjumped algorithm)
            //WORKING
            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && hasJumped == false) {
                isJumping = true;
                isMoving = true;
                hasJumped = true;
            }
            if (isJumping == true && hasJumped == true) {
                playerPosition.Y -= jumpDist;
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE) || Raylib.IsKeyUp(KeyboardKey.KEY_SPACE)) {
                isJumping = false;
                hasJumped = false;
            }


            //WORKING
            //movement of player
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP)) {
                isMovingUp = true;
                isMoving = true;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                isMovingDown = true;
                isMoving = true;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                isMovingLeft = true;
                isMoving = true;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                isMovingRight = true;
                isMoving = true;
            }

            if (isMovingUp == true) {
                playerPosition.Y -= movementDist;  
            }
            if (isMovingDown == true) {
                playerPosition.Y += movementDist;
            }
            if (isMovingLeft == true) {
                playerPosition.X -= movementDist;
            }
            if(isMovingRight == true) {
                playerPosition.X += movementDist;    
            }

            if (Raylib.IsKeyReleased(KeyboardKey.KEY_UP) || Raylib.IsKeyUp(KeyboardKey.KEY_UP)) {
                isMovingUp = false;
                isMoving = false;
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_DOWN) || Raylib.IsKeyUp(KeyboardKey.KEY_DOWN))
            {
                isMovingDown = false;
                isMoving = false;
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT) || Raylib.IsKeyUp(KeyboardKey.KEY_LEFT))
            {
                isMovingLeft = false;
                isMoving = false;
            }
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyUp(KeyboardKey.KEY_RIGHT))
            {
                isMovingRight = false;
                isMoving = false;
            }

            //NOT WORKING
            /**
            if(playerPosition.X > Raylib.GetScreenWidth()){
                playerPosition.X -= Raylib.GetScreenWidth();
            }
            if (playerPosition.Y > Raylib.GetScreenHeight()) {
                playerPosition.Y -= Raylib.GetScreenHeight();
            }
            **/
        }
    }
}
