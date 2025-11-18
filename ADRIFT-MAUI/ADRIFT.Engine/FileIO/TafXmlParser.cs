using System.Xml;

namespace ADRIFT.Engine.FileIO;

/// <summary>
/// Extension methods for parsing ADRIFT TAF XML format
/// </summary>
internal static class TafXmlParser
{
    public static void ParseLocations(XmlElement root, AdventureData adventure)
    {
        var locationNodes = root.GetElementsByTagName("Location");

        foreach (XmlElement locNode in locationNodes)
        {
            var location = new LocationData
            {
                Key = GetStringValue(locNode, "Key", Guid.NewGuid().ToString()),
                ShortDescription = GetStringValue(locNode, "ShortDescription"),
                LongDescription = ParseDescription(locNode, "LongDescription"),
                ViewFromHereDescription = ParseDescription(locNode, "ViewFromHere"),
                HideOnMap = GetBoolValue(locNode, "HideOnMap"),
                IsLibrary = GetBoolValue(locNode, "Library"),
                LastUpdated = GetDateTimeValue(locNode, "LastUpdated", DateTime.Now)
            };

            // Parse directions/movements
            var movementNodes = locNode.GetElementsByTagName("Movement");
            foreach (XmlElement moveNode in movementNodes)
            {
                var direction = new DirectionData
                {
                    Direction = GetStringValue(moveNode, "Direction", "North"),
                    DestinationKey = GetStringValue(moveNode, "Destination")
                };
                location.Directions.Add(direction);
            }

            // Parse properties
            var propNodes = locNode.SelectNodes("Property");
            if (propNodes != null)
            {
                foreach (XmlElement propNode in propNodes)
                {
                    var key = GetStringValue(propNode, "Key");
                    var value = GetStringValue(propNode, "Value");
                    if (!string.IsNullOrEmpty(key))
                        location.Properties[key] = value;
                }
            }

            adventure.Locations.Add(location);
        }
    }

    public static void ParseObjects(XmlElement root, AdventureData adventure)
    {
        var objectNodes = root.GetElementsByTagName("Object");

        foreach (XmlElement objNode in objectNodes)
        {
            var obj = new ObjectData
            {
                Key = GetStringValue(objNode, "Key", Guid.NewGuid().ToString()),
                Article = GetStringValue(objNode, "Article", "a"),
                Prefix = GetStringValue(objNode, "Prefix"),
                Name = GetStringValue(objNode, "Name", "object"),
                Description = ParseDescription(objNode, "Description"),
                IsLibrary = GetBoolValue(objNode, "Library"),
                LastUpdated = GetDateTimeValue(objNode, "LastUpdated", DateTime.Now)
            };

            // Parse aliases (Names node contains multiple Name child elements)
            var namesNode = objNode.SelectSingleNode("Names") as XmlElement;
            if (namesNode != null)
            {
                var nameNodes = namesNode.GetElementsByTagName("Name");
                foreach (XmlElement nameNode in nameNodes)
                {
                    var alias = nameNode.InnerText;
                    if (!string.IsNullOrEmpty(alias) && alias != obj.Name)
                        obj.Aliases.Add(alias);
                }
            }

            // Parse properties (will include physical properties like size, weight, etc.)
            var propNodes = objNode.SelectNodes("Property");
            if (propNodes != null)
            {
                foreach (XmlElement propNode in propNodes)
                {
                    var key = GetStringValue(propNode, "Key");
                    var value = GetStringValue(propNode, "Value");
                    if (!string.IsNullOrEmpty(key))
                    {
                        obj.Properties[key] = value;

                        // Map known properties to strongly-typed fields
                        switch (key.ToLower())
                        {
                            case "size":
                                obj.Size = int.TryParse(value, out int size) ? size : 5;
                                break;
                            case "weight":
                                obj.Weight = int.TryParse(value, out int weight) ? weight : 5;
                                break;
                            case "static":
                                obj.IsStatic = value == "1" || value.ToLower() == "true";
                                break;
                            case "container":
                                obj.IsContainer = value == "1" || value.ToLower() == "true";
                                break;
                            case "surface":
                                obj.IsSurface = value == "1" || value.ToLower() == "true";
                                break;
                            case "wearable":
                                obj.IsWearable = value == "1" || value.ToLower() == "true";
                                break;
                            case "edible":
                                obj.IsEdible = value == "1" || value.ToLower() == "true";
                                break;
                            case "lightsource":
                                obj.IsLightSource = value == "1" || value.ToLower() == "true";
                                break;
                            case "readable":
                                obj.IsReadable = value == "1" || value.ToLower() == "true";
                                break;
                            case "capacity":
                                obj.Capacity = int.TryParse(value, out int cap) ? cap : 0;
                                break;
                            case "openable":
                                obj.IsOpenable = value == "1" || value.ToLower() == "true";
                                break;
                            case "lockable":
                                obj.IsLockable = value == "1" || value.ToLower() == "true";
                                break;
                            case "open":
                                obj.IsOpen = value == "1" || value.ToLower() == "true";
                                break;
                            case "locked":
                                obj.IsLocked = value == "1" || value.ToLower() == "true";
                                break;
                        }
                    }
                }
            }

            adventure.Objects.Add(obj);
        }
    }

