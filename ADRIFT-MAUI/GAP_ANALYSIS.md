# ADRIFT-MAUI Gap Analysis
**Generated:** 2025-11-18

## Executive Summary

This document identifies the gaps between ADRIFT 5 and ADRIFT-MAUI for achieving 100% feature parity and backward compatibility.

**Current Status:** ~35% feature parity
**Target:** 100% feature parity with ADRIFT 5.0.36

---

## 1. MODEL GAPS

### 1.1 Location Model
**Current:** Basic location with short/long description and directions
**Missing:**
- `HideObjects` property
- Direction-specific properties (custom messages per exit, see ADRIFT-5/ADRIFT-CSharp/ADRIFT/Item Classes/clsLocation.cs:586)
- `ShowEnterText` / `ShowExitText`
- `ViewLocation` display logic
- 12 directional exits (currently has generic List, needs: N, NE, E, SE, S, SW, W, NW, Up, Down, In, Out)
- Per-direction restrictions (currently just string, needs full Restriction object)
- Properties system integration (need to support custom location properties)

### 1.2 AdriftObject Model
**Current:** Basic object with location, names, basic properties
**Missing:**
- Multiple names array (Names[]) - currently has single Name + Aliases
- `DynamicExistsWhereEnum` with all values:
  - `PartOfCharacter` (currently missing)
  - `PartOfObject` (currently missing)
  - `LocationGroup` (currently missing)
- `StaticExistsWhereEnum` for static objects:
  - `AllRooms`, `NoRooms`, `PartOfCharacter`, `PartOfObject`, `SingleLocation`, `LocationGroup`
- Surface support (IsSurface exists but no surface-specific properties)
- Wearable system (where worn, what it covers)
- Lightgiver advanced properties (turns until extinguished)
- Readable text property (currently just `ReadingText` string)
- Advanced list options
- Parent/child relationships
- OnObject vs InObject distinction (currently just ContainerKey)

### 1.3 Character Model
**Current:** Basic character with name, location, walks, topics
**Missing:**
- `Gender` enum (Male, Female, Unknown)
- `CharacterType` (Player vs NonPlayer) - currently has NPC/Companion/Enemy/etc but not ADRIFT 5's model
- `KnownTo` - which characters know this character
- `Descriptor` property separate from `Name`
- `Perspective` (First/Second/Third person)
- `Position` enum (Standing, Sitting, Lying)
- `ExistsWhereEnum` with all values:
  - `OnCharacter`, `InCharacter`, `OnSurface`, `InContainer`
- Battle system stats (Stamina, Strength, Defense, Accuracy)
- Walk system improvements:
  - `StartActive` flag
  - `Loop` option (currently has WalkLoops but needs more)
  - Status tracking
- Topic improvements:
  - `bCommand` - Execute as command vs conversation
  - `Stay` option (keep in conversation)
  - `ParentKey` - Parent topic for tree structure
  - `Introduction` text separate from response

### 1.4 Task Model
**Current:** Basic task with commands, restrictions, actions
**Missing:**
- `TaskTypeEnum` proper values (currently has General/System/Specific but needs ADRIFT 5 semantics)
- `RunImmediately` for system tasks
- `LocationTrigger` - execute when entering location
- `Specifics` - for specific tasks (object/character references)
- `References` - referenced items in command (need full reference system)
- `SpecificOverrideType` (Before/Override/After general tasks)
- Restrictions need full `clsRestriction` model:
  - `RestrictionTypeEnum` (Location, Object, Task, Character, Variable, Item, Property, Direction, Expression)
  - `MustEnum` (Must, MustNot)
  - Full restriction properties
- Actions need full `clsAction` model:
  - All `ItemEnum` types (MoveObject, AddRemoveObject, MoveCharacter, SetProperties, SetVariable, SetTasks, ExecuteTask, Conversation, EndGame, Time, Score, DisplayMessage)
  - `MoveObjectWhatEnum`, `MoveObjectToEnum` with all values
  - `MoveCharacterWhoEnum`, `MoveCharacterToEnum` with all values
  - `EndGameEnum` (Running, Win, Lose, Neutral)
