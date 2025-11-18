using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Generates standard system tasks for common interactive fiction commands
/// </summary>
public static class SystemTaskGenerator
{
    /// <summary>
    /// Add standard system tasks to an adventure
    /// </summary>
    public static void AddSystemTasks(Adventure adventure)
    {
        // Take/Get object
        if (!HasTask(adventure, "System_Take"))
        {
            var takeTask = new Core.Models.Task
            {
                Key = "System_Take",
                Name = "Take object",
                Description = "Pick up an object",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            takeTask.Commands.Add(new TaskCommand { Command = "take %object%" });
            takeTask.Commands.Add(new TaskCommand { Command = "get %object%" });
            takeTask.Commands.Add(new TaskCommand { Command = "pick up %object%" });

            takeTask.Restrictions.Add("NOT Player has %object%");
            takeTask.Restrictions.Add("%object% at player location");

            takeTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "MoveObject",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" }, { "Destination", "Inventory" } }
            });

            takeTask.SuccessMessage = "You take %The[%object%]%.";
            takeTask.FailureMessage = "You can't take that.";

            adventure.Tasks.Add(takeTask.Key, takeTask);
        }

        // Drop object
        if (!HasTask(adventure, "System_Drop"))
        {
            var dropTask = new Core.Models.Task
            {
                Key = "System_Drop",
                Name = "Drop object",
                Description = "Drop an object you're carrying",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            dropTask.Commands.Add(new TaskCommand { Command = "drop %object%" });
            dropTask.Commands.Add(new TaskCommand { Command = "put down %object%" });

            dropTask.Restrictions.Add("Player has %object%");

            dropTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "MoveObject",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" }, { "Destination", "CurrentLocation" } }
            });

            dropTask.SuccessMessage = "You drop %The[%object%]%.";
            dropTask.FailureMessage = "You're not carrying that.";

