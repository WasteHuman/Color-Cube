using UnityEngine;
using UnityEditor;

namespace Minimalist
{
	public class MinimalistSkyEditor : ShaderGUI
	{
		private MaterialProperty _DownColor;
		private MaterialProperty _TopColor;
		private MaterialProperty _Intensity;
		private MaterialProperty _Exponent;
		private MaterialProperty _DirX;
		private MaterialProperty _DirY;
		private MaterialProperty _UpVector;

		private void InitializeMatProps(MaterialProperty[] _props)
		{
            _DownColor = FindProperty("_DownColor", _props);
            _TopColor = FindProperty("_TopColor", _props);
			_Intensity = FindProperty("_Intensity", _props);
			_Exponent = FindProperty("_Exponent", _props);
			_DirX = FindProperty("_DirX", _props);
			_DirY = FindProperty("_DirY", _props);
			_UpVector = FindProperty("_UpVector", _props);
		}

		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			InitializeMatProps(properties);
			EditorGUI.BeginChangeCheck();
			{
				materialEditor.ColorProperty(_DownColor, "Down color");
				materialEditor.ColorProperty(_TopColor, "Top color");

				materialEditor.FloatProperty(_Intensity, "Intensity");
				materialEditor.FloatProperty(_Exponent, "Exponent");

				materialEditor.RangeProperty(_DirY, "Pitch");
				materialEditor.RangeProperty(_DirX, "Yaw");

				float x = _DirX.floatValue * Mathf.Deg2Rad;
				float y = _DirY.floatValue * Mathf.Deg2Rad;

				_UpVector.vectorValue = new Vector4(Mathf.Sin(y) * Mathf.Sin(x), Mathf.Cos(y),
					Mathf.Sin(y) * Mathf.Cos(x), 0.0f);
			}
			if (EditorGUI.EndChangeCheck())
			{
				InitializeMatProps(properties);
			}
		}
	}
}