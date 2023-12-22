# Milo

**Milo** is a codename for a **Toy Robot** simulator written in C# using cross platform .NET Core framework.

Milo can be placed on a surface of specified dimension (currently 5 units x 5 units) and can roam freely within the bounds of the surface. 

Currently Milo understands the following commands:

|Command|Description|
|---|---|
|PLACE|Puts Milo at a specified coordinate and facing NORTH, EAST, SOUTH or WEST|
|MOVE|Moves Milo one step ahead from its current coordinate in the direction it is facing|
|LEFT|Rotates Milo 90 degrees left without changing the position of the Milo|
|RIGHT|Rotates Milo 90 degrees right without changing the position of the Milo|
|REPORT|Reports the current state of Milo|

## Setting up Milo

### Prerequisites

|Software|Mandatory|Download Link|
|---|---|---|
|.NET Core 2.2|Yes|https://dotnet.microsoft.com/download|

> **All the commands below need to be executed in the command line mode for simplicity**

### Building Milo

Run `dotnet build` to build Milo.

### Running Milo

|OS|Command|
|---|---|
|Windows|`dotnet run --project Robot\Robot.csproj`|
|Non-windows|`dotnet run --project Robot/Robot.csproj`|

### Testing Milo

Run `dotnet test` to test Milo.

## Using Milo

![alt text](/Screenshots/WelcomeScreen.png?raw=true "Welcome screen")

Refer the table below to control Milo:

|Key|Purpose|Usage|Remarks|
|---|---|---|---|
|P|Triggers PLACE command|P 1,2,E|Once this key is pressed further 3 key actions are required to complete the command. First key press requires a **number** (this is mandated by Milo) which sets the **X coordinate**. Second key press again requires a **number** (this is also mandated by Milo) which sets the **Y** coordinate. Finally third key press requires a **character** (N/E/S/W) (this is again mandated by Milo) which sets the **direction**.
|M|Triggers MOVE command|M|
|L|Triggers LEFT command|L|
|R|Triggers RIGHT command|R|
|S|Triggers REPORT command|S|
|C|Clears the console window|C|
|ESC|Exits Milo Simulator|ESC|

The feedback is provided after every action so you know what Milo is doing. See Milo in action below:

![alt text](./Screenshots/MiloInAction.png?raw=true "Milo in action")
