using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Manages character conversations and topic matching
/// </summary>
public class ConversationManager
{
    private readonly Adventure _adventure;
    private readonly GameState _state;

    public ConversationManager(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _state = state;
    }

    /// <summary>
    /// Process a conversation command with a character
    /// </summary>
    public string ProcessConversation(string characterKey, string? topic = null)
    {
        if (!_adventure.Characters.TryGetValue(characterKey, out var character))
        {
            return "That character is not here.";
        }

        // Check if character is at player location
        var charLocation = _state.CharacterLocations.GetValueOrDefault(characterKey);
        if (charLocation != _state.CurrentLocationKey)
        {
            return $"{character.FullName} is not here.";
        }

        // If no topic specified, show general greeting
        if (string.IsNullOrWhiteSpace(topic))
        {
            return ShowGreeting(character);
        }

        // Try to match topic
        var matchedTopic = FindMatchingTopic(character, topic);
        if (matchedTopic != null)
        {
            return matchedTopic.Response;
        }

        // No topic matched - show default response
        if (!string.IsNullOrEmpty(character.UnknownTopicResponse))
        {
            return character.UnknownTopicResponse;
        }

        return $"{character.FullName} doesn't know anything about that.";
    }

    /// <summary>
    /// Ask character about a specific topic
    /// </summary>
    public string AskAbout(string characterKey, string topic)
    {
        return ProcessConversation(characterKey, topic);
    }

    /// <summary>
    /// Tell character about a topic
    /// </summary>
    public string TellAbout(string characterKey, string topic)
    {
        if (!_adventure.Characters.TryGetValue(characterKey, out var character))
        {
            return "That character is not here.";
        }

        // Check if character is at player location
        var charLocation = _state.CharacterLocations.GetValueOrDefault(characterKey);
        if (charLocation != _state.CurrentLocationKey)
        {
            return $"{character.FullName} is not here.";
        }

        // Try to find a topic match
        var matchedTopic = FindMatchingTopic(character, topic);
        if (matchedTopic != null && !string.IsNullOrEmpty(matchedTopic.TellResponse))
        {
            return matchedTopic.TellResponse;
        }

        return $"{character.FullName} listens politely.";
    }

    /// <summary>
    /// Show character's general greeting
    /// </summary>
    private string ShowGreeting(Character character)
    {
        if (!string.IsNullOrEmpty(character.GeneralGreeting))
        {
            return character.GeneralGreeting;
        }

        return $"{character.FullName} nods at you.";
    }

    /// <summary>
    /// Find a matching conversation topic
    /// </summary>
    private ConversationTopic? FindMatchingTopic(Character character, string input)
    {
        if (character.Topics.Count == 0)
            return null;

        input = input.Trim().ToLower();

        // Try exact topic name match first
        var exactMatch = character.Topics.FirstOrDefault(t =>
            t.TopicName.Equals(input, StringComparison.OrdinalIgnoreCase));

        if (exactMatch != null)
            return exactMatch;

        // Try keyword matching
        foreach (var topic in character.Topics)
        {
            if (string.IsNullOrEmpty(topic.Keywords))
                continue;

            var keywords = topic.Keywords.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(k => k.Trim().ToLower());

            foreach (var keyword in keywords)
            {
                if (input.Contains(keyword) || keyword.Contains(input))
                {
                    return topic;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Get list of available topics for a character
    /// </summary>
    public List<string> GetAvailableTopics(string characterKey)
    {
        if (!_adventure.Characters.TryGetValue(characterKey, out var character))
        {
            return new List<string>();
        }

        return character.Topics
            .Where(t => !string.IsNullOrEmpty(t.TopicName))
            .Select(t => t.TopicName)
            .ToList();
    }

    /// <summary>
    /// Check if a character has any conversation topics
    /// </summary>
    public bool HasTopics(string characterKey)
    {
        if (!_adventure.Characters.TryGetValue(characterKey, out var character))
        {
            return false;
        }

        return character.Topics.Count > 0;
    }
}
