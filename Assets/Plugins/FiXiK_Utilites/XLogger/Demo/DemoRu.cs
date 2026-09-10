using UnityEngine;

namespace FiXiK.CustomLogger
{
    public class DemoRu : MonoBehaviour
    {
        [SerializeField] private bool _isLog = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                XLogger.Log("Это обычный лог");
                XLogger.LogWarning("Это лог с предупреждением");
                XLogger.LogError("Это лог с ошибкой");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                XLogger.ClearConsole();
                XLogger.Log("Очистил консоль");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                XLogger.Log($"Выводится только если {_isLog} == true", _isLog);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                XLogger.Log($"Покрасил это сообщение в зелёный", Color.green);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                XLogger.Log($"Этот лог тегается: при нажатии он указывает на объект, на котором висит компонент", this);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                XLogger.Log($"Если надо сделать полужирным курсивом", FontStyle.BoldAndItalic);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                XLogger.Message("А это самый гибкий вариант. Можно добавлять что угодно." +
                    "\nТут бросил контекст на камеру, чтобы её подсвечивало при нажатии " +
                    "\nЕщё выбрал тег из списка и поменял цвет сообщения, добавил полужирный шрифт. И сделал предупреждением")
                    .WithColor(LogColor.Pink)
                    .WithFontStyle(FontStyle.Bold)
                    .WithContext(Camera.main)
                    .WithLogType(XLogType.LogWarning)
                    .WithTag(TagType.ANALYTICS)
                    .Log();
            }
        }
    }
}