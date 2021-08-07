using Raylib_cs;
using System.Numerics;
using System.Resources;

namespace Gamerpg
{
    public static class Program
    {
        //cam
        private static Camera2D camera;

        //play
        //private static Rectangle player;
        private static Vector2 playerPosition = new Vector2(400, 400);
        private static Texture2D playerTex;
        private static float playerHeight = 150;
        private static float playerWidth = 150;

        private static Texture2D screenBackTex;
        //private static Rectangle screenBack;
        private static float scrollBack = 0.0f; 

        private static bool isJumping = false;
        private static bool hasJumped = false;
        private static bool isMoving, isMovingUp, isMovingDown, isMovingLeft, isMovingRight;
        private static bool isUsing = false;
        private static bool gameRunning = false;
        private static float gravityAmount = 3;
        private static float jumpDist = 10;
        private static float movementDist = 5;

        private static Sound mainTheme;
        private static Sound elfTheme;
        private static float volumeMain = 1;

        private static int screenUpperLimit = 40;

        public static void Main(string[] args)
        {
            Raylib.SetTargetFPS(300);
            Raylib.InitWindow(1200, 800, "Woodcutter Adventures");
            
            //load player texture
            playerTex = Raylib.LoadTexture(@"C:\Users\joshu\source\repos\Gamerpg(develop)\Game assets\Main character sprites\Woodcutter\Woodcutter.png");
            playerTex.height = 200;
            playerTex.width = 200;

            //load screen background
            screenBackTex = Raylib.LoadTexture(@"C:\Users\joshu\source\repos\Gamerpg\Game assets\Backgrounds\Battleground1\Pale\Battleground1.png");
            screenBackTex.height = 1200;
            screenBackTex.width = 1200;

            //load music
            Raylib.InitAudioDevice();
            //mainTheme = Raylib.LoadSound(@"C:\Users\joshu\source\repos\Gamerpg\Game assets\Music\main (OGG).ogg");
            elfTheme = Raylib.LoadSound(@"C:\Users\joshu\source\repos\Gamerpg\Game assets\Music\elf theme (OGG).ogg");
            Raylib.SetSoundVolume(elfTheme, volumeMain);
            Raylib.PlaySound(elfTheme);

            while (!Raylib.WindowShouldClose())
            {
                gameRunning = true;
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                
                UpdateBackground();

                Raylib.DrawFPS(10, 25);
                Raylib.DrawText("This is a rpg, based on a wood cutter main character.", 30, 50, 20, Color.WHITE);
                Raylib.DrawText("Press N to pause the music, M to resume and B to restart the music.", 30, 70, 20, Color.WHITE);
                Raylib.DrawText("Use the directional keys to move the character. ", 30, 90, 20, Color.WHITE);

                
                UpdateMusic();
                UpdatePlayer();
                UpdateSprite();

                if (gameRunning == true)
                {
                    playerPosition.Y += gravityAmount;
                }

                Raylib.EndMode2D();
                Raylib.EndDrawing();
            }

            Raylib.UnloadSound(elfTheme);
            Raylib.CloseAudioDevice();
            Raylib.UnloadTexture(playerTex);
            Raylib.UnloadTexture(screenBackTex);

            gameRunning = false;
            Raylib.CloseWindow();
        }

        private static void UpdateBackground() {
            //scrollBack -= 0.1f;

            /**
            if (scrollBack <= -screenBackTex.width * 2)
            {
                scrollBack = 0;
            }
            **/

            float yer = screenBackTex.width * 2 + scrollBack;
            Vector2 vec = new Vector2(scrollBack, 0);
            //Vector2 vec2 = new Vector2(yer, 0);
            Raylib.DrawTextureEx(screenBackTex, vec, 0.0f, 1.0f, Color.WHITE);
            //Raylib.DrawTextureEx(screenBackTex, vec2, 0.0f, 1.0f, Color.WHITE);           
        }

        private static void UpdateMusic() { 
            //Raylib.UpdateSound(mainTheme);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_N) || Raylib.IsKeyDown(KeyboardKey.KEY_N) && Raylib.IsSoundPlaying(mainTheme)) {
                Raylib.PauseSound(elfTheme);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_M) || Raylib.IsKeyDown(KeyboardKey.KEY_M) && Raylib.IsSoundPlaying(mainTheme))
            {
                Raylib.ResumeSound(elfTheme);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_B) || Raylib.IsKeyDown(KeyboardKey.KEY_B) && Raylib.IsSoundPlaying(mainTheme))
            {
                Raylib.PlaySound(elfTheme);
            }

        }

        private static void UpdatePlayer() {
            //Raylib.DrawTexture(playerTex, (int)playerPosition.X, (int)playerPosition.Y, Color.WHITE);
            //player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 150, 150); //player collision rectangle
            Rectangle player = new Rectangle(0.0f, 0.0f, playerTex.width, playerTex.height);
            Raylib.DrawTextureRec(playerTex, player, playerPosition, Color.WHITE);
            
            //player collision detection
            if ((playerPosition.X + playerTex.width) >= Raylib.GetScreenWidth())
            {
                playerPosition.X = Raylib.GetScreenWidth() - playerTex.width;
            }
            else if (playerPosition.X <= 0)
            {
                playerPosition.X = 0;
            }

            if ((playerPosition.Y + playerTex.height) >= Raylib.GetScreenHeight())
            {
                playerPosition.Y = Raylib.GetScreenHeight() - playerTex.height;
            }
            else if (playerPosition.Y <= screenUpperLimit)
            {
                playerPosition.Y = screenUpperLimit;
            }

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
        }

        private static void UpdateSprite() { 
            
        }
    }
}
