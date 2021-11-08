﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;


namespace Prime31Editor
{
	// Note: This class uses UnityEditorInternal which is an undocumented internal feature
	public class ConstantsGeneratorKit : MonoBehaviour
	{
		private const string FOLDER_LOCATION = "Scripts/Keys/";
		private const string NAMESPACE = "k";
		private static ConstantNamingStyle CONSTANT_NAMING_STYLE = ConstantNamingStyle.UppercaseWithUnderscores;
		private const string DIGIT_PREFIX = "k";
		private static bool SHOW_SUCCESS_MESSAGE = false;

		private const string TAGS_FILE_NAME = "Tags.cs";
		private const string LAYERS_FILE_NAME = "Layers.cs";

		[MenuItem("Edit/Generate Constants Classes...")]
		static void rebuildConstantsClassesMenuItem()
		{
			rebuildConstantsClasses();
		}


		public static void rebuildConstantsClasses(bool buildResources = true, bool buildScenes = true, bool buildTagsAndLayers = true, bool buildSortingLayers = true)
		{
			var folderPath = Application.dataPath + "/" + FOLDER_LOCATION;
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			if (buildTagsAndLayers)
			{
				File.WriteAllText(folderPath + TAGS_FILE_NAME, getClassContent(TAGS_FILE_NAME.Replace(".cs", string.Empty), UnityEditorInternal.InternalEditorUtility.tags));
				File.WriteAllText(folderPath + LAYERS_FILE_NAME, getLayerClassContent(LAYERS_FILE_NAME.Replace(".cs", string.Empty), UnityEditorInternal.InternalEditorUtility.layers));

				AssetDatabase.ImportAsset("Assets/" + FOLDER_LOCATION + TAGS_FILE_NAME, ImportAssetOptions.ForceUpdate);
				AssetDatabase.ImportAsset("Assets/" + FOLDER_LOCATION + LAYERS_FILE_NAME, ImportAssetOptions.ForceUpdate);
			}

			if (SHOW_SUCCESS_MESSAGE && buildResources && buildScenes && buildTagsAndLayers)
				Debug.Log("ConstantsGeneratorKit complete. Constants classes built to " + FOLDER_LOCATION);
		}

		private static string getClassContent(string className, string[] labelsArray)
		{
			var output = "";
			output += "//This class is auto-generated do not modify\n";
			output += "namespace " + NAMESPACE + "\n";
			output += "{\n";
			output += "\tpublic static class " + className + "\n";
			output += "\t{\n";

			foreach (var label in labelsArray)
				output += "\t\t" + buildConstVariable(label) + "\n";

			output += "\t}\n";
			output += "}";

			return output;
		}


		private static string getLayerClassContent(string className, string[] labelsArray)
		{
			var output = "";
			output += "// This class is auto-generated do not modify\n";
			output += "namespace " + NAMESPACE + "\n";
			output += "{\n";
			output += "\tpublic static class " + className + "\n";
			output += "\t{\n";

			foreach (var label in labelsArray)
				output += "\t\t" + "public const int " + formatConstVariableName(label) + " = " + LayerMask.NameToLayer(label) + ";\n";

			output += "\n\n";
			output += @"		public static int onlyIncluding( params int[] layers )
		{
			int mask = 0;
			for( var i = 0; i < layers.Length; i++ )
				mask |= ( 1 << layers[i] );
			return mask;
		}
		public static int everythingBut( params int[] layers )
		{
			return ~onlyIncluding( layers );
		}";

			output += "\n";
			output += "\t}\n";
			output += "}";

			return output;
		}

		private static string buildConstVariable(string varName, string suffix = "", string value = null)
		{
			value = value ?? varName;
			return "public const string " + formatConstVariableName(varName) + suffix + " = " + '"' + value + '"' + ";";
		}


		private static string formatConstVariableName(string input)
		{
			switch (CONSTANT_NAMING_STYLE)
			{
				case ConstantNamingStyle.UppercaseWithUnderscores:
					return toUpperCaseWithUnderscores(input);
				case ConstantNamingStyle.CamelCase:
					return toCamelCase(input);
				default:
					return toUpperCaseWithUnderscores(input);
			}
		}

		private static string toCamelCase(string input)
		{
			input = input.Replace(" ", "");

			if (char.IsLower(input[0]))
				input = char.ToUpper(input[0]) + input.Substring(1);

			// uppercase letters before dash or underline
			Func<char, int, string> func = (x, i) => {
				if (x == '-' || x == '_')
					return "";

				if (i > 0 && (input[i - 1] == '-' || input[i - 1] == '_'))
					return x.ToString().ToUpper();

				return x.ToString();
			};
			input = string.Concat(input.Select(func).ToArray());

			// digits are a no-no so stick prefix in front
			if (char.IsDigit(input[0]))
				return DIGIT_PREFIX + input;
			return input;
		}

		private static string toUpperCaseWithUnderscores(string input)
		{
			input = input.Replace("-", "_");
			input = Regex.Replace(input, @"\s+", "_");

			// make camel-case have an underscore between letters
			Func<char, int, string> func = (x, i) =>
			{
				if (i > 0 && char.IsUpper(x) && char.IsLower(input[i - 1]))
					return "_" + x.ToString();
				return x.ToString();
			};
			input = string.Concat(input.Select(func).ToArray());

			// digits are a no-no so stick prefix in front
			if (char.IsDigit(input[0]))
				return DIGIT_PREFIX + input.ToUpper();
			return input.ToUpper();
		}

		private enum ConstantNamingStyle
		{
			UppercaseWithUnderscores,
			CamelCase
		}
	}

}
#endif