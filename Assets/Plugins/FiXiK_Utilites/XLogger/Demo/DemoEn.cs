using UnityEngine;

namespace FiXiK.CustomLogger
{
    public class DemoEn : MonoBehaviour
    {
        [SerializeField] private bool _isLog = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                XLogger.Log("This is a normal log");
                XLogger.LogWarning("This is a warning log");
                XLogger.LogError("This is an error log");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                XLogger.ClearConsole();
                XLogger.Log("Cleared the console");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                XLogger.Log($"Displayed only if {_isLog} == true", _isLog);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                XLogger.Log($"Colored this message in green", Color.green);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                XLogger.Log($"This log is tagged: on press it points to the object this component is attached to", this);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                XLogger.Log($"If you need bold italic", FontStyle.BoldAndItalic);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                XLogger.Message("This is the most flexible option. You can add anything." +
                    "\nHere I passed the camera as context so it highlights on click " +
                    "\nAlso chose a tag from the list, changed the message color, added bold font, and made it a warning")
                    .WithColor(LogColor.Pink)
                    .WithFontStyle(FontStyle.Bold)
                    .WithContext(Camera.main)
                    .WithLogType(XLogType.LogWarning)
                    .WithTag(TagType.INPUT)
                    .Log();
            }
        }
    }
}