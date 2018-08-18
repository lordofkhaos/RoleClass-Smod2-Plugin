# RoleClass-Smod2-Plugin

## What is it?
RoleClass is a plugin that lets you assign items to a certain role.

## How do I use it?
Simply download the file from [here](https://github.com/lordofkhaos/RoleClass-Smod2-Plugin/releases/latest) and put it in the folder titled `sm_plugins`. **Requires [Smod](https://github.com/Grover-c13/Smod2) to function.**

## How do I configure it?

### Config Options
Navigate to your `config_gameplay.txt` file, and put the following in (anywhere):

| Config Option  | Type | Default | Description |
| ------------- | ------------- | ------------- | ------------- |
| k_global_give  | Dictionary  | **EMPTY** | Use this config option to give any role whatever item you want. Format: `role:item#`. A list of items is available on the Smod discord. NOTE: Affects globally (doesn't matter the class). |

### Commands
| Command Name  | Arg | Arg | Arg | Description |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| save | rank  | class | itemlist | Use this command to save a configuration to a role. |
