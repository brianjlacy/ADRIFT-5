using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Manages character movement including walk routes and following
/// </summary>
public class CharacterMovementManager
{
    private readonly Adventure _adventure;
    private readonly GameState _state;

    public CharacterMovementManager(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _state = state;
    }

    /// <summary>
    /// Process all character movements for the current turn
    /// </summary>
    public void ProcessTurn()
    {
        // First, process characters following the player
        ProcessFollowingCharacters();

        // Then, process walk routes
        ProcessWalkRoutes();
    }

    /// <summary>
    /// Process characters that follow the player
    /// </summary>
    private void ProcessFollowingCharacters()
    {
        foreach (var character in _adventure.Characters.Values)
        {
            if (!character.FollowsPlayer)
                continue;

            // Move character to player's location
            if (_state.CharacterLocations.TryGetValue(character.Key, out var currentLocation))
            {
                if (currentLocation != _state.CurrentLocationKey)
                {
                    _state.CharacterLocations[character.Key] = _state.CurrentLocationKey;

                    // Output message if character was visible or becomes visible
                    _state.AddOutput($"{character.FullName} follows you.");
                }
            }
        }
    }

    /// <summary>
    /// Process walk routes for all characters
    /// </summary>
    private void ProcessWalkRoutes()
    {
        foreach (var character in _adventure.Characters.Values)
        {
            // Skip if no walk route or not movable
            if (!character.HasWalkRoute || !character.CanMove || character.WalkSteps.Count == 0)
                continue;

            // Skip if following player (following overrides walk route)
            if (character.FollowsPlayer)
                continue;

            // Get or initialize walk state
            if (!_state.CharacterWalkStates.TryGetValue(character.Key, out var walkState))
            {
                var firstStep = character.WalkSteps.OrderBy(s => s.StepNumber).FirstOrDefault();
                walkState = new CharacterWalkState
                {
                    CurrentStepIndex = 0,
                    TurnsUntilNextMove = firstStep?.DelayTurns ?? 0,
                    IsActive = true
                };
                _state.CharacterWalkStates[character.Key] = walkState;
            }

            if (!walkState.IsActive)
                continue;

            // Decrement turn counter
            if (walkState.TurnsUntilNextMove > 0)
            {
                walkState.TurnsUntilNextMove--;
                return;
            }

            // Execute current step
            ExecuteWalkStep(character, walkState);
        }
    }

    /// <summary>
    /// Execute a single walk step for a character
    /// </summary>
    private void ExecuteWalkStep(Character character, CharacterWalkState walkState)
    {
        var orderedSteps = character.WalkSteps.OrderBy(s => s.StepNumber).ToList();
        if (walkState.CurrentStepIndex >= orderedSteps.Count)
        {
            // End of walk route
            if (character.WalkLoops)
            {
                // Loop back to start
                walkState.CurrentStepIndex = 0;
            }
            else
            {
                // Stop walking
                walkState.IsActive = false;
                return;
            }
        }

        var currentStep = orderedSteps[walkState.CurrentStepIndex];

        // Get current character location
        var wasAtPlayerLocation = false;
        if (_state.CharacterLocations.TryGetValue(character.Key, out var currentLocation))
        {
            wasAtPlayerLocation = (currentLocation == _state.CurrentLocationKey);
        }

        // Move character to new location
        string newLocation;

        if (!string.IsNullOrEmpty(currentStep.LocationKey))
        {
            // Explicit location specified
            newLocation = currentStep.LocationKey;
        }
        else if (!string.IsNullOrEmpty(currentStep.Direction) && currentLocation != null)
        {
            // Direction specified - find destination from current location
            if (_adventure.Locations.TryGetValue(currentLocation, out var location))
            {
                var direction = location.Directions
                    .FirstOrDefault(d => d.DirectionName.Equals(currentStep.Direction,
                        StringComparison.OrdinalIgnoreCase));

                if (direction != null && !string.IsNullOrEmpty(direction.DestinationKey))
                {
                    newLocation = direction.DestinationKey;
                }
                else
                {
                    // Can't find direction, skip this step
                    AdvanceToNextStep(character, walkState, orderedSteps);
                    return;
                }
            }
            else
            {
                // Current location not found, skip
                AdvanceToNextStep(character, walkState, orderedSteps);
                return;
            }
        }
        else
        {
            // No location or direction specified, skip
            AdvanceToNextStep(character, walkState, orderedSteps);
            return;
        }

        // Update character location
        _state.CharacterLocations[character.Key] = newLocation;

        // Generate output messages if character moves into or out of player's location
        var nowAtPlayerLocation = (newLocation == _state.CurrentLocationKey);

        if (wasAtPlayerLocation && !nowAtPlayerLocation)
        {
            // Character left player's location
            var directionName = GetDirectionName(currentLocation, newLocation);
            if (!string.IsNullOrEmpty(directionName))
            {
                _state.AddOutput($"{character.FullName} leaves to the {directionName}.");
            }
            else
            {
                _state.AddOutput($"{character.FullName} leaves.");
            }
        }
        else if (!wasAtPlayerLocation && nowAtPlayerLocation)
        {
            // Character entered player's location
            var directionName = GetOppositeDirection(GetDirectionName(newLocation, currentLocation));
            if (!string.IsNullOrEmpty(directionName))
            {
                _state.AddOutput($"{character.FullName} arrives from the {directionName}.");
            }
            else
            {
                _state.AddOutput($"{character.FullName} arrives.");
            }
        }

        // Advance to next step
        AdvanceToNextStep(character, walkState, orderedSteps);
    }