- Multiple command patterns per task (currently has List<TaskCommand> but needs reference resolution)
- Optional references in commands
- Reference types (Object, Character, Number, Text, Direction, Location, Item)
- Up to 5 references per command

### 1.5 Event Model
**Current:** Basic event with triggers, sub-events, actions
**Missing:**
- `WhenStartEnum` (Immediately, AfterATask, BetweenXandYTurns)
- `StartDelay` property
- `Length` property (how long event runs)
- `WhatSetOff` (what triggers the event)
- `StatusEnum` (NotYetStarted, CountingDownToStart, Running, Finished, Paused)
- Sub-events (`arlSubEvents[]`) with:
  - When to execute (turn/time)
  - Actions to perform
  - Repeating option
- `MeasureEnum` (Turns, Seconds)
- `WhenShowEnum` (Always, StartOnly, Never)

### 1.6 Variable Model
**Current:** Basic variable with Integer/Text/Boolean types
**Missing:**
- Array support (Length property > 1)
- Proper `IntValue` / `StringValue` separation (currently has InitialValue/CurrentValue strings)
- Built-in variables:
  - `Score` (need to integrate with adventure-level)
  - `MaxScore`
  - `Turns`

### 1.7 Missing Model Classes
**Not implemented at all:**
- `clsProperty.cs` - Custom properties system
  - `PropertyTypeEnum` (SelectionOnly, Integer, Text, ObjectKey, CharacterKey, LocationKey, LocationGroupKey, StateList, ValueList)
  - `PropertyOfEnum` (Locations, Objects, Characters)
  - Mandatory properties
  - Dependent properties (show if another property matches)
  - Restricted values
  - AppendToProperty
  - Standard built-in properties
- `clsALR.cs` - Alternate Reality Layer / Text Overrides
  - `OldText`, `NewText` (Description with alternates)
  - Global text transformation system
- `clsMacro.cs` - User-defined command shortcuts
  - Title, Commands, Shared, IFID, Shortcut
- `clsUserFunction.cs` - User Defined Functions
  - Name, Output (Description), Arguments
  - Argument types (Object, Character, Location, Number, Text)
- `clsMap.cs` - Map system
  - MapNode, MapLink, MapPage classes
  - Multi-page maps
  - 3D coordinates
  - Auto-layout
- `clsSound.cs` - Sound/multimedia system
  - Multi-channel audio (8 channels)
  - Multiple formats (WAV, MP3, MIDI, OGG)
  - Volume control, looping, pause/resume
- `clsState.cs` - Game state for undo/redo
  - State stack
  - State serialization
- `Folder` - Folder organization system
  - Not yet implemented

### 1.8 Description Model
**Current:** Basic description with restrictions
**Missing:**
- All the text formatting functions:
  - `%CharacterName[key]%`, `%ObjectName[key]%`, `%LocationName[key]%`
  - Articles: `%a[object]%`, `%the[object]%`
  - Pronouns: `%he%`, `%she%`, `%it%`, `%they%`, `%his%`, `%her%`, `%its%`, `%their%`
  - Capitalization: `%proper[text]%`, `%sentence[text]%`
  - Numbers: `%number[123]%`
  - Location info: `%CharacterLocation[key]%`, `%ObjectLocation[key]%`
  - Lists: `%ListObjectsAtLocation[key]%`
  - Game state: `%Player%`, `%Time%`, `%Score%`, `%MaxScore%`, `%Turns%`
  - Directions: `%Direction[N]%`
  - Properties: `%property[key]%`
  - Expressions: `%expr[1+2*3]%`
  - Conditionals: `%if[condition]text1%else%text2%ifend%`
  - Random: `%random[text1|text2|text3]%`
- Image embedding: `<img src="...">`
- Audio embedding: `<audio src="..." channel=1 loop=true>`

---

## 2. ENGINE GAPS

### 2.1 Command Processing
**Current:** Basic command parser with pattern matching
**Missing:**
- Pronoun resolution ("it", "them", "him", "her")
- Autocomplete system
- Multi-word command support (currently limited)
- Ambiguity handling (ask for clarification)
- Reference matching for all reference types
- Synonym processing integration
- Task execution modes (HighestPriorityTask vs HighestPriorityPassingTask)

