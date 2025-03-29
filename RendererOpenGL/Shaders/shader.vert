#version 330 core

in vec3 position;
in vec3 vertNormal;

uniform mat4 transform;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 lightPos;

out vec3 fragNormal;
out vec3 lightDir;

void main()
{
    vec3 worldPos = (vec4(position, 1.0) * transform).xyz;
    gl_Position = vec4(worldPos, 1.0) * view * projection;

    fragNormal = (transform * vec4(vertNormal, 0.0f)).xyz;
    lightDir = lightPos - worldPos;
}