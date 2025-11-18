using System.Xml;

namespace ADRIFT.Engine.FileIO;

/// <summary>
/// Extension methods for writing ADRIFT TAF XML format
/// </summary>
internal static class TafXmlWriter
{
    public static void WriteLocations(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var location in adventure.Locations)
        {
            writer.WriteStartElement("Location");

            writer.WriteElementString("Key", location.Key);
            WriteDescription(writer, "ShortDescription", location.ShortDescription);
            WriteDescription(writer, "LongDescription", location.LongDescription);

            if (!string.IsNullOrWhiteSpace(location.ViewFromHereDescription))
                WriteDescription(writer, "ViewFromHere", location.ViewFromHereDescription);

            // Write directions/movements
            foreach (var direction in location.Directions)
            {
                writer.WriteStartElement("Movement");
                writer.WriteElementString("Direction", direction.Direction);
                writer.WriteElementString("Destination", direction.DestinationKey);
                writer.WriteEndElement(); // Movement
            }

            // Write properties
            foreach (var prop in location.Properties)
            {
                writer.WriteStartElement("Property");
                writer.WriteElementString("Key", prop.Key);
                writer.WriteElementString("Value", prop.Value);
                writer.WriteEndElement(); // Property
            }

            if (location.HideOnMap)
                writer.WriteElementString("HideOnMap", "1");

            if (location.IsLibrary)
                writer.WriteElementString("Library", "1");

            writer.WriteElementString("LastUpdated", location.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));