### 2.2 Task Execution
**Current:** Basic task matching and execution
**Missing:**
- Specific task override (Before/Override/After)
- System tasks auto-execution
- Task chaining via ExecuteTask action
- Task queue for sequential execution
- Location trigger tasks
- Priority-based sorting (0-99999 range)

### 2.3 State Management
**Current:** Basic game state (location, score, turns)
**Missing:**
- State stack for undo/redo
- State serialization for save/restore
- Timer system for real-time events
- Full state preservation (all game state)

### 2.4 Display & Output
**Current:** Basic text output
**Missing:**
- Rich text/HTML formatting
- All text formatting functions (see 1.8 above)
- ALR (Text Override) processing
- Image display
- Sound playback
- Custom fonts/colors
- Status bar system

### 2.5 Special Commands
**Current:** Some system tasks generated
**Missing:**
- Full implementation of all special commands
- Undo command
- Transcript recording
- Debug mode commands

### 2.6 Character AI
**Current:** Basic character movement manager
**Missing:**
- Full walk execution with status tracking
- Pathfinding improvements
- NPC autonomous actions
- Character inventory management

### 2.7 Restriction Evaluation
**Current:** Basic restriction evaluator
**Missing:**
- All restriction types
- Full expression evaluation in restrictions
- Property value checks
- Direction availability checks

---

## 3. UI GAPS

### 3.1 Developer UI
**Current:** ~60% complete - basic MVVM structure, partial editors
**Missing Editor Pages:**
- Property editor
- Text Override (ALR) editor
- User Function editor
- Macro editor
- Map editor
- Complete all existing editors with all properties

**Missing Features in Existing Editors:**
- Drag-and-drop support
- Syntax highlighting for expressions
- Preview panes for descriptions
- Find/replace across adventure
- Reference tracking
- Dependency tracking
- Broken reference detection
- Undo/redo system

### 3.2 Runner UI
**Current:** ~20% complete - basic structure
**Missing:**
- Game output display (rich text with HTML)
- Command input with autocomplete
- Inventory display panel
- Map display panel
- Graphics panel
- Status bar
- Debugger panel
- Mouse click interactions
- Menu customization
- Custom fonts/colors
- Save/restore UI
- Transcript UI
- Hints UI
- Score display

---

## 4. FILE I/O GAPS

**Current:** Complete TAF/XML support with compression and obfuscation
**Missing:**
- Blorb resource extraction (images, sounds)
- EXE standalone generation
- V4 compatibility mode for loading ADRIFT 4 games
- V4 media extraction
- Library import/export

---

## 5. ADVANCED FEATURE GAPS

### 5.1 Property System
**Status:** Not implemented
**Required:**
- Full property definition system
- Standard built-in properties
- Custom properties
- Property groups
- Dependent properties
- Restricted values
- Append states

### 5.2 Map System
**Status:** Not implemented
**Required:**
- Map node/link/page classes
- Auto-layout algorithm
- Multi-page support
- 3D coordinates
- Visual map editor
- Runner map display

### 5.3 Sound System
**Status:** Scaffolded (Plugin.Maui.Audio reference) but not implemented
**Required:**
- Multi-channel audio
- Multiple format support
- Blorb resource extraction
- Audio tags in descriptions
- Volume control
- Looping, pause/resume

### 5.4 Multimedia
**Status:** Not implemented
**Required:**
- Image display
- Image embedding in descriptions
- Blorb image extraction
- Video playback (if supported in ADRIFT 5)

### 5.5 ALR (Text Overrides)
**Status:** Not implemented
**Required:**
- ALR model class
- Find/replace text system
- Conditional alternates on replacements
- Global text transformation

### 5.6 User Functions
**Status:** Not implemented
**Required:**
- UDF model class
- Function calling in text
- Argument passing
- Dynamic text generation

### 5.7 Macros
**Status:** Not implemented
**Required:**
- Macro model class
- Macro execution
- Keyboard shortcuts
- Game-specific vs shared macros

### 5.8 Battle System
**Status:** Not implemented (optional)
**Required:**
- Battle stats on characters
- Combat properties
- Battle mechanics (if used)

### 5.9 Babel Treaty Support
**Status:** Partially implemented
**Missing:**
- Full Babel metadata
- IFID generation
- Cover art
- Bibliographic data

