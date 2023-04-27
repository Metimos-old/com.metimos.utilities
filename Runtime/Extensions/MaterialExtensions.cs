using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Metimos
{
	[SuppressMessage("ReSharper", "StringLiteralTypo")]
	public static class MaterialExtensions
	{
		private static readonly int k_mode = Shader.PropertyToID("_Mode");
		private static readonly int k_srcBlend = Shader.PropertyToID("_SrcBlend");
		private static readonly int k_dstBlend = Shader.PropertyToID("_DstBlend");
		private static readonly int k_zWrite = Shader.PropertyToID("_ZWrite");
			
		public static void SetOpaque(this Material material)
		{
			material.SetOverrideTag("RenderType", "");
			material.SetFloat(k_mode, 0f);
			material.SetFloat(k_srcBlend, (float)UnityEngine.Rendering.BlendMode.One);
			material.SetFloat(k_dstBlend, (float)UnityEngine.Rendering.BlendMode.Zero);
			material.SetFloat(k_zWrite, 1f);
			material.DisableKeyword("_ALPHATEST_ON");
			material.DisableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			
			material.renderQueue = -1;
		}
	
		public static void SetFade(this Material material)
		{
			material.SetOverrideTag("RenderType", "Transparent");
			material.SetFloat(k_mode, 2f);
			material.SetFloat(k_srcBlend, (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
			material.SetFloat(k_dstBlend, (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetFloat(k_zWrite, 0f);
			material.DisableKeyword("_ALPHATEST_ON");
			material.EnableKeyword("_ALPHABLEND_ON");
			material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			
			material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
		}
		
		public static void SetTransparent(this Material material)
		{
			material.SetOverrideTag("RenderType", "Transparent");
			material.SetFloat(k_mode, 3f);
			material.SetFloat(k_srcBlend, (float)UnityEngine.Rendering.BlendMode.One);
			material.SetFloat(k_dstBlend, (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			material.SetFloat(k_zWrite, 0f);
			material.DisableKeyword("_ALPHATEST_ON");
			material.DisableKeyword("_ALPHABLEND_ON");
			material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
			
			material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
		}
	}
}