    /// <summary>
    /// Advance to the next walk step
    /// </summary>
    private void AdvanceToNextStep(Character character, CharacterWalkState walkState, List<WalkStep> orderedSteps)
    {
        walkState.CurrentStepIndex++;

        if (walkState.CurrentStepIndex >= orderedSteps.Count)
        {
            if (character.WalkLoops)
            {
                walkState.CurrentStepIndex = 0;
            }
            else
            {
                walkState.IsActive = false;
                return;
            }
        }

        // Set delay for next step
        var nextStep = orderedSteps[walkState.CurrentStepIndex];
        walkState.TurnsUntilNextMove = nextStep.DelayTurns;
    }

    /// <summary>
    /// Get the direction name from one location to another
    /// </summary>
    private string GetDirectionName(string fromLocationKey, string toLocationKey)
    {
        if (!_adventure.Locations.TryGetValue(fromLocationKey, out var fromLocation))
            return string.Empty;

        var direction = fromLocation.Directions
            .FirstOrDefault(d => d.DestinationKey == toLocationKey);

        return direction?.DirectionName ?? string.Empty;
    }

    /// <summary>
    /// Get the opposite direction name (for arrival messages)
    /// </summary>
    private string GetOppositeDirection(string direction)
    {
        return direction.ToLower() switch
        {
            "north" => "south",
            "south" => "north",
            "east" => "west",
            "west" => "east",
            "northeast" => "southwest",
            "northwest" => "southeast",
            "southeast" => "northwest",
            "southwest" => "northeast",
            "up" => "down",
            "down" => "up",
            "in" => "out",
            "out" => "in",
            _ => direction
        };
    }

    /// <summary>
    /// Move a character to a specific location (for actions/tasks)
    /// </summary>
    public void MoveCharacter(string characterKey, string destinationKey)
    {
        if (!_adventure.Characters.ContainsKey(characterKey))
            return;

        _state.CharacterLocations[characterKey] = destinationKey;

        // Pause walk route when character is manually moved
        if (_state.CharacterWalkStates.TryGetValue(characterKey, out var walkState))
        {
            walkState.IsActive = false;
        }
    }

    /// <summary>
    /// Resume a character's walk route
    /// </summary>
    public void ResumeWalkRoute(string characterKey)
    {
        if (!_adventure.Characters.TryGetValue(characterKey, out var character))
            return;

        if (!character.HasWalkRoute || character.WalkSteps.Count == 0)
            return;

        if (_state.CharacterWalkStates.TryGetValue(characterKey, out var walkState))
        {
            walkState.IsActive = true;
        }
    }
}
