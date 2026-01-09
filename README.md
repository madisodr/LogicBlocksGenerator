# LogicBlocksGenerator
LogicBlocksGenerator is a code gen tool for quickly creating setting up all the boiler plate code needed in order to use [Chickensoft's LogicBlocks](https://github.com/chickensoft-games/LogicBlocks) inside of a Godot project.

While [Chickensoft's LogicBlocks](https://github.com/chickensoft-games/LogicBlocks) being installed is not a requirement to run this tool, you should have it installed anyways. So go do that part first.

Tested on
- [x] Windows 11
- [x] MacOS
- [ ] *Nix

## Installation
```bash
>> dotnet tool install --global LogicBlocksGenerator --version 0.1.2

>> dotnet tool logicblocksgenerator  \
   --namespace MyGame.Application \
   --name Application \
   --template ./templates \
   --output ./MyGame/Application/ \
   --overwrite
```

Now you can run this with

## Build & Run From Source
```bash
>> git clone git@github.com:madisodr/LogicBlocksGenerator.git
>> cd LogicBlocksGenerator
>> dotnet build
```

### Run
Example, if you have a project namespace of "MyGame" and a folder "Application" which you want to generate a LogicBlock for, the following command will output the necessary files to ./MyGame/Application/State/*

```bash
dotnet run --  \
  --namespace MyGame.Application \
  --name Application \
  --template ./templates \
  --output ./MyGame/Application/ \
  --overwrite
```

## Tests
From within the LogicBlocksGenerator folder
```bash
>> dotnet test LogicBlocksGenerator.Tests/
```