            writer.WriteEndElement(); // Location
        }
    }

    public static void WriteObjects(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var obj in adventure.Objects)
        {
            writer.WriteStartElement("Object");

            writer.WriteElementString("Key", obj.Key);
            writer.WriteElementString("Article", obj.Article);

            if (!string.IsNullOrEmpty(obj.Prefix))
                writer.WriteElementString("Prefix", obj.Prefix);

            writer.WriteElementString("Name", obj.Name);

            // Write aliases
            if (obj.Aliases.Count > 0)
            {
                writer.WriteStartElement("Names");
                writer.WriteElementString("Name", obj.Name); // Primary name
                foreach (var alias in obj.Aliases)
                {
                    writer.WriteElementString("Name", alias);
                }
                writer.WriteEndElement(); // Names
            }

            WriteDescription(writer, "Description", obj.Description);

            // Write properties (including physical properties)
            WriteObjectProperties(writer, obj);

            if (obj.IsLibrary)
                writer.WriteElementString("Library", "1");

            writer.WriteElementString("LastUpdated", obj.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));

            writer.WriteEndElement(); // Object
        }
    }

    private static void WriteObjectProperties(XmlTextWriter writer, ObjectData obj)
    {
        // Write physical properties
        WriteProperty(writer, "Size", obj.Size.ToString());
        WriteProperty(writer, "Weight", obj.Weight.ToString());

        if (obj.IsStatic) WriteProperty(writer, "Static", "1");
        if (obj.IsContainer) WriteProperty(writer, "Container", "1");
        if (obj.IsSurface) WriteProperty(writer, "Surface", "1");
        if (obj.IsWearable) WriteProperty(writer, "Wearable", "1");
        if (obj.IsEdible) WriteProperty(writer, "Edible", "1");
        if (obj.IsLightSource) WriteProperty(writer, "LightSource", "1");
        if (obj.IsReadable) WriteProperty(writer, "Readable", "1");

        if (obj.IsContainer)
        {
            WriteProperty(writer, "Capacity", obj.Capacity.ToString());
            if (obj.IsOpenable) WriteProperty(writer, "Openable", "1");
            if (obj.IsLockable) WriteProperty(writer, "Lockable", "1");
            WriteProperty(writer, "Open", obj.IsOpen ? "1" : "0");
            if (obj.IsLocked) WriteProperty(writer, "Locked", "1");
        }

        // Write custom properties
        foreach (var prop in obj.Properties)
        {
            // Skip properties we've already written
            var key = prop.Key.ToLower();
            if (key != "size" && key != "weight" && key != "static" && key != "container" &&
                key != "surface" && key != "wearable" && key != "edible" && key != "lightsource" &&
                key != "readable" && key != "capacity" && key != "openable" && key != "lockable" &&
                key != "open" && key != "locked")
            {
                WriteProperty(writer, prop.Key, prop.Value);
            }
        }
    }

    private static void WriteProperty(XmlTextWriter writer, string key, string value)
    {
        writer.WriteStartElement("Property");
        writer.WriteElementString("Key", key);
        writer.WriteElementString("Value", value);
        writer.WriteEndElement(); // Property
    }

    public static void WriteTasks(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var task in adventure.Tasks)
        {
            writer.WriteStartElement("Task");

            writer.WriteElementString("Key", task.Key);
            writer.WriteElementString("Description", task.Description);
            writer.WriteElementString("Priority", task.Priority.ToString());
            writer.WriteElementString("Type", task.TaskType);

            // Write commands
            foreach (var command in task.Commands)
            {
                writer.WriteElementString("Command", command);
            }

            if (!string.IsNullOrWhiteSpace(task.CompletionMessage))
                WriteDescription(writer, "CompletionMessage", task.CompletionMessage);

            if (task.Score > 0)
                writer.WriteElementString("Score", task.Score.ToString());

            if (task.IsLibrary)
                writer.WriteElementString("Library", "1");

            writer.WriteElementString("LastUpdated", task.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));

            writer.WriteEndElement(); // Task
        }
    }

    public static void WriteCharacters(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var character in adventure.Characters)
        {
            writer.WriteStartElement("Character");

            writer.WriteElementString("Key", character.Key);
            writer.WriteElementString("Name", character.Name);
            writer.WriteElementString("Gender", character.Gender);

            WriteDescription(writer, "Description", character.Description);

            // Write properties
            foreach (var prop in character.Properties)
            {
                WriteProperty(writer, prop.Key, prop.Value);
            }

            // Write conversation topics
            if (character.Topics.Count > 0)
            {
                foreach (var topic in character.Topics)
                {
                    writer.WriteStartElement("Topic");
                    writer.WriteElementString("Keyword", topic.Keyword);
                    writer.WriteElementString("Response", topic.Response);
                    writer.WriteEndElement(); // Topic
                }
            }

            if (character.IsLibrary)
                writer.WriteElementString("Library", "1");

            writer.WriteElementString("LastUpdated", character.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));

            writer.WriteEndElement(); // Character
        }
    }

    public static void WriteEvents(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var evt in adventure.Events)
        {
            writer.WriteStartElement("Event");

            writer.WriteElementString("Key", evt.Key);
            writer.WriteElementString("Description", evt.Description);
            writer.WriteElementString("Type", evt.EventType);

            if (evt.DelayTurns > 0)
                writer.WriteElementString("DelayTurns", evt.DelayTurns.ToString());

            if (evt.RepeatInterval > 0)
                writer.WriteElementString("RepeatInterval", evt.RepeatInterval.ToString());

            if (evt.IsRepeating)
                writer.WriteElementString("Repeating", "1");

            if (evt.IsLibrary)
                writer.WriteElementString("Library", "1");

            writer.WriteElementString("LastUpdated", evt.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));

            writer.WriteEndElement(); // Event
        }
    }

    public static void WriteVariables(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var variable in adventure.Variables)
        {
            writer.WriteStartElement("Variable");

            writer.WriteElementString("Key", variable.Key);
            writer.WriteElementString("Name", variable.Name);
            writer.WriteElementString("Type", variable.Type);

            // Write value based on type
            string value = variable.Type.ToLower() switch
            {
                "integer" => variable.IntValue.ToString(),
                "text" => variable.StringValue,
                "boolean" => variable.BoolValue ? "1" : "0",
                _ => variable.InitialValue
            };

            writer.WriteElementString("Value", value);

            writer.WriteEndElement(); // Variable
        }
    }

    public static void WriteGroups(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var group in adventure.Groups)
        {
            writer.WriteStartElement("Group");

            writer.WriteElementString("Key", group.Key);
            writer.WriteElementString("Name", group.Name);
            writer.WriteElementString("GroupType", group.GroupType);

            // Write members
            foreach (var member in group.Members)
            {
                writer.WriteElementString("Member", member);
            }

            writer.WriteEndElement(); // Group
        }
    }

    public static void WriteHints(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var hint in adventure.Hints)
        {
            writer.WriteStartElement("Hint");

            writer.WriteElementString("Key", hint.Key);
            writer.WriteElementString("Question", hint.Question);

            // Write hint texts
            for (int i = 0; i < hint.HintTexts.Count; i++)
            {
                writer.WriteElementString($"Hint{i + 1}", hint.HintTexts[i]);
            }

            writer.WriteEndElement(); // Hint
        }
    }

    public static void WriteSynonyms(XmlTextWriter writer, AdventureData adventure)
    {
        foreach (var synonym in adventure.Synonyms)
        {
            writer.WriteStartElement("Synonym");

            writer.WriteElementString("Key", synonym.Key);
            writer.WriteElementString("CommonName", synonym.CommonName);

            // Write replacements
            foreach (var replacement in synonym.Synonyms)
            {
                writer.WriteElementString("Replacement", replacement);
            }

            writer.WriteEndElement(); // Synonym
        }
    }

    private static void WriteDescription(XmlTextWriter writer, string elementName, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        writer.WriteStartElement(elementName);
        writer.WriteStartElement("Description");
        writer.WriteElementString("DisplayWhen", "Always");
        writer.WriteElementString("Text", text);
        writer.WriteEndElement(); // Description
        writer.WriteEndElement(); // elementName
    }
}