    public static void ParseTasks(XmlElement root, AdventureData adventure)
    {
        var taskNodes = root.GetElementsByTagName("Task");

        foreach (XmlElement taskNode in taskNodes)
        {
            var task = new TaskData
            {
                Key = GetStringValue(taskNode, "Key", Guid.NewGuid().ToString()),
                Description = GetStringValue(taskNode, "Description"),
                Priority = GetIntValue(taskNode, "Priority", 50),
                TaskType = GetStringValue(taskNode, "Type", "General"),
                IsLibrary = GetBoolValue(taskNode, "Library"),
                LastUpdated = GetDateTimeValue(taskNode, "LastUpdated", DateTime.Now),
                CompletionMessage = ParseDescription(taskNode, "CompletionMessage"),
                Score = GetIntValue(taskNode, "Score", 0)
            };

            // Parse commands
            var commandNodes = taskNode.GetElementsByTagName("Command");
            foreach (XmlElement cmdNode in commandNodes)
            {
                var cmd = cmdNode.InnerText;
                if (!string.IsNullOrEmpty(cmd))
                    task.Commands.Add(cmd);
            }

            adventure.Tasks.Add(task);
        }
    }

    public static void ParseCharacters(XmlElement root, AdventureData adventure)
    {
        var charNodes = root.GetElementsByTagName("Character");

        foreach (XmlElement charNode in charNodes)
        {
            var character = new CharacterData
            {
                Key = GetStringValue(charNode, "Key", Guid.NewGuid().ToString()),
                Name = GetStringValue(charNode, "Name", "character"),
                Gender = GetStringValue(charNode, "Gender", "Male"),
                Description = ParseDescription(charNode, "Description"),
                IsLibrary = GetBoolValue(charNode, "Library"),
                LastUpdated = GetDateTimeValue(charNode, "LastUpdated", DateTime.Now)
            };

            // Parse properties
            var propNodes = charNode.SelectNodes("Property");
            if (propNodes != null)
            {
                foreach (XmlElement propNode in propNodes)
                {
                    var key = GetStringValue(propNode, "Key");
                    var value = GetStringValue(propNode, "Value");
                    if (!string.IsNullOrEmpty(key))
                        character.Properties[key] = value;
                }
            }

            // Parse conversation topics
            var topicNodes = charNode.GetElementsByTagName("Topic");
            foreach (XmlElement topicNode in topicNodes)
            {
                var topic = new TopicData
                {
                    Keyword = GetStringValue(topicNode, "Keyword"),
                    Response = GetStringValue(topicNode, "Response")
                };
                character.Topics.Add(topic);
            }

            adventure.Characters.Add(character);
        }
    }

    public static void ParseEvents(XmlElement root, AdventureData adventure)
    {
        var eventNodes = root.GetElementsByTagName("Event");

        foreach (XmlElement eventNode in eventNodes)
        {
            var evt = new EventData
            {
                Key = GetStringValue(eventNode, "Key", Guid.NewGuid().ToString()),
                Description = GetStringValue(eventNode, "Description"),
                EventType = GetStringValue(eventNode, "Type", "Immediate"),
                IsLibrary = GetBoolValue(eventNode, "Library"),
                LastUpdated = GetDateTimeValue(eventNode, "LastUpdated", DateTime.Now),
                DelayTurns = GetIntValue(eventNode, "DelayTurns", 0),
                RepeatInterval = GetIntValue(eventNode, "RepeatInterval", 0),
                IsRepeating = GetBoolValue(eventNode, "Repeating")
            };

            adventure.Events.Add(evt);
        }
    }