            adventure.Tasks.Add(dropTask.Key, dropTask);
        }

        // Examine object
        if (!HasTask(adventure, "System_Examine"))
        {
            var examineTask = new Core.Models.Task
            {
                Key = "System_Examine",
                Name = "Examine object",
                Description = "Look at an object closely",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            examineTask.Commands.Add(new TaskCommand { Command = "examine %object%" });
            examineTask.Commands.Add(new TaskCommand { Command = "look at %object%" });
            examineTask.Commands.Add(new TaskCommand { Command = "x %object%" });

            examineTask.SuccessMessage = "%object%.LongDescription%";

            adventure.Tasks.Add(examineTask.Key, examineTask);
        }

        // Open object
        if (!HasTask(adventure, "System_Open"))
        {
            var openTask = new Core.Models.Task
            {
                Key = "System_Open",
                Name = "Open object",
                Description = "Open a container or door",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            openTask.Commands.Add(new TaskCommand { Command = "open %object%" });

            openTask.Restrictions.Add("%object% is openable");
            openTask.Restrictions.Add("NOT %object% is open");
            openTask.Restrictions.Add("NOT %object% is locked");

            openTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "SetObjectProperty",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" }, { "Property", "IsOpen" }, { "Value", "true" } }
            });

            openTask.SuccessMessage = "You open %The[%object%]%.";
            openTask.FailureMessage = "You can't open that.";

            adventure.Tasks.Add(openTask.Key, openTask);
        }

        // Close object
        if (!HasTask(adventure, "System_Close"))
        {
            var closeTask = new Core.Models.Task
            {
                Key = "System_Close",
                Name = "Close object",
                Description = "Close a container or door",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            closeTask.Commands.Add(new TaskCommand { Command = "close %object%" });

            closeTask.Restrictions.Add("%object% is openable");
            closeTask.Restrictions.Add("%object% is open");

            closeTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "SetObjectProperty",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" }, { "Property", "IsOpen" }, { "Value", "false" } }
            });

            closeTask.SuccessMessage = "You close %The[%object%]%.";
            closeTask.FailureMessage = "That's not open.";

            adventure.Tasks.Add(closeTask.Key, closeTask);
        }

        // Read object
        if (!HasTask(adventure, "System_Read"))
        {
            var readTask = new Core.Models.Task
            {
                Key = "System_Read",
                Name = "Read object",
                Description = "Read text on an object",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            readTask.Commands.Add(new TaskCommand { Command = "read %object%" });

            readTask.Restrictions.Add("%object% is readable");

            readTask.SuccessMessage = "%object%.ReadingText%";
            readTask.FailureMessage = "There's nothing to read on that.";

            adventure.Tasks.Add(readTask.Key, readTask);
        }

        // Wear object
        if (!HasTask(adventure, "System_Wear"))
        {
            var wearTask = new Core.Models.Task
            {
                Key = "System_Wear",
                Name = "Wear object",
                Description = "Put on wearable item",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            wearTask.Commands.Add(new TaskCommand { Command = "wear %object%" });
            wearTask.Commands.Add(new TaskCommand { Command = "put on %object%" });

            wearTask.Restrictions.Add("%object% is wearable");
            wearTask.Restrictions.Add("Player has %object%");

            wearTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "SetObjectProperty",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" }, { "Property", "IsWorn" }, { "Value", "true" } }
            });

            wearTask.SuccessMessage = "You put on %The[%object%]%.";
            wearTask.FailureMessage = "You can't wear that.";

            adventure.Tasks.Add(wearTask.Key, wearTask);
        }

        // Eat object
        if (!HasTask(adventure, "System_Eat"))
        {
            var eatTask = new Core.Models.Task
            {
                Key = "System_Eat",
                Name = "Eat object",
                Description = "Consume edible item",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            eatTask.Commands.Add(new TaskCommand { Command = "eat %object%" });
            eatTask.Commands.Add(new TaskCommand { Command = "consume %object%" });

            eatTask.Restrictions.Add("%object% is edible");
            eatTask.Restrictions.Add("Player has %object%");

            eatTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "RemoveObject",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" } }
            });

            eatTask.SuccessMessage = "You eat %The[%object%]%. It's quite tasty.";
            eatTask.FailureMessage = "You can't eat that.";

            adventure.Tasks.Add(eatTask.Key, eatTask);
        }

        // Talk to character
        if (!HasTask(adventure, "System_Talk"))
        {
            var talkTask = new Core.Models.Task
            {
                Key = "System_Talk",
                Name = "Talk to character",
                Description = "Speak with a character",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            talkTask.Commands.Add(new TaskCommand { Command = "talk to %character%" });
            talkTask.Commands.Add(new TaskCommand { Command = "speak to %character%" });
            talkTask.Commands.Add(new TaskCommand { Command = "greet %character%" });

            talkTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "ShowConversation",
                Parameters = new Dictionary<string, string> { { "Character", "%character%" } }
            });

            talkTask.SuccessMessage = ""; // Conversation system will handle output
            talkTask.FailureMessage = "There's no one here by that name.";

            adventure.Tasks.Add(talkTask.Key, talkTask);
        }

        // Give object to character
        if (!HasTask(adventure, "System_Give"))
        {
            var giveTask = new Core.Models.Task
            {
                Key = "System_Give",
                Name = "Give object to character",
                Description = "Give an item to a character",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            giveTask.Commands.Add(new TaskCommand { Command = "give %object% to %character%" });

            giveTask.Restrictions.Add("Player has %object%");
            giveTask.Restrictions.Add("%character% at player location");

            giveTask.SuccessActions.Add(new TaskAction
            {
                Order = 1,
                ActionType = "MoveObject",
                Parameters = new Dictionary<string, string> { { "Object", "%object%" }, { "Destination", "%character%" } }
            });

            giveTask.SuccessMessage = "You give %The[%object%]% to %The[%character%]%.";
            giveTask.FailureMessage = "You can't do that.";

            adventure.Tasks.Add(giveTask.Key, giveTask);
        }

        // Use object
        if (!HasTask(adventure, "System_Use"))
        {
            var useTask = new Core.Models.Task
            {
                Key = "System_Use",
                Name = "Use object",
                Description = "Use an object",
                Type = TaskType.System,
                Priority = 4, // Lower priority so specific use tasks take precedence
                IsRepeatable = true
            };

            useTask.Commands.Add(new TaskCommand { Command = "use %object%" });

            useTask.SuccessMessage = "You're not sure how to use that.";

            adventure.Tasks.Add(useTask.Key, useTask);
        }

        // Wait/Pass turn
        if (!HasTask(adventure, "System_Wait"))
        {
            var waitTask = new Core.Models.Task
            {
                Key = "System_Wait",
                Name = "Wait",
                Description = "Pass a turn without doing anything",
                Type = TaskType.System,
                Priority = 5,
                IsRepeatable = true
            };

            waitTask.Commands.Add(new TaskCommand { Command = "wait" });
            waitTask.Commands.Add(new TaskCommand { Command = "z" });

            waitTask.SuccessMessage = "Time passes...";

            adventure.Tasks.Add(waitTask.Key, waitTask);
        }
    }

    private static bool HasTask(Adventure adventure, string taskKey)
    {
        return adventure.Tasks.ContainsKey(taskKey);
    }

    /// <summary>
    /// Initialize an adventure with system tasks
    /// </summary>
    public static void InitializeAdventure(Adventure adventure)
    {
        AddSystemTasks(adventure);
    }
}
