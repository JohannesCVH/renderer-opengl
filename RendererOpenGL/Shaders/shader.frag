#version 330 core

in vec3 fragNormal;
in vec3 lightDir;

out vec4 color;

void main()
{
    vec3 normalU = normalize(fragNormal);
    vec3 lightDirU = normalize(lightDir);

    float surNormToLightDot = dot(normalU, lightDirU);
    float diffuse = max(surNormToLightDot, 0.1f);
    
    color = vec4(diffuse * vec3(0.6f, 0.6f, 0.6f), 1.0f);
}