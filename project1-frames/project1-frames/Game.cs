﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input; //for keyboard input
using OpenTK.Graphics.OpenGL; //for rendering
using System.Drawing; //for colours

namespace project1_frames
{
    public class Game : IDisposable
    {
        protected int SCREEN_WIDTH = 800;
        protected int SCREEN_HEIGHT = 600;

        protected GameWindow game;

        //constructor
        public Game()
        {
            game = new GameWindow(
                SCREEN_WIDTH, //Width
                SCREEN_HEIGHT, //Height
                new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4), //GraphicsMode
                "First Game" //Title
                );

            game.Load += LoadResources;
            game.Resize += Resize;
            game.KeyDown += Input;
            game.RenderFrame += Render;

        }

        //run game
        public void Run(double fps)
        {
            game.Run(fps);
        }

        //setup GL
        public bool InitGL()
        {
            //initalize projection matrix
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, SCREEN_WIDTH, SCREEN_HEIGHT, 0.0, 1.0, -1.0);

            //initalize Modelview matrix
            //use modeview instead of projection
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            //check for errors
            ErrorCode error = GL.GetError();
            if(error != ErrorCode.NoError)
            {
                System.Console.WriteLine("Error initializing OpenTK, error: " + error.ToString());
                return false;
            }
            return true;
        }

        protected void LoadResources( object sender, EventArgs e)
        {
            game.VSync = VSyncMode.On;
        }

        protected void Resize(object sender, EventArgs e )
        {
            SCREEN_WIDTH = game.Width;
            SCREEN_HEIGHT = game.Height;
            OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, game.Width, game.Height);
        }

        //user input
        protected void Input(object sender, KeyboardKeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                game.Exit();
            }
        }

        //update display
        protected void Render(object sender, FrameEventArgs e)
        {
            //set clear colour to black
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            //clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LoadIdentity();
            GL.Translate(SCREEN_WIDTH / 2.0f, SCREEN_HEIGHT / 2.0f, 0.0f);
            GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.SpringGreen);
                GL.Vertex2(-50.0f, -50.0f);
                GL.Color3(Color.MidnightBlue);
                GL.Vertex2(-50.0f, 50.0f);
                GL.Color3(Color.Ivory);
                GL.Vertex2(50.0f, 50.0f);
                GL.Color3(Color.Red);
                GL.Vertex2(50.0f, -50.0f);
            GL.End();
            

            /*
            //start the shader?
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.MidnightBlue);
            GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex3(1.0f, 0.0f, 0.0f);

            //end the shader?
            GL.End();
            */

            //swap Display memory with virtual memory
            game.SwapBuffers();
            
        }


        public void Dispose()
        {
            game.Dispose();
        }
    }
}
