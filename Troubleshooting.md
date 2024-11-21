# Troubleshooting

## Intellisense isn't working

Check the bin folder for a "dump.h" file. If missing this file needs to be added. In your Urho3D checkout (from Git) the Docs/AngelscriptAPI.h file is the dump, copy it to the "bin" folder of the IDE and rename it to "dump.h" In the future it will likely be renamed to "AngelscriptAPI.h"

## Script API / Events / Attributes tabs aren't available

Copy the ScriptAPI.dox from your Urho3D checkout's Docs folder to the "bin" folder where the debugger is installed (or running from).

## When compiling using the Urho3D compilers errors/warnings aren't being logged

ScriptCompiler.exe is most likely out of date. ScriptCompiler.exe must have been built from a checkout later than April 15, 2015 so that the compiler will print in ASCII when it detects that its' output is being piped. Previously the compiler printed Unicode, which cannot be piped (with any sanity) on Windows.