### 5.10 Libraries
**Status:** Not implemented
**Required:**
- Library item marking
- Import/export
- Library tracking
- Update mechanism

### 5.11 Undo/Redo
**Status:** Not implemented
**Required:**
- State stack
- Undo command
- State differential tracking

### 5.12 Debugging
**Status:** Partial
**Missing:**
- Debug levels
- Visual debugger UI
- Task tracing
- Restriction evaluation display

### 5.13 Folders
**Status:** Not implemented
**Required:**
- Folder organization
- Folder navigation
- Items in folders

---

## 6. PRIORITY RANKING

### P0 - Critical for Compatibility (Must have for basic game playback)
1. Complete all model properties (Location, Object, Character, Task, Event)
2. Full restriction system with all restriction types
3. Full action system with all action types
4. Complete Description text function support
5. Property system (required by many games)
6. Complete Runner UI (game playback)

### P1 - High Priority (Common features)
1. ALR (Text Overrides)
2. Map system
3. Sound system
4. Complete all Developer editors
5. User Functions
6. Undo/Redo system
7. Save/Restore game state

### P2 - Medium Priority (Advanced features)
1. Macros
2. Battle system
3. Blorb resource support
4. Visual debugger
5. Libraries
6. Folders
7. V4 compatibility mode

### P3 - Low Priority (Nice to have)
1. EXE standalone generation
2. Enhanced UI features (drag-drop, syntax highlighting)
3. Find/replace
4. Reference tracking
5. Advanced editor features

---

## 7. IMPLEMENTATION ROADMAP

### Phase 1: Core Model Completion (2-3 days)
- Expand all model classes with missing properties
- Implement Property system
- Implement Restriction classes
- Implement Action classes
- Add missing model classes (ALR, UserFunction, Macro, Map, Sound, State)

### Phase 2: Engine Enhancement (3-4 days)
- Complete restriction evaluation
- Complete action execution
- Add all text formatting functions
- Implement pronoun resolution
- Implement autocomplete
- Implement task execution modes

### Phase 3: Description System (1-2 days)
- Implement all text functions
- Add image/audio embedding support
- Integrate ALR processing
- Add expression evaluation in text

### Phase 4: Runner UI (2-3 days)
- Complete game output display
- Implement command input with autocomplete
- Add inventory panel
- Add map panel
- Add status bar
- Implement save/restore UI
- Add hints UI

### Phase 5: Developer UI (3-4 days)
- Complete all existing editors
- Add missing editors (Property, ALR, UserFunction, Macro, Map)
- Implement advanced features (undo/redo, find/replace, reference tracking)
- Add preview panes

### Phase 6: Advanced Features (2-3 days)
- Map system implementation
- Sound system implementation
- Undo/Redo with state stack
- Blorb resource extraction
- Visual debugger

### Phase 7: Polish & Testing (2-3 days)
- Test with real ADRIFT 5 games
- Fix compatibility issues
- Performance optimization
- Bug fixes
- Documentation

**Total Estimated Time: 15-22 days**

---

## 8. SUCCESS CRITERIA

### 100% Feature Parity Checklist
- [ ] All ADRIFT 5 model properties implemented
- [ ] All restriction types supported
- [ ] All action types supported
- [ ] All text functions implemented
- [ ] Property system fully functional
- [ ] Map system complete
- [ ] Sound system complete
- [ ] Runner can play all ADRIFT 5 games
- [ ] Developer can edit all game elements
- [ ] Round-trip compatibility (load/save preserves all data)
- [ ] All special commands work
- [ ] Full UI feature parity

### 100% Compatibility Checklist
- [ ] Load all ADRIFT 5.0.36 TAF files
- [ ] Preserve all game data on save
- [ ] Execute all game mechanics correctly
- [ ] Display all game content correctly
- [ ] Support all game features (map, sound, images, etc.)
- [ ] Handle all edge cases
- [ ] V4 compatibility (if required)

---

## Next Steps

1. Start with Phase 1: Core Model Completion
2. Implement missing model properties systematically
3. Add Property system
4. Add missing model classes
5. Test file I/O with expanded models
6. Continue to Phase 2 and beyond

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
