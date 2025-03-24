using System.Globalization;

namespace RendererOpenGL;

public class ObjReader
{
    private string[] Lines { get; set; }
    public List<float> Vertices { get; set; } = new List<float>();
    public List<int> Faces { get; set; } = new List<int>();
    
    public ObjReader(string filePath)
    {
        Lines = File.ReadAllLines(filePath);
        Process();
    }

    private void Process()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            string[] tokens = Lines[i].Split(' ');
            if (tokens[0].Equals("v"))
            {
                Vertices.Add(float.Parse(tokens[1], CultureInfo.InvariantCulture));
                Vertices.Add(float.Parse(tokens[2], CultureInfo.InvariantCulture));
                Vertices.Add(float.Parse(tokens[3], CultureInfo.InvariantCulture));
            }
        }

        for (int i = 0; i < Lines.Length; i++)
        {
            string[] tokens = Lines[i].Split(' ');
            if (tokens[0].Equals("f"))
            {
                int verLoc1 = int.Parse(tokens[1]) - 1;
                int verLoc2 = int.Parse(tokens[2]) - 1;
                int verLoc3 = int.Parse(tokens[3]) - 1;
                
                Faces.Add(verLoc1);
                Faces.Add(verLoc2);
                Faces.Add(verLoc3);
            }
        }
    }
}