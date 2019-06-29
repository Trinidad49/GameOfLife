using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JuegoDeLaVida
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch,spriteBatch2;
        Texture2D vivo, muerto, play, pause, clear, reja, barrazoom, sliderzoom, barravelocidad, vel1, vel2, vel3, info, infox, cartelinfo;
        Vector2 aux,position, posicionmover;
        Array array;
        bool ejecuta, ventanainfo;
        int activo, contador, velocidad;
        double x, y;
        float zoom;
        Camera camara;
        Button botonplay, botonclear, zoomup, zoomdown, botonbarravel, botonvel1, botonvel2, botonvel3, botoninfo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            array = new Array();
            ejecuta = false;
            //Actualizaciones por segundo
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f);
            contador = 0;
            velocidad = 2;
            //Tamaño ventana
            graphics.PreferredBackBufferWidth = 860;
            graphics.PreferredBackBufferHeight = 860;
            graphics.ApplyChanges();
            //Camara
            camara = new Camera(GraphicsDevice.Viewport);
            //Botones
            botonplay = new Button(50, 35, 770, 775);
            botonclear = new Button(35, 35, 778, 720);
            zoomup = new Button(30, 30, 780, 500);
            zoomdown = new Button(30, 30, 780, 670);
            botonbarravel = new Button(170, 35, 580, 775 );
            botonvel1 = new Button(55, 35, 580, 775);
            botonvel2 = new Button(50, 35, 640, 775);
            botonvel3 = new Button(55, 35, 695, 775);
            botoninfo = new Button(35, 35, 770, 50);

            ventanainfo = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch2 = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            vivo = this.Content.Load<Texture2D>("vivo");
            muerto = this.Content.Load<Texture2D>("muerto");
            play = this.Content.Load<Texture2D>("play");
            clear = this.Content.Load<Texture2D>("clear");
            reja = this.Content.Load<Texture2D>("reja");
            barrazoom = this.Content.Load<Texture2D>("barrazoom");
            sliderzoom = this.Content.Load<Texture2D>("sliderzoom");
            barravelocidad = this.Content.Load<Texture2D>("barravelocidad");
            vel1 = this.Content.Load<Texture2D>("vel1");
            vel2 = this.Content.Load<Texture2D>("vel2");
            vel3 = this.Content.Load<Texture2D>("vel3");
            pause = this.Content.Load<Texture2D>("pause");
            info = this.Content.Load<Texture2D>("info");
            infox = this.Content.Load<Texture2D>("infox");
            cartelinfo = this.Content.Load<Texture2D>("cartelinfo");

            MouseController.getInstance().load(this.Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Conseguir la posicion y el zoom
            posicionmover = camara.GetPosition();
            zoom = camara.GetZoom();

            //Manejo de botones
            if (ventanainfo == true)
            {
                if (botoninfo.EnterButton() && MouseController.getInstance().leftClick())
                {
                    ventanainfo = false;
                }
            }
            else
            {
                if (botoninfo.EnterButton() && MouseController.getInstance().leftClick())
                {
                    ventanainfo = true;
                }
                else if (botonclear.EnterButton() && MouseController.getInstance().leftClick())
                {
                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        for (int j = 0; j < array.GetLength(1); j++)
                        {
                            if (array.Valor(i, j) == 1)
                            {
                                array.CambioEstado(i, j);
                            }
                        }
                    }
                }
                else if (botonplay.EnterButton() && MouseController.getInstance().leftClick())
                {
                    ejecuta = !ejecuta;
                }
                else if (zoomup.EnterButton() && MouseController.getInstance().leftClick())
                {
                    camara.AdjustZoom(1f);
                }
                else if (zoomdown.EnterButton() && MouseController.getInstance().leftClick())
                {
                    camara.AdjustZoom(-1f);
                }
                else if (botonvel1.EnterButton() && MouseController.getInstance().leftClick())
                {
                    velocidad = 30;
                    contador = 0;
                }
                else if (botonvel2.EnterButton() && MouseController.getInstance().leftClick())
                {
                    velocidad = 2;
                    contador = 0;
                }
                else if (botonvel3.EnterButton() && MouseController.getInstance().leftClick())
                {
                    velocidad = 1;
                    contador = 0;
                }
                //Cambio de estado de celulas con click izquierdo
                else if (MouseController.getInstance().leftClick() && (ejecuta == false))
                {
                    position = MouseController.getInstance().getLocation().ToVector2();
                    y = ((((int)position.Y + (((int)posicionmover.Y - 430)) * zoom) - (30 * zoom) + (430 * (zoom - 1))) / (8 * zoom));
                    x = ((((int)position.X + (((int)posicionmover.X - 430)) * zoom) - (30 * zoom) + (430 * (zoom - 1))) / (8 * zoom));

                    if (x >= 0 && x < 100 && y < 100 && y >= 0)
                    {
                        array.CambioEstado((int)Math.Floor(x), (int)Math.Floor(y));
                    }
                }
            }

            //Toggle de la ejecucion del juego
            if (MouseController.getInstance().rightClick())
            {
                    ejecuta = !ejecuta;
            }

            //Update de la camara
            camara.UpdateCamera(GraphicsDevice.Viewport);

            //Ejecucion del juego
            if (ejecuta == true) {
                contador += 1;
                if (contador == velocidad)
                {
                    array.Ciclo();
                    contador = 0;
                }
            }

            activo = array.Activo();

            MouseController.getInstance().update();
            KeyboardController.getInstance().update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, camara.Transform);
            spriteBatch2.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            if (ejecuta == true)
            {
                spriteBatch2.Draw(pause, destinationRectangle: botonplay.GetRectangle());
            }
            else
            {
                spriteBatch2.Draw(play, destinationRectangle: botonplay.GetRectangle());
            }
            spriteBatch2.Draw(clear, destinationRectangle: botonclear.GetRectangle());
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    aux.X = i * 8 + 30;
                    aux.Y = j * 8 + 30;
                    if (array.Valor(i, j) == 1)
                    {
                        spriteBatch.Draw(vivo, aux);
                    }
                    else
                    {
                        spriteBatch.Draw(muerto, aux);
                    }
                }
            }

            if (zoom == 3 || zoom == 4 || zoom == 5)
            {
                aux.X = 30;
                aux.Y = 30;
                spriteBatch.Draw(reja, aux);
            }

            aux.X = 770;
            aux.Y = 500;
            spriteBatch2.Draw(barrazoom, aux);

            spriteBatch2.Draw(barravelocidad, destinationRectangle: botonbarravel.GetRectangle());

            if (velocidad == 30)
            {
                spriteBatch2.Draw(vel1, destinationRectangle: botonvel1.GetRectangle());
            }

            if (velocidad == 2)
            {
                spriteBatch2.Draw(vel2, destinationRectangle: botonvel2.GetRectangle());
            }

            if (velocidad == 1)
            {
                spriteBatch2.Draw(vel3, destinationRectangle: botonvel3.GetRectangle());
            }

            if (zoom == 1)
            {
                aux.X = 780;
                aux.Y = 641;
                spriteBatch2.Draw(sliderzoom, aux);
            }
            else if (zoom == 2)
            {
                aux.X = 780;
                aux.Y = 618;
                spriteBatch2.Draw(sliderzoom, aux);
            }
            else if (zoom == 3)
            {
                aux.X = 780;
                aux.Y = 594;
                spriteBatch2.Draw(sliderzoom, aux);
            }
            else if (zoom == 4)
            {
                aux.X = 780;
                aux.Y = 571;
                spriteBatch2.Draw(sliderzoom, aux);
            }
            else if (zoom == 5)
            {
                aux.X = 780;
                aux.Y = 548;
                spriteBatch2.Draw(sliderzoom, aux);
            }

            if (ventanainfo == true)
            {
                spriteBatch2.Draw(cartelinfo,Vector2.Zero);
                spriteBatch2.Draw(infox, destinationRectangle: botoninfo.GetRectangle());
            }
            else
            {
                spriteBatch2.Draw(info, destinationRectangle: botoninfo.GetRectangle());
            }

            MouseController.getInstance().draw(spriteBatch2);
            spriteBatch.End();
            spriteBatch2.End();


            base.Draw(gameTime);
        }
    }
}
