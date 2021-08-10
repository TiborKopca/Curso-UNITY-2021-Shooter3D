# Output Lit

Menu Path : **Context > Output [Data Type] Lit [Type]**

*(Output Particle (Lit) Quad, Output Particle (Lit) Mesh, Output ParticleStrip (Lit), etc.)*

![img](Images/Context-OutputLitExample.gif)

Lit Outputs are a variant of outputs which can receive lighting information and support additional texture and material types pertaining to lighting. They are useful in scenarios where the visual effect needs to react to the lighting in the scene.

## Render pipeline compatibility

| **Feature**     | **Built-in Render Pipeline** | **Universal Render Pipeline (URP)** | **High Definition Render Pipeline (HDRP)** |
| --------------- | ---------------------------- | ----------------------------------- | ------------------------------------------ |
| **Lit outputs** | No                           | No                                  | Yes                                        |

Below is a list of settings and properties specific to lit outputs. For information about the generic output settings this Context shares with all other Contexts, see [Global Output Settings and Properties](Context-OutputSharedSettings.md).

## Context settings

| **Setting**                       | **Type**                                                     | **Description**                                              |
| --------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| **Normal Bending**                | Bool                                                         | **(Inspector)** Indicates whether this output supports normal bending. If you enable this property, the **Normal Bending Factor** slider appears which you can use to adjust the curvature of the normals.<br/>This property only appears on **Lit Planar Primitives** (quad, triangle, octagon) and **Lit Particle Strips**. |
| **Material Type**                 | Enum                                                         | **(Inspector)** Specifies the surface type for the output. This determines how the particles react to light. The options are: <br/>&#8226; **Standard**:  The standard lit material that supports most lighting features.<br/>&#8226; **Specular Color**: Allows you to manually define the specular color.<br/>&#8226; **Translucent**:  Uses a [Diffusion Profile](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/Diffusion-Profile.html) to simulate light passing through particles.<br/>&#8226; **Simple Lit**: Allows you to toggle specific lighting calculations to improve performance. Only point, spot, and direction lights affect particles in this mode. This mode. This mode renders particles in forward mode.<br/>&#8226; **Simple Lit Translucent**: Similar to **Simple Lit**, but with a Translucent workflow to simulate light passing through objects. |
| **Only Ambient Lighting**         | Bool                                                         | **(Inspector)** Indicates whether output particles only receive light from ambient light sources and light probes. Disable this setting to have particles receive lighting from all light sources in the scene. |
| **Use Base Color Map**            | Enum                                                         | **(Inspector)** Specifies what parts of the base color map to apply to the output. The options are:<br/>&#8226; **None**: Does not apply the base color map to the output.<br/>&#8226; **Color**: Only applies the color channels of the base color map to the output.<br/>&#8226; **Alpha**: Only applies the alpha channel of the base color map to the output.<br/>&#8226; **Color and Alpha**: Applies the color channels and alpha channel of the base color map to the output. |
| **Use Mask Map**                  | Bool                                                         | **(Inspector)** indicates whether the output accepts a mask map input to control how the particle receives lighting. |
| **Use Normal Map**                | Bool                                                         | **(Inspector)** Indicates whether the output accepts a normal map input to simulate additional surface details when illuminated. |
| **Use Emissive Map**              | Bool                                                         | **(Inspector)** Indicates whether the output accepts an emissive map to control how particles glow, independently of how they are lit. |
| **Color Mode**                    | Enum                                                         | **(Inspector)** Specifies how to apply the per-particle color attribute to the particles. The options are:<br/>&#8226; **None**: Disregards the color attribute.<br/>&#8226; **Base Color**: Uses the color attribute as the particle's base color.<br/>&#8226; **Emissive**: Uses the color attribute as the particle's emissive color.<br/>&#8226; **Base Color and Emissive**: Uses the color attribute as both the particle's base color and emissive color. |
| **Use Emissive**                  | Bool                                                         | **(Inspector)** Indicates whether the output supports emissive particles. Enable this setting to expose the **Emissive Color** property which you can use to make particles glow. |
| **Double-Sided**                  | Bool                                                         | **(Inspector)** Indicates whether one-sided particles, like quads, render from both sides. This flips the normals for particles when they are viewed from behind which means they also receive correct lighting information.<br/>Note: If you enable this setting, also set **Cull Mode** to **Off**. Otherwise, Unity culls the backside of the particle and thus does not render both sides of the particle. |
| **Preserve Specular Lighting**    | Bool                                                         | **(Inspector)** Indicates whether to render specular lighting regardless of the particle’s opacity. This setting is useful for creating glass-like effects where transparent particles still reflect light. |
| **Diffusion Profile Asset**       | [Diffusion Profile](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/Diffusion-Profile.html) | **(Inspector)** The Diffusion Profile that determines how to simulate light which passes through the particle. <br/>Note: You also must add the Diffusion Profile to the [HDRP asset](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/HDRP-Asset.html**) or to a [Volume’s](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/Volumes.html) [Diffusion Profile Override](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/Override-Diffusion-Profile.html) in the Scene .<br/>This setting only appears if you set **Material Type** to **Translucent** or **Simple Lit Translucent**. |
| **Multiply Thickness With Alpha** | Bool                                                         | **(Inspector)** Indicates whether to multiply the **Thickness** of the particle by its alpha value.<br/>This setting only appears if you set **Material Type** to **Translucent** or **Simple Lit Translucent**. |
| **Enable Shadows**                | Bool                                                         | **(Inspector)** Indicates whether the particle receives shadows.<br/>This setting only appears if you set **Material Type** to **Simple Lit** or **Simple Lit Translucent**. |
| **Enable Specular**               | Bool                                                         | **(Inspector)** Indicates whether the particle receives specular highlights.<br/>This setting only appears if you set **Material Type** to **Simple Lit** or **Simple Lit Translucent**. |
| **Enable Cookie**                 | Bool                                                         | **(Inspector)** indicates whether light cookies affect the particle or not. This setting only appears if you set **Material Type** to **Simple Lit** or **Simple Lit Translucent**. |
| **Enable Env Light**              | Bool                                                         | **(Inspector)** Indicates whether the particle receives the environment light set in the global [Volume Profile](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/Volume-Profile.html).<br/>This setting only appears if you set **Material Type** to **Simple Lit** or **Simple Lit Translucent**. |

