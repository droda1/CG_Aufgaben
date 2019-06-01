using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core {

    public class HierarchyInput : RenderCanvas {
    
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _baseTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _foreArmTransform;
        private TransformComponent _finger1;
        private TransformComponent _finger2;
        private TransformComponent _finger3;
        SceneContainer CreateScene() {
        
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _bodyTransform = new TransformComponent {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
                };
            _upperArmTransform = new TransformComponent {
                Rotation = new float3(0.8f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };
            _foreArmTransform = new TransformComponent {
                Rotation = new float3(0.8f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 8, 0)
            };
            _finger1 = new TransformComponent {
                Rotation = new float3(0.2f, 0, 0),
                Scale = new float3(0.4f, 0.4f, 0.4f),
                Translation = new float3(0, 8.4f, 0.7f)
            };
            _finger2 = new TransformComponent {
                Rotation = new float3(-0.4f, 0, 0),
                Scale = new float3(0.4f, 0.4f, 0.4f),
                Translation = new float3(0, 8.4f, -0.7f)
            };
            _finger3 = new TransformComponent {
                Rotation = new float3(0, 0,-0.2f),
                Scale = new float3(0.4f, 0.4f, 0.4f),
                Translation = new float3(1, 8.4f, 0)
            };
            // Setup the scene graph
            return new SceneContainer {
                //Initialising a list for the children to append            
                Children = new List<SceneNodeContainer> {                
                    // GREY BASE
                    new SceneNodeContainer {
                        //Initialising a list for the components to append
                        Components = new List<SceneComponentContainer> {                        
                            // TRANSFROM COMPONENT
                            _baseTransform,
                            // MATERIAL COMPONENT
                            new MaterialComponent {
                                Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },
                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },
                    // RED BODY
                    new SceneNodeContainer {
                        Components = new List<SceneComponentContainer> {
                            _bodyTransform,
                            new MaterialComponent {
                                Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },
                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                        },
                        Children = new List<SceneNodeContainer> {
                            // GREEN UPPER ARM
                            new SceneNodeContainer {
                                Components = new List<SceneComponentContainer> {
                                    _upperArmTransform,
                                },
                                Children = new List<SceneNodeContainer> {
                                    new SceneNodeContainer {
                                        Components = new List<SceneComponentContainer> {
                                            new TransformComponent
                                            {
                                                Rotation = new float3(0, 0, 0),
                                                Scale = new float3(1, 1, 1),
                                                Translation = new float3(0, 4, 0)
                                            },
                                            new MaterialComponent
                                            {
                                                Diffuse = new MatChannelContainer { Color = new float3(0, 1, 0) },
                                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                            },
                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                        }
                                    },
                                    // BLUE FOREARM
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _foreArmTransform,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            },
                                            //Finger 1
                                              new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _finger1,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                             new SceneNodeContainer{
                                                 
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent{
                                                        Rotation = new float3(0,0,0),
                                                        Scale = new float3(1,1,1),
                                                        Translation = new float3(0,4,0)
                                                        
                                                    },
                                                   new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 5, 2))
                                                }
                                                
                                            }
                                        }
                                    },
                                    //Finger 2
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _finger2,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                             new SceneNodeContainer{
                                                 
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent{
                                                        Rotation = new float3(0,0,0),
                                                        Scale = new float3(1,1,1),
                                                        Translation = new float3(0,4,0)
                   
                                                    },
                                                   new MaterialComponent {
                                                                    
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 5, 2))
                                                }
                                                
                                                    }
                                                }
                                        },
                                    //Finger 3
                                	new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _finger3,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                             new SceneNodeContainer{
                                                 
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent{
                                                        Rotation = new float3(0,0,0),
                                                        Scale = new float3(1,1,1),
                                                        Translation = new float3(0,4,0)
                   
                                                    },
                                                   new MaterialComponent {
                                                                    
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 5, 2))
                                                }
                                                
                                                    }
                                                }
                                        }
                                    }
                                    }    
                                }
                            },
                        }
                    }
                }
            };
        }
        // Init is called on startup. 
        public override void Init()
        {       
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);
            _scene = CreateScene();
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {   
            Diagnostics.Log(_finger2.Rotation.x);
            //body
            float bodyRot = _bodyTransform.Rotation.y;
            bodyRot += 0.9f * Keyboard.LeftRightAxis * DeltaTime;   
            _bodyTransform.Rotation = new float3(0, bodyRot, 0);
            //upperarm
            float upper = _upperArmTransform.Rotation.x;
            upper += 0.3f * Keyboard.UpDownAxis * DeltaTime;
            _upperArmTransform.Rotation = new float3(upper,0,0);
            //forearm
            float forearm = _foreArmTransform.Rotation.x;
            forearm += 0.5f * Keyboard.UpDownAxis * DeltaTime;
            _foreArmTransform.Rotation = new float3(forearm,0,0);
            //finger 
            float _finger1_move = _finger1.Rotation.x;
            if(Mouse.MiddleButton == true ){
                if(_finger1_move>-0.108f){_finger1_move -= 0.4f * DeltaTime;}
                _finger1.Rotation = new float3(_finger1_move,0,0);
            }
            float finger1_unmove = _finger1.Rotation.x;
            if(Mouse.RightButton == true){
                if(_finger1_move<0.4f){_finger1_move += 0.4f * DeltaTime;}
                _finger1.Rotation = new float3(_finger1_move,0,0);
            }
             float _finger2_move = _finger2.Rotation.x;
            if(Mouse.MiddleButton == true ){
                if(_finger2_move<0.108f){_finger2_move += 0.4f * DeltaTime;}
                _finger2.Rotation = new float3(_finger2_move,0,0);
            }
            float finger2_unmove = _finger1.Rotation.x;
            if(Mouse.LeftButton == true){
                if(_finger2_move>=-0.4f){_finger2_move -= 0.4f * DeltaTime;}
                _finger2.Rotation = new float3(_finger2_move,0,0);
            }
           
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            if(Mouse.LeftButton == true){
            _camAngle += 0.005f * (Mouse.Velocity.x * DeltaTime);}
            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);
            // Render the scene on the current render context
            _sceneRenderer.Render(RC);
            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }
        // Is called when the window was resized
        public override void Resize() {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);
            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;
            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}