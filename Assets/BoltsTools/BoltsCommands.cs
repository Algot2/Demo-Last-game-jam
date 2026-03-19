using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BoltsTools
{
    public class BoltsCommands : MonoBehaviour
    {
        public static BoltsCommands command;

        List<Command> commands = new();

        public static bool isTyping;

        string commandTyped = "";

        void Update()
        {
            if (LoadBoltsDebugMenu._settings != null &&
                Input.GetKeyDown(LoadBoltsDebugMenu._settings.keyToOpenCommands) && !isTyping)
            {
                isTyping = true;
            }

            if (isTyping && Input.GetKeyDown(KeyCode.Escape))
            {
                commandTyped = "";
                isTyping = false;
            }
        }

        /// <summary>
        /// Adds A Command
        /// </summary>
        /// <param name="commandName">The Name For The Command</param>
        /// <param name="methodName">The Name Of The Method The Command Should Run (Dont Add Arguments)</param>
        /// <param name="target">What Script On What Game Object Should Run The Command</param>
        public void AddCommand(string commandName, string methodName, MonoBehaviour target)
        {
            string finalName = methodName.Replace(" ", "");

            int index = -1;
            index = commands.FindIndex(x => x.name == commandName);
            if (index > -1)
            {
                MethodInfo method = target.GetType().GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method == null)
                {
                    Debug.Log($"No Method Named: {methodName} Found");
                }

                commands[index].method = method;
            }
            else
            {
                MethodInfo method = target.GetType().GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method == null)
                {
                    Debug.Log($"No Method Named: {methodName} Found");
                }

                commands.Add(new() { name = commandName, method = method, target = target });
            }
        }

        void RunCommand()
        {
            string input = commandTyped.Trim();
            commandTyped = "";
            isTyping = false;

            if (string.IsNullOrWhiteSpace(input))
            {
                Debug.Log("No Command Typed");
                return;
            }

            List<string> tokens = ParseTokens(input);

            if (tokens.Count == 0)
                return;

            string commandName = tokens[0];

            int commandIndex = -1;
            commandIndex = commands.FindIndex(x => x.name == commandName);

            if (commandIndex == -1)
            {
                Debug.LogError($"{commandName} Is An Unknown Command");
                return;
            }

            Command cmd = commands[commandIndex];
            ParameterInfo[] parameters = cmd.method.GetParameters();
            List<object> arguments = new();

            for (int i = 0; i < parameters.Length; i++)
            {
                int tokenIndex = i + 1;

                if (tokenIndex >= tokens.Count)
                {
                    Debug.LogWarning(
                        $"Command {commandName} Expected {parameters.Length} Arguments(s) But Recived {i}");
                    return;
                }

                object converted;

                if (parameters[i].ParameterType == typeof(Vector3))
                {
                    string[] parts = tokens[tokenIndex].Split(",");
                    if (parts.Length != 3 ||
                        !float.TryParse(parts[0], out float x) ||
                        !float.TryParse(parts[1], out float y) ||
                        !float.TryParse(parts[2], out float z))
                    {
                        Debug.LogWarning($"Could Not Parse '{tokens[tokenIndex]}' As A Vector3. Use Format: 'X,Y,Z'");
                        return;
                    }

                    converted = new Vector3(x, y, z);
                }
                else if (parameters[i].ParameterType == typeof(Vector2))
                {
                    string[] parts = tokens[tokenIndex].Split(",");
                    if (parts.Length != 2 ||
                        !float.TryParse(parts[0], out float x) ||
                        !float.TryParse(parts[1], out float y))
                    {
                        Debug.LogWarning($"Could Not Parse '{tokens[tokenIndex]}' As A Vector2. Use Format: 'X,Y'");
                        return;
                    }

                    converted = new Vector2(x, y);
                }
                else
                {
                    try
                    {
                        converted = Convert.ChangeType(tokens[tokenIndex], parameters[i].ParameterType);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning(
                            $"Could Not Convert Argument '{tokens[tokenIndex]}' To {parameters[i].ParameterType.Name}: {e.Message}");
                        return;
                    }
                }

                arguments.Add(converted);
            }

            cmd.method.Invoke(cmd.target, arguments.ToArray());
        }

        void OnGUI()
        {
            if (isTyping)
            {
                GUIStyle style = new GUIStyle(GUI.skin.textArea);
                style.fontSize = 50;

                GUI.SetNextControlName("Command");
                commandTyped = GUI.TextArea(new(0, Screen.height - 200, Screen.width, 150), commandTyped, style);

                GUI.FocusControl("Command");

                if (commandTyped.EndsWith("\n") && commandTyped.Length > 0)
                    RunCommand();
            }
        }

        List<string> ParseTokens(string input)
        {
            List<string> tokens = new();
            string[] words = input.Split(" ");

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].StartsWith("{"))
                {
                    string grouped = "";
                    for (int j = i; j < words.Length; j++)
                    {
                        grouped += (j == i ? "" : " ") + words[j];
                        if (words[i].EndsWith("}"))
                        {
                            i = j;
                            break;
                        }
                    }

                    tokens.Add(grouped.Replace("{", "").Replace("}", ""));
                }
                else if (!string.IsNullOrEmpty(words[i]))
                    tokens.Add(words[i]);
            }

            return tokens;
        }

        void Awake()
        {
            command = this;
        }
    }

    class Command
    {
        public string name;
        public MethodInfo method;
        public object target;
    }
}