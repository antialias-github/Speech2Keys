﻿/*
 * Created by SharpDevelop.
 * User: Peter
 * Date: 10.11.2014
 * Time: 08:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Speech2Keys
{
	/// <summary>
	/// Description of CommandList.
	/// </summary>
	[Serializable()]
	public class CommandList
	{	
		public string version;
		public List<Command> listOfCommands;
		// other config stuff
		public List<string> standardResponses;
		public string ProfileName;
		public string commandToBeErased;
		
		//TODO: add all other stuff here from the settings and config
		
		public CommandList()
		{
			version = "1,0,0";
			listOfCommands = new List<Command>();
			standardResponses = new List<string>();
			Reset();
		}
	
		public void Reset()
		{
			listOfCommands.Clear();
			commandToBeErased = "";
		}
		
		public bool Add(Command _command, out string result)
		{
			if (_command.isValid(out result) && ListIsValid(_command, out result))
			{
				RemoveCommand(_command.name); // overwrite duplicates
				// if the command uses standard responses plug them in
				RemoveCommand(commandToBeErased);
				if (_command.useStandardResponses)
					foreach (var s in standardResponses)
						if (!_command.responses.Contains(s))
							_command.responses.Add(s);
				
			    listOfCommands.Add(_command);
			    result += "Command added" + Environment.NewLine;
			    return true;
			}
			
			result+= "Command not added. Please resolve conflicts and try again" + Environment.NewLine;
			return false;
		}
	
		public bool CommandIsAlreadyDefined (string name)
		{
			foreach (var c in listOfCommands)
				if (c.name == name)
					return true;
			return false;
		}
		
		public Command GetCommand (string name)
		{
			foreach (var c in listOfCommands)
				if (c.name == name)
					return c;
			return null;
		}
		
		public void RemoveCommand (string _name)
		{
			Command foundCommand = null;
			foreach (var c in listOfCommands)
			{
				if (c.name == _name)
				{
					foundCommand = c;
					break;
				}
			}
			if (null != foundCommand)
				listOfCommands.Remove(foundCommand);
		}
		
		
		public bool ListIsValid(Command _command, out string error)
		{
			// check that list would be valid if _command was added to it
			// invalidation criteria:
			// - command contains a keyword/phrase that is already assigned in another command
			
			// ignore commands of the same name,as this one will be replaced
			
			error ="";
			bool isValid = true;
			
			// keyphrase already defined
			foreach (var c in listOfCommands)
				if (c.name != _command.name && c.name != commandToBeErased)
					foreach (var k in c.keyPhrases)
						if (_command.keyPhrases.Contains(k))				
						{
							error+= "keyword/phrase '" + k + "' already defined in command " + c.name +Environment.NewLine;
							isValid = false;
						}
						
			return isValid;
		}
		
		public List<Command> CreateStandardCommands()
		{
				var result = new List<Command>();
			
				Command command1 = new Command();
				command1.name = "Standard Command - AIName";
				result.Add(command1);
				command1.keyPhrases.Add("Flora");
				command1.responses.Add("Listening");
				listOfCommands.Add(command1);
				
				Command command2 = new Command();
				command2.name = "Standard Command - Jokes";
				result.Add(command2);
				command2.keyPhrases.Add("Tell me a joke");
				command2.keyPhrases.Add("Give me a joke");
				command2.keyPhrases.Add("Tell me something funny");
				command2.responses.Add("I have nothing funny to say");
				listOfCommands.Add(command2);
				
				Command command3 = new Command();
				command3.name = "Standard Command - Pause Recognition";
				result.Add(command3);
				command3.keyPhrases.Add("Pause speech recognition");
				command3.keyPhrases.Add("Stop listening");
				command3.keyPhrases.Add("Go offline");
				command3.keyPhrases.Add("Deactivate speech recognition");
				command3.responses.Add("Going offline");
				listOfCommands.Add(command3);
				
				Command command4 = new Command();
				command4.name = "Standard Command - Reactivate Recognition";
				result.Add(command4);
				command4.keyPhrases.Add("Resume speech recognition");
				command4.keyPhrases.Add("Start listening");
				command4.keyPhrases.Add("Come online");
				command4.keyPhrases.Add("Reactivate speech recognition");
				command4.responses.Add("Back online");
				listOfCommands.Add(command4);
				
				Command command5 = new Command();
				command5.name = "Standard Command - Stop Speech Output";
				result.Add(command5);
				command5.keyPhrases.Add("Shut up");
				command5.keyPhrases.Add("Stop speech output");
				command5.keyPhrases.Add("Be silent");
				command5.responses.Add("Shutting up");
				listOfCommands.Add(command5);
				
				standardResponses.Clear();
				standardResponses.Add("Roger");
				standardResponses.Add("You got it");
				standardResponses.Add("Affirmative");
				standardResponses.Add("Will do");
				
				return result;
		}
	}
}