    public static void ParseVariables(XmlElement root, AdventureData adventure)
    {
        var varNodes = root.GetElementsByTagName("Variable");

        foreach (XmlElement varNode in varNodes)
        {
            var variable = new VariableData
            {
                Key = GetStringValue(varNode, "Key", Guid.NewGuid().ToString()),
                Name = GetStringValue(varNode, "Name", "variable"),
                Type = GetStringValue(varNode, "Type", "Integer"),
                InitialValue = GetStringValue(varNode, "Value", "0")
            };

            // Parse typed values
            switch (variable.Type.ToLower())
            {
                case "integer":
                    variable.IntValue = int.TryParse(variable.InitialValue, out int intVal) ? intVal : 0;
                    break;
                case "text":
                    variable.StringValue = variable.InitialValue;
                    break;
                case "boolean":
                    variable.BoolValue = variable.InitialValue == "1" || variable.InitialValue.ToLower() == "true";
                    break;
            }

            adventure.Variables.Add(variable);
        }
    }

    public static void ParseGroups(XmlElement root, AdventureData adventure)
    {
        var groupNodes = root.GetElementsByTagName("Group");

        foreach (XmlElement groupNode in groupNodes)
        {
            var group = new GroupData
            {
                Key = GetStringValue(groupNode, "Key", Guid.NewGuid().ToString()),
                Name = GetStringValue(groupNode, "Name", "group"),
                GroupType = GetStringValue(groupNode, "GroupType", "Objects")
            };

            // Parse members
            var memberNodes = groupNode.GetElementsByTagName("Member");
            foreach (XmlElement memberNode in memberNodes)
            {
                var memberKey = memberNode.InnerText;
                if (!string.IsNullOrEmpty(memberKey))
                    group.Members.Add(memberKey);
            }

            adventure.Groups.Add(group);
        }
    }

    public static void ParseHints(XmlElement root, AdventureData adventure)
    {
        var hintNodes = root.GetElementsByTagName("Hint");

        foreach (XmlElement hintNode in hintNodes)
        {
            var hint = new HintData
            {
                Key = GetStringValue(hintNode, "Key", Guid.NewGuid().ToString()),
                Question = GetStringValue(hintNode, "Question")
            };

            // Parse hint texts (Hint1, Hint2, etc.)
            for (int i = 1; i <= 10; i++)
            {
                var hintText = GetStringValue(hintNode, $"Hint{i}");
                if (!string.IsNullOrEmpty(hintText))
                    hint.HintTexts.Add(hintText);
                else
                    break;
            }

            adventure.Hints.Add(hint);
        }
    }

    public static void ParseSynonyms(XmlElement root, AdventureData adventure)
    {
        var synNodes = root.GetElementsByTagName("Synonym");

        foreach (XmlElement synNode in synNodes)
        {
            var synonym = new SynonymData
            {
                Key = GetStringValue(synNode, "Key", Guid.NewGuid().ToString()),
                CommonName = GetStringValue(synNode, "CommonName")
            };

            // Parse replacement words
            var replNodes = synNode.GetElementsByTagName("Replacement");
            foreach (XmlElement replNode in replNodes)
            {
                var replacement = replNode.InnerText;
                if (!string.IsNullOrEmpty(replacement))
                    synonym.Synonyms.Add(replacement);
            }

            adventure.Synonyms.Add(synonym);
        }
    }

    // Helper methods
    private static string GetStringValue(XmlElement parent, string elementName, string defaultValue = "")
    {
        var element = parent.SelectSingleNode(elementName) as XmlElement;
        return element?.InnerText ?? defaultValue;
    }

    private static int GetIntValue(XmlElement parent, string elementName, int defaultValue = 0)
    {
        var value = GetStringValue(parent, elementName);
        return int.TryParse(value, out int result) ? result : defaultValue;
    }

    private static bool GetBoolValue(XmlElement parent, string elementName, bool defaultValue = false)
    {
        var value = GetStringValue(parent, elementName);
        return value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    private static DateTime GetDateTimeValue(XmlElement parent, string elementName, DateTime defaultValue)
    {
        var value = GetStringValue(parent, elementName);
        return DateTime.TryParse(value, out DateTime result) ? result : defaultValue;
    }

    private static string ParseDescription(XmlElement parent, string elementName)
    {
        var descElement = parent.SelectSingleNode($"{elementName}/Description/Text") as XmlElement;
        if (descElement != null)
            return descElement.InnerText;

        // Fallback: try direct text element
        descElement = parent.SelectSingleNode(elementName) as XmlElement;
        return descElement?.InnerText ?? "";
    }
}
