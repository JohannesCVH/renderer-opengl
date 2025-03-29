using System;
using OpenTK.Mathematics;

namespace RendererOpenGL;

public class EntityLight : Entity
{
    public float Color { get; set ;}
    
    public EntityLight(Vector3 pos) : base(pos)
    {
    
    }
}
