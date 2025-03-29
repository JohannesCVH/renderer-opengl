using OpenTK.Mathematics;
using RendererOpenGL.Models3D;

namespace RendererOpenGL;

public class EntityPhysical : Entity, IDisposable
{
    public float ScaleFactor { get; set; }
	public float Rotation { get; set; }
	private float _rotX;
	public float RotX { 
		get { return _rotX; } 
		set {
			if (_rotX + value >= 360) 
				_rotX = 0 + value;
			else if (_rotX + value < 0) 
				_rotX = 360 + value;
			else _rotX = value;
		}
	}
	private float _rotY;
	public float RotY { 
		get { return _rotY; } 
		set {
			if (_rotY + value >= 360) 
				_rotY = 0 + value;
			else if (_rotY + value < 0) 
				_rotY = 360 + value;
			else _rotY = value;
		}
	}
	private float _rotZ;
	public float RotZ { 
		get { return _rotZ; } 
		set {
			if (_rotZ + value >= 360) 
				_rotZ = 0 + value;
			else if (_rotZ + value < 0) 
				_rotZ = 360 + value;
			else _rotZ = value;
		}
	}
	
	public Mesh Mesh { get; set; }
    
    public EntityPhysical(Vector3 pos, float scaleF, float angle, string filePathObj, string? filePathMtl = null) : base(pos)
    {
		ScaleFactor = scaleF;
		RotX = angle;
		
        ObjReader ObjReader = new ObjReader(filePathObj, filePathMtl);
        Mesh = ObjReader.Mesh;
    }
    
    public void RotatePitch(float angle)
	{   
		RotX += angle;
	}

	public void RotateYaw(float angle)
	{   
		RotY += angle;
	}
	
	public void RotateRoll(float angle)
	{   
		RotZ += angle;
	}
	
	public void Dispose()
    {
        Mesh.Dispose();
    }
}
