using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EmodiaQuest.Core
{

    public class GameObject
    {
        Random rnd = new Random();

        private int texture;

        public Vector3 position;
        public Vector2 Position2D;
        public int rotation;
        private float rotationY = 0;
        private float rotationX = 0;
        private float rotationZ = 0;
        private float scale = 1;
        public Model model;
        public List<Texture2D> Textures;
        public String Name;
        public bool IsRandomStuff;
        float testwert = 0.0f;
        static int test = 0;
        bool JanosWHY = true;

        Vector2 directionWith90Degrees;
        Vector2 directionPlusPlayer;
        Vector2 playerPos;
        Vector2 playerView;


        // Portal Content
        int portalCount = 1;
        private Model portal;
        List<Texture2D> portalList = new List<Texture2D>();
        Texture2D portalTest;
        private float portalTimer = 0;
        private float qRotAngle = 0.0f;

        // Effect
        Effect copiedEffect;

        private float distanceToPlayer = 0.0f;

        /*
        private Vector3 ambi = new Vector3(0.05333332f, 0.09882354f, 0.1819608f) * 3;
        private Vector3 diff = new Vector3(0.1178511f, 0.05631156f, 0.03418359f) * 2;
        private Vector3 spec = new Vector3(0.25f, 0.25f, 0.25f);
        private Vector3 emis = new Vector3(0.0f, 0.0f, 0.0f);
        */

        private Vector3 ambi = new Vector3(0.05333332f, 0.05882354f, 0.0519608f) * 3;
        private Vector3 diff = new Vector3(0.0578511f, 0.05631156f, 0.03418359f) * 2;

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="model">A model for the .</param>
        /// <param name="position">Position of object.</param>
        /// <param name="rotation">Creates rotation of object: 0 -> 0°, 1 -> 90° etc.</param>
        /// </summary>
        public GameObject(Model model, Vector3 position, int rotation, String name, bool isRandomStuff)
        {
            this.model = model;
            this.position = position;
            this.Name = name;
            this.IsRandomStuff = isRandomStuff;
            Textures = new List<Texture2D>();
            if (rotation == 0 || rotation == 1 || rotation == 2 || rotation == 3)
                this.rotation = rotation;
        }

        public void loadContent(ContentManager Content)
        {
            if (this.Name == "teleporter")
            {
                // Portal Content
                portal = Content.Load<Model>("fbxContent/miscellaneous/portal/portal");
                for (int i = 0; i < 16; i++)
                {
                    portalList.Add(portalTest);
                    portalList[i] = Content.Load<Texture2D>("fbxContent/miscellaneous/portal/Texture" + (i + 1));
                }
                portalTest = portalList[0];
            }

            if (IsRandomStuff)
            {
                rnd = new Random();
                switch (Name)
                {
                    case "Gras_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_1/Random_Gras_1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_1/Random_Gras_1_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_1/Random_Gras_1_tex_3"));
                        break;
                    case "Gras_2":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_2/Random_Gras_2_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_2/Random_Gras_2_tex_3"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_2/Random_Gras_2_tex_4"));
                        break;
                    case "Gras_3":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_3/Random_Gras_3_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_3/Random_Gras_3_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Gras_3/Random_Gras_3_tex_3"));
                        break;
                    case "Busch_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Busch_1/Random_Busch_1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Busch_1/Random_Busch_1_tex_2"));
                        break;
                    case "Busch_2":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Busch_2/Random_Busch_2_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Busch_2/Random_Busch_2_tex_2"));
                        break;
                    case "Busch_3":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Busch_3/Random_Busch_3_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Busch_3/Random_Busch_3_tex_2"));
                        break;
                    case "Stein_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_1/Random_Stein1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_1/Random_Stein1_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_1/Random_Stein1_tex_3"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_1/Random_Stein1_tex_4"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_1/Random_Stein1_tex_5"));
                        rotationZ = (float)(rnd.NextDouble() * 2 * Math.PI);
                        rotationX = (float)(rnd.NextDouble() * 2 * Math.PI);
                        break;
                    case "Stein_2":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_2/Random_Stein2_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_2/Random_Stein2_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_2/Random_Stein2_tex_3"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_2/Random_Stein2_tex_4"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_2/Random_Stein2_tex_5"));
                        rotationZ = (float)(rnd.NextDouble() * 2 * Math.PI);
                        rotationX = (float)(rnd.NextDouble() * 2 * Math.PI);
                        break;
                    case "Stein_3":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_3/Random_Stein_3_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_3/Random_Stein_3_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_3/Random_Stein_3_tex_3"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_3/Random_Stein_3_tex_4"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Stein_3/Random_Stein_3_tex_5"));
                        rotationZ = (float)(rnd.NextDouble() * 2 * Math.PI);
                        rotationX = (float)(rnd.NextDouble() * 2 * Math.PI);
                        break;
                    case "Holzplanke_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Holzplanke_1/Random_Holzplanke_1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Holzplanke_1/Random_Holzplanke_1_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Holzplanke_1/Random_Holzplanke_1_tex_3"));
                        break;
                    case "Kaputter_Flugzeugmotor_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Kaputter_Flugzeugmotor_1/Random_Kaputter_Flugzeugmotor_1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Kaputter_Flugzeugmotor_1/Random_Kaputter_Flugzeugmotor_1_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Kaputter_Flugzeugmotor_1/Random_Kaputter_Flugzeugmotor_1_tex_3"));
                        break;
                    case "MetallStück_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_1/Random_MetallStück_1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_1/Random_MetallStück_1_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_1/Random_MetallStück_1_tex_4"));
                        break;
                    case "MetallStück_2":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_2/Random_MetallStück_2_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_2/Random_MetallStück_2_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_2/Random_MetallStück_2_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/MetallStück_2/Random_MetallStück_2_tex_3"));
                        break;
                    case "Tonne_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Tonne_1/Random_Tonne_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Tonne_1/Random_Tonne_tex_2"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Tonne_1/Random_Tonne_tex_3"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Tonne_1/Random_Tonne_tex_4"));
                        break;
                    case "Tumbleweed_1":
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Tumbleweed_1/Random_Tumbleweed_1_tex_1"));
                        addTexture(Content.Load<Texture2D>("fbxContent/gameobjects/RandomStuff/Tumbleweed_1/Random_Tumbleweed_1_tex_1"));
                        break;
                }
                rotationY = rnd.Next(360);
                texture = rnd.Next(Textures.Count - 1);
            }


        }

        public void update(GameTime gametime)
        {

            // culling and update
            //distanceToPlayer = (float)EuclideanDistance(new Vector2(this.position.X, this.position.Z), Player.Instance.Position);
            //playerPos = Player.Instance.Position;
            //playerView = Player.Instance.PlayerViewDirection;
            //playerView.Normalize();

        }

        /*
  
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateTranslation(position);
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
        */
        public double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public void addTexture(Texture2D tex)
        {
            Textures.Add(tex);
        }

        private float pointSide(Vector2 g, Vector2 s, Vector2 p)
        {
            float result;
            result = g.Y * (p.X - s.X) - (g.X * (p.Y - s.Y));
            return result;
        }

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="world">World matrix for multiplying other functions.</param>
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>      
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {

            

            // Safeworld rendering
            if (EmodiaQuest.Core.Ingame.Instance.ActiveWorld == WorldState.Safeworld)
            {
                if (this.Name == "teleporter")
                {
                    // Updating the portal
                    portalTimer += GUI.Controls_GUI.Instance.Control_GameTime.ElapsedGameTime.Milliseconds;
                    //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.B))
                    if (portalTimer > 75)
                    {
                        portalCount++;
                        portalTimer = 0;
                        if (portalCount > 16)
                            portalCount = 1;
                        portalTest = portalList[portalCount - 1];
                    }
                    qRotAngle += 0.015f;
                    if (qRotAngle > Math.PI * 2)
                        qRotAngle -= (float)Math.PI * 2;
                    if (qRotAngle < Math.PI * 2)
                        qRotAngle += (float)Math.PI * 2;

                    // Drawing the portal
                    foreach (ModelMesh mesh in portal.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.Texture = portalTest;
                            effect.EnableDefaultLighting();
                            effect.World = Matrix.CreateRotationZ((float)(qRotAngle)) * Matrix.CreateScale(1f) *Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateRotationY(rotationY) * Matrix.CreateRotationX(rotationX) * Matrix.CreateRotationZ(rotationZ) * Matrix.CreateTranslation(0, 1.65f, 0) * Matrix.CreateTranslation(position) * world;
                            effect.View = view;
                            effect.Projection = projection;
                            effect.EmissiveColor = new Vector3(0.5f, 0.5f, 0.5f);
                            effect.SpecularColor = new Vector3(0.0f);
                            effect.SpecularPower = 0;
                            effect.Alpha = 0.8f;
                            effect.PreferPerPixelLighting = true;
                        }
                        mesh.Draw();
                    }
                }

                if (IsRandomStuff && distanceToPlayer > Settings.Instance.EnvironmentDetailDistance)
                {
                    return;
                }
                else
                {
                    {
                        foreach (ModelMesh mesh in model.Meshes)
                        {
                            foreach (BasicEffect effect in mesh.Effects)
                            {

                                {
                                    effect.EnableDefaultLighting();
                                    effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateRotationY(rotationY) * Matrix.CreateRotationX(rotationX) * Matrix.CreateRotationZ(rotationZ) * Matrix.CreateTranslation(position) * world;
                                    effect.View = view;
                                    effect.Projection = projection;

                                    effect.FogColor = new Vector3(0.5f, 0.55f, 0.5f);
                                    effect.FogEnabled = true;
                                    effect.FogStart = 150f;
                                    effect.FogEnd = 1000f;
                                    effect.PreferPerPixelLighting = true;

                                    effect.AmbientLightColor = ambi * (4.0f - distanceToPlayer * 0.02f);
                                    effect.DiffuseColor = diff * (4.0f - distanceToPlayer * 0.02f);
                                    if (Name == "brownWay")
                                    {
                                        effect.DiffuseColor = new Vector3(0.1578511f, 0.05631156f, 0.03418359f) * 3;
                                    }
                                }

                            }
                            mesh.Draw();
                        }
                    }
                }

            }

            // Dungeon Rendering
            else
            {

                if (this.Name == "teleporter")
                {
                    // Updating the portal
                    portalTimer += GUI.Controls_GUI.Instance.Control_GameTime.ElapsedGameTime.Milliseconds;
                    //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.B))
                    if (portalTimer > 75)
                    {
                        portalCount++;
                        portalTimer = 0;
                        if (portalCount > 16)
                            portalCount = 1;
                        portalTest = portalList[portalCount - 1];
                    }
                    qRotAngle += 0.015f;
                    if (qRotAngle > Math.PI * 2)
                        qRotAngle -= (float)Math.PI * 2;
                    if (qRotAngle < Math.PI * 2)
                        qRotAngle += (float)Math.PI * 2;

                    // Drawing the portal
                    foreach (ModelMesh mesh in portal.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.Texture = portalTest;
                            effect.EnableDefaultLighting();
                            effect.World = Matrix.CreateRotationZ((float)(qRotAngle)) * Matrix.CreateScale(1f) * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateRotationY(rotationY) * Matrix.CreateRotationX(rotationX) * Matrix.CreateRotationZ(rotationZ) * Matrix.CreateTranslation(0, 1.65f, 0) * Matrix.CreateTranslation(position) * world;
                            effect.View = view;
                            effect.Projection = projection;
                            effect.EmissiveColor = new Vector3(0.5f, 0.5f, 0.5f);
                            effect.SpecularColor = new Vector3(0.0f);
                            effect.SpecularPower = 0;
                            effect.Alpha = 0.8f;
                            effect.PreferPerPixelLighting = true;
                        }
                        mesh.Draw();
                    }
                }

                copiedEffect = Player.Instance.copiedEffect;

                if (distanceToPlayer > 60)
                {
                    foreach (ModelMesh mesh in model.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.PreferPerPixelLighting = true;
                            effect.EnableDefaultLighting();
                            effect.World = world * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateTranslation(position);
                            effect.View = view;
                            effect.Projection = projection;

                            effect.AmbientLightColor = new Vector3(0, 0, 0);
                            effect.DiffuseColor = new Vector3(0, 0, 0);
                            effect.SpecularColor = new Vector3(0, 0, 0);
                            effect.EmissiveColor = new Vector3(0, 0, 0);
                            //else
                            //{
                            //    effect.FogColor = new Vector3(0.0f, 0.0f, 0.0f);
                            //    effect.FogEnabled = true;
                            //    effect.FogStart = 8f;
                            //    effect.FogEnd = 75f;
                            //    effect.PreferPerPixelLighting = true;

                            //    effect.AmbientLightColor = ambi * (4.0f - distanceToPlayer * 0.02f);
                            //    effect.DiffuseColor = diff * (4.0f - distanceToPlayer * 0.02f);


                            //    //effect.SpecularColor = spec * (2.0f - distanceToPlayer * 0.05f);
                            //    //effect.EmissiveColor = emis * (1.0f - distanceToPlayer * 0.005f);



                            //}

                        }
                        mesh.Draw();
                    }
                }
                else
                {
                    //Vector2 directionWith90Degrees = Vector2.Transform(Player.Instance.PlayerViewDirection, Matrix.CreateRotationZ((float)(0.5 * Math.PI)));
                    //Vector2 directionWith90Degrees = Vector2.Transform(directionPlusPlayer, Matrix.CreateRotationZ((float)(0.5 * Math.PI)));
                    //if (pointSide(directionWith90Degrees, Position2D, Player.Instance.Position) > 0)
                    //Console.WriteLine(pointSide(directionWith90Degrees, Player.Instance.Position, Position2D));
                    //if (pointSide(directionWith90Degrees, playerPos, Position2D) > 0)
                    //this.Position2D = Vector2.Transform(playerView, Matrix.CreateRotationZ((float)(Math.PI)));

                    //Vector2 position2D = new Vector2(this.position.X, this.position.Z);
                    //Vector2 turnedBulletDirection = Vector2.Transform(playerView, Matrix.CreateRotationZ((float)(0.5 * Math.PI)));
                    //Vector2 ppp = Vector2.Transform(playerView, Matrix.CreateRotationZ((float)(Math.PI)));
                    //ppp.Normalize();
                    //ppp *= -20;
                    //ppp = Vector2.Add(ppp, position2D);

                    //if (pointSide(turnedBulletDirection, playerPos, ppp) > 0)
                    {
                        foreach (ModelMesh mesh in model.Meshes)
                        {
                            foreach (BasicEffect effect in mesh.Effects)
                            {

                                {
                                    effect.EnableDefaultLighting();
                                    effect.World = world * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateTranslation(position);
                                    effect.View = view;
                                    effect.Projection = projection;


                                    effect.FogColor = new Vector3(0.0f, 0.0f, 0.0f);
                                    effect.FogEnabled = true;
                                    effect.FogStart = 8f;
                                    effect.FogEnd = 75f;
                                    effect.PreferPerPixelLighting = true;

                                    // culling and update
                                    effect.AmbientLightColor = ambi * (4.0f - distanceToPlayer * 0.02f);
                                    effect.DiffuseColor = diff * (4.0f - distanceToPlayer * 0.02f);
                                }


                                //effect.Texture = Textures.ElementAt(0);
                                //effect.SpecularColor = spec * (2.0f - distanceToPlayer * 0.05f);
                                //effect.EmissiveColor = emis * (1.0f - distanceToPlayer * 0.005f);



                                //}

                            }
                            mesh.Draw();
                        }
                    }
                }
            }

        }



        //foreach (ModelMesh mesh in model.Meshes)
        //{
        //    foreach (ModelMeshPart part in mesh.MeshParts)
        //    {
        //        part.Effect = EmodiaQuest.Core.Ingame.Instance.DiffuseEffect;
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["World"].SetValue(world * mesh.ParentBone.Transform);
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["View"].SetValue(view);
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["Projection"].SetValue(projection);


        //        //ambientEffect.Parameters["AmbientColor"].SetValue(Color.Green.ToVector4());
        //        //ambientEffect.Parameters["AmbientIntensity"].SetValue(0.9f);

        //        Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * world));
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
        //    }
        //    mesh.Draw();
        //}


    }
}
