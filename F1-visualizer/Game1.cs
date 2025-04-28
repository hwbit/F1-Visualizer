using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
namespace F1_visualizer;

public class Game1 : Game
{
    Texture2D ballTexture;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    List<Location> filteredLoc;

    private string[] driverAndSession;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public Game1(string[] driverAndSession)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this.driverAndSession = driverAndSession;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        // string fileName = string.Format("..{0}F1-visualizer{0}Location{0}test.json", Path.DirectorySeparatorChar);
        // string jsonString = File.ReadAllText(fileName);

        DataController da = new DataController();
        string[] arr = {"9161", "1"};
        string[] arr2 = {"9169", "1"};
        string jsonString = da.ProcessCall("location", 
            this.driverAndSession != null ? this.driverAndSession: arr2);

        List<Location> location = JsonSerializer.Deserialize<List<Location>>(jsonString);
        Track track = new Track(
            location,
            _graphics.PreferredBackBufferHeight,
            _graphics.PreferredBackBufferWidth
            );

        filteredLoc = track.getFilteredLoc();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ballTexture = Content.Load<Texture2D>("1x1");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (var loc in filteredLoc)
        {
            _spriteBatch.Draw(
                ballTexture,
                new Vector2(loc.x, loc.y),
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                new Vector2(5, 5),
                SpriteEffects.None,
                0f
            );
        }
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
