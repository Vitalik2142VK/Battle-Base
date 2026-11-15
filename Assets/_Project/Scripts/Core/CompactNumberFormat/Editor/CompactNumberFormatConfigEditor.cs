using System;
using System.Globalization;
using System.Numerics;
using UnityEditor;
using UnityEngine;

namespace BattleBase.Core.Formatting.CompactNumber.EditorTools
{
    [CustomEditor(typeof(CompactNumberFormatConfig))]
    public class CompactNumberFormatConfigEditor : Editor
    {
        private const int ResultLabelWidth = 140;
        private const int SpaceAbovePreview = 8;
        private const int SpaceAboveSamples = 4;
        private const string PreviewLabel = "Preview";
        private const string SamplesLabel = "Samples";
        private const string CustomValueLabel = "Custom value";
        private const string ResetButtonLabel = "Reset to defaults";
        private const string EmptyCustomValueOutput = "—";
        private const string InvalidNumberOutput = "<invalid number>";
        private const string ErrorOutputTemplate = "<error: {0}>";
        private const string RawNumberFormat = "N0";
        private const string InitialCustomValue = "1234567";

        private static readonly BigInteger[] _samples =
        {
            BigInteger.Zero,
            BigInteger.One,
            new(999),
            new(1_000),
            new(1_234),
            new(1_500),
            new(9_999),
            new(10_000),
            new(999_999),
            new(1_000_000),
            new(1_234_567),
            new(1_000_000_000),
            new(1_234_567_890),
            new(1_000_000_000_000L),
            BigInteger.Pow(10, 15),
            BigInteger.Pow(10, 18),
            BigInteger.Pow(10, 21),
            BigInteger.Pow(10, 30),
            BigInteger.Pow(10, 45),
            BigInteger.Pow(10, 60),
            BigInteger.Pow(10, 75),
            BigInteger.Pow(10, 76),
            new(-1_500),
        };

        private string _customValueInput = InitialCustomValue;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CompactNumberFormatConfig config = (CompactNumberFormatConfig)target;

            DrawResetButton(config);

            EditorGUILayout.Space(SpaceAbovePreview);
            EditorGUILayout.LabelField(PreviewLabel, EditorStyles.boldLabel);

            DrawCustomValueInput(config);

            EditorGUILayout.Space(SpaceAboveSamples);
            EditorGUILayout.LabelField(SamplesLabel, EditorStyles.miniBoldLabel);

            DrawSampleRows(config);
        }

        private static void DrawResetButton(CompactNumberFormatConfig config)
        {
            if (GUILayout.Button(ResetButtonLabel) == false)
                return;

            config.ResetToDefaults();
            EditorUtility.SetDirty(config);
        }

        private void DrawCustomValueInput(CompactNumberFormatConfig config)
        {
            EditorGUILayout.BeginHorizontal();
            _customValueInput = EditorGUILayout.TextField(CustomValueLabel, _customValueInput);
            EditorGUILayout.LabelField(FormatCustomValue(config), GUILayout.Width(ResultLabelWidth));
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSampleRows(CompactNumberFormatConfig config)
        {
            foreach (BigInteger sample in _samples)
                DrawSampleRow(sample, config);
        }

        private void DrawSampleRow(BigInteger sample, CompactNumberFormatConfig config)
        {
            string rawValue = sample.ToString(RawNumberFormat, CultureInfo.InvariantCulture);

            EditorGUILayout.LabelField(
                new GUIContent(rawValue),
                new GUIContent(FormatValue(sample, config)));
        }

        private string FormatCustomValue(CompactNumberFormatConfig config)
        {
            if (string.IsNullOrWhiteSpace(_customValueInput))
                return EmptyCustomValueOutput;

            if (BigInteger.TryParse(
                    _customValueInput.Trim(),
                    NumberStyles.Integer | NumberStyles.AllowThousands,
                    CultureInfo.InvariantCulture,
                    out BigInteger value) == false)
                return InvalidNumberOutput;

            return FormatValue(value, config);
        }

        private static string FormatValue(BigInteger value, CompactNumberFormatConfig config)
        {
            try
            {
                return CompactNumberFormatter.Format(value, config);
            }
            catch (Exception exception)
            {
                return string.Format(ErrorOutputTemplate, exception.Message);
            }
        }
    }
}