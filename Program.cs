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
        private static Rectangle screenBack;

        private static bool isJumping = false;
        private static bool hasJumped = false;
        private static bool isMoving, isMovingUp, isMovingDown, isMovingLeft, isMovingRight;
        private static bool isUsing = false;
        private static bool gameRunning = false;
        private static float gravityAmount = 1;
        private static float jumpDist = 5;
        private static float movementDist = 3;

        private static Sound mainTheme;
        private static float volumeMain = 1;

        private static int screenUpperLimit = 600;

        public static void Main(string[] args)
        {

            Raylib.InitWindow(800, 600, "Woodcutter Adventures");
            //load player texture
            playerTex = Raylib.LoadTexture(@"C:\Users\joshu\source\repos\Gamerpg(develop)\Game assets\Main character sprites\Woodcutter\Woodcutter.png");
            playerTex.height = 150;
            playerTex.width = 150;

            //load screen background
            screenBackTex = Raylib.LoadTexture(@"C:\Users\joshu\source\repos\Gamerpg(develop)\Game assets\Backgrounds\Battleground1\Bright\Battleground1.png");
            screenBackTex.height = 600;
            screenBackTex.width = 800;

            //load music
            Raylib.InitAudioDevice();
            mainTheme = Raylib.LoadSound(@"C:\Users\joshu\source\repos\Gamerpg\Game assets\Music\main.mp3");
            Raylib.SetSoundVolume(mainTheme, volumeMain);
            Raylib.PlaySound(mainTheme);

            while (!Raylib.WindowShouldClose())
            {
                gameRunning = true;
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                Raylib.DrawFPS(10, 10);
                Raylib.DrawText("This is a rpg, based on a wood cutter main character.", 30, 50, 20, Color.BLACK);

                UpdateBackground();
                UpdateMusic();
                UpdatePlayer();

                if (gameRunning == true)
                {
                    playerPosition.Y += gravityAmount;
                }

                Raylib.EndMode2D();
                Raylib.EndDrawing();
            }

            Raylib.UnloadSound(mainTheme);
            Raylib.CloseAudioDevice();
            Raylib.UnloadTexture(playerTex);

            gameRunning = false;
            Raylib.CloseWindow();
        }

        private static void UpdateBackground() {
            Raylib.DrawTexture(screenBackTex, screenBackTex.width, screenBackTex.height, Color.BLANK);

        }

        private static void UpdateMusic() { 
            //Raylib.UpdateSound(mainTheme);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_N) || Raylib.IsKeyDown(KeyboardKey.KEY_N) && Raylib.IsSoundPlaying(mainTheme)) {
                Raylib.PauseSound(mainTheme);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_M) || Raylib.IsKeyDown(KeyboardKey.KEY_M) && Raylib.IsSoundPlaying(mainTheme))
            {
                Raylib.ResumeSound(mainTheme);
            }

        }

        private static void UpdatePlayer() {
            //Raylib.DrawTexture(playerTex, (int)playerPosition.X, (int)playerPosition.Y, Color.WHITE);
            //player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 150, 150); //player collision rectangle
            Rectangle player = new Rectangle(0.0f, 0.0f, playerTex.width, playerTex.height);
            Raylib.DrawTextureRec(playerTex, player, playerPosition, Color.WHITE);
            
            //player collision detection
            if ((player.x + player.width) >= Raylib.GetScreenWidth())
            {
                player.x = Raylib.GetScreenWidth() - player.width;
            }
            else if (player.x <= 0)
            {
                player.x = 0;
            }

            if ((player.y + player.height) >= Raylib.GetScreenHeight())
            {
                player.y = Raylib.GetScreenHeight() - player.height;
            }
            else if (player.y <= screenUpperLimit)
            {
                player.y = screenUpperLimit;
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