## Context properties

| **Input**                 | **Type**       | **Description**                                              |
| ------------------------- | -------------- | ------------------------------------------------------------ |
| **Smoothness**            | Float (slider) | The smoothness of the particle. Smoother surfaces bounce light off more uniformly, creating clearer reflections. |
| **Metallic**              | Float (slider) | The metallicity of the particle. Metallic surfaces reflect their environment more, making their albedo color less visible.<br/>This property only appears if **Material Type** is set to **Standard** or **Simple Lit**. |
| **Normal Bending Factor** | Float (slider) | The amount by which to bend the normals. More bent normals create a rounder look.<br/>This property only appears if you enable **Normal Bending**. |
| **Thickness**             | Float (slider) | The thickness of the translucent particles. This affects the influence of the **Diffusion Profile Asset**.<br/>This property only appears if **Material Type** is set to **Translucent** or **Simple Lit Translucent**. |
| **Base Color Map**        | Texture2D      | The base color (RGB) and opacity (A) of the particle.<br/>This property only appears if you set **Use Base Color Map** to anything other than ‘None’. |
| **Mask Map**              | Texture2D      | The [mask map](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@latest/index.html?subfolder=/manual/Mask-Map-and-Detail-Map.html) for the particle - Metallic (R), Ambient Occlusion (G), and Smoothness (A).<br/>This property only appears if you enable **Use Mask Map**. |
| **Normal Map**            | Texture2D      | The normal map the system uses to get normals, in the tangent space, for the particle.<br/>This property only appears if you enable **Use Normal Map**. |
| **Emissive Map**          | Texture2D      | The emissive map (RGB) the system uses to make particles glow.<br/>This property only appears if you enable **Use Emissive Map**. |
| **Base Color**            | Color          | The base color of the particle.<br/>This property only appears if **Color Mode** is set to **None**. |
| **Emissive Color**        | Color          | The emissive color the system uses to make particles glow.<br/>This property only appears if you enable **Use Emissive**. |
| **Specular Color**        | Color          | The specular color of the particle.<br/>This property only appears if **Material Type** is set to **Specular Color**. |