﻿

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

namespace Fusee.Tutorial.Core
{
    public class FirstStepsCompleted : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;

        private float _camAngle = 0;
        private SceneNodeContainer[] _cubes;

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
           RC.ClearColor = new float4(0.7f, 1.0f, 0.5f, 1.0f);
            // Create five cubes
            _cubes = new SceneNodeContainer[5];

            for (int i = 0; i <5; i++)
            {
                var cubeTransform = new TransformComponent { Scale = new float3(1, 1, 1), Translation = new float3((i*2)*8, 0, 0) };
                var cubeShader = new ShaderEffectComponent {
                 Effect = SimpleMeshes.MakeShaderEffect(new float3 (1, 1, 1), new float3 (1, 1, 1),  1)};                        
                var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
                var cubeNode = new SceneNodeContainer();
                cubeNode.Components = new List<SceneComponentContainer>();
                cubeNode.Components.Add(cubeTransform);
                cubeNode.Components.Add(cubeShader);
                cubeNode.Components.Add(cubeMesh);

                _cubes[i] = cubeNode;
            }

            // Create the scene containing the cubes
            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            foreach (SceneNodeContainer cube in _cubes)
            {
                _scene.Children.Add(cube);
            }

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Animate the cubes
            for (int i = 0; i < 5; i++) {
                      
            _cubes[i].GetTransform().Translation = new float3((i*2) * 10, 2 * M.Sin((2 * TimeSinceStart)+ i ), M.Cos(2 * TimeSinceStart));
            _cubes[i].GetTransform().Rotation = new float3(0, 5 * M.Sin((2 * TimeSinceStart)+ i), 0);
            _cubes[i].GetTransform().Scale = new float3(0.5f * M.Cos(TimeSinceStart + (i * M.PiOver4)) + 0.5f, 0.5f * M.Cos(TimeSinceStart + (i * M.PiOver4)) + 0.5f, 0.5f * M.Cos(TimeSinceStart + (i * M.PiOver4)) + 0.5f);
            }

            // Animate the camera angle
            _camAngle = _camAngle + 15.0f * M.Pi/180.0f * DeltaTime;

            // Setup the camera
            RC.View = float4x4.CreateTranslation(0, 0, 100) * float4x4.CreateRotationY(_camAngle);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rerndered farame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
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
