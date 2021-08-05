﻿using Raylib_cs;
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
        private static bool isJumping = false;
        private static bool isMoving, isMovingUp, isMovingDown, isMovingLeft, isMovingRight;
        private static bool isUsing = false;

        private static float gravity = 10;
        private static float jumpDist = 10;
        private static float jumpTime = 0.15f;
        private static float timeBetweenJumps = 0.2f;
        private static float movementDist = 5;
        


        public static void Main(string[] args)
        {
            Raylib.InitWindow(800, 600, "BlockMan Adventures");

            //load player texture
            //Raylib.LoadTexture("");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                Raylib.DrawFPS(10, 10);

                Raylib.DrawText("Yeaaaaaaah boi", 30, 50, 40, Color.BLACK);

                PlayerLoop();
                UpdateCamera();

                Raylib.EndMode2D();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static void UpdateCamera() { 
            camera.target = playerPosition;
            camera.offset = new Vector2(1200 / 2.0f, 7.0f / 2.0f);
            camera.rotation = 0.0f;
            camera.zoom = 1.0f;
        }

        private static void PlayerLoop() {
            Raylib.DrawRectangle((int)playerPosition.X, (int)playerPosition.Y, 100, 100, Color.BROWN);
            player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 100, 100);
            //Rectangle screen = new Rectangle((int)Raylib.GetScreenWidth(), (int)Raylib.GetScreenHeight(), 0, 0);
            //Raylib.GetCollisionRec(screen, player);

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) {
                isJumping = true;
                isMoving = true;
            }
            if (isJumping == true) {
                playerPosition.Y -= jumpDist;
            }

            //WORKING CODE
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

            //workout the screen stuff
            StopOffScreen();
        }

        private static void StopOffScreen() { 
            if(playerPosition.X > Raylib.GetScreenWidth()){
                playerPosition.X -= Raylib.GetScreenWidth();
            }
            if (playerPosition.Y > Raylib.GetScreenHeight()) {
                playerPosition.Y -= Raylib.GetScreenHeight();
            }
        }
    }
}