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
        public int rotation;
        private float rotationY = 0;
        private float rotationX = 0;
        private float rotationZ = 0;
        private float scale = 1;
        public Model model;
        public List<Texture2D> Textures;
        public String Name;
        public bool IsRandomStuff;

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
            if(IsRandomStuff)
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
                }
                rotationY = (float) (rnd.NextDouble() * 2 * Math.PI);
                texture = rnd.Next(Textures.Count);
                scale *= (float) (rnd.NextDouble() *1.5);
                
            }


        }
        
        public void update(GameTime gametime)
        {
            distanceToPlayer = (float)EuclideanDistance(new Vector2(this.position.X, this.position.Z), Player.Instance.Position);
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

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="world">World matrix for multiplying other functions.</param>
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>      
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {
            if (EmodiaQuest.Core.Ingame.Instance.ActiveWorld == WorldState.Safeworld)
            {
                if (distanceToPlayer > Settings.Instance.EnvironmentDetailDistance && IsRandomStuff)
                {
                    return;
                }
                else
                {
                    foreach (ModelMesh mesh in model.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.SpecularPower = 2f;
                            effect.EnableDefaultLighting();
                            effect.World = Matrix.CreateScale(scale) * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateRotationX(rotationX) * Matrix.CreateRotationY(rotationY) * Matrix.CreateRotationZ(rotationZ) * Matrix.CreateTranslation(position) * world;
                            effect.View = view;
                            effect.Projection = projection;
                            if (IsRandomStuff)
                            {
                                
                                effect.FogEnabled = true;
                                effect.FogStart = 15f;
                                effect.FogEnd = Settings.Instance.EnvironmentDetailDistance;
                                effect.PreferPerPixelLighting = true;
                                effect.Texture = Textures.ElementAt(texture);

                                if (distanceToPlayer < 20)
                                {
                                    effect.FogEnabled = false;
                                    effect.Alpha = 1.0f;
                                }
                                if (distanceToPlayer < 30 && distanceToPlayer > 20)
                                {
                                    effect.FogColor = new Vector3(0.1f, 0.1f, 0.1f);
                                    effect.Alpha = 0.9f;
                                }
                                else if (distanceToPlayer < 40 && distanceToPlayer > 30)
                                {
                                    effect.FogColor = new Vector3(0.2f, 0.2f, 0.2f);
                                    effect.Alpha = 0.8f;
                                }
                                else if (distanceToPlayer < 50 && distanceToPlayer > 40)
                                {
                                    effect.FogColor = new Vector3(0.3f, 0.3f, 0.3f);
                                    effect.Alpha = 0.6f;
                                }
                                else if (distanceToPlayer < 60 && distanceToPlayer > 50)
                                {
                                    effect.FogColor = new Vector3(0.4f, 0.4f, 0.4f);
                                    effect.Alpha = 0.4f;
                                }
                                else if (distanceToPlayer > 50)
                                {
                                    effect.FogColor = new Vector3(0.5f, 0.5f, 0.5f);
                                    effect.Alpha = 0.2f;
                                }
                            }

                            //Ambient: {X:0,05333332 Y:0,09882354 Z:0,1819608}         
                            //Diffuse: {X:0,1178511 Y:0,05631156 Z:0,03418359}
                            //Specular: {X:0,25 Y:0,25 Z:0,25}
                            //Emissive: {X:0 Y:0 Z:0}
                        }
                        mesh.Draw();
                    }
                }               
            }
            else
            {
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
                    foreach (ModelMesh mesh in model.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
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

                            effect.AmbientLightColor = ambi * (4.0f - distanceToPlayer * 0.02f);
                            effect.DiffuseColor = diff * (4.0f - distanceToPlayer * 0.02f);